using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class ClassControlDupl
{
    public enum DeleteStrategy
    {
        KeepOldest,
        KeepNewest,
        KeepFirstFound,
        KeepLargestName,
        KeepShortestName
    }

    public async Task<DeleteResult> DeleteDuplicatesAsync(
        List<DuplicateGroup> duplicateGroups,
        DeleteStrategy strategy,
        IProgress<int> progress = null,
        CancellationToken cancellationToken = default)
    {
        var result = new DeleteResult();
        int totalFilesToDelete = duplicateGroups.Sum(g => g.Files.Count - 1);
        int processed = 0;

        foreach (var group in duplicateGroups)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var filesToDelete = GetFilesToDelete(group, strategy);

            foreach (var file in filesToDelete)
            {
                cancellationToken.ThrowIfCancellationRequested();

                try
                {
                    if (File.Exists(file.Path))
                    {
                        File.Delete(file.Path);
                        result.DeletedFiles.Add(file.Path);
                        result.TotalFreedSpace += file.Size;
                    }
                    else
                    {
                        result.NotFoundFiles.Add(file.Path);
                    }
                }
                catch (UnauthorizedAccessException ex)
                {
                    result.Errors.Add($"Нет прав доступа: {file.Path} - {ex.Message}");
                }
                catch (IOException ex)
                {
                    result.Errors.Add($"Ошибка ввода-вывода: {file.Path} - {ex.Message}");
                }
                catch (Exception ex)
                {
                    result.Errors.Add($"Ошибка при удалении {file.Path}: {ex.Message}");
                }

                processed++;
                progress?.Report((processed * 100) / Math.Max(1, totalFilesToDelete));
            }
        }

        return result;
    }

    private List<FileInfoSaDDF> GetFilesToDelete(DuplicateGroup group, DeleteStrategy strategy)
    {
        var files = group.Files;

        if (files.Count <= 1)
            return new List<FileInfoSaDDF>();

        switch (strategy)
        {
            case DeleteStrategy.KeepOldest:
                var oldest = files.OrderBy(f => f.Created).First();
                return files.Except(new[] { oldest }).ToList();

            case DeleteStrategy.KeepNewest:
                var newest = files.OrderByDescending(f => f.Modified).First();
                return files.Except(new[] { newest }).ToList();

            case DeleteStrategy.KeepFirstFound:
                var first = files.OrderBy(f => f.Path).First();
                return files.Except(new[] { first }).ToList();

            case DeleteStrategy.KeepLargestName:
                var largestName = files.OrderByDescending(f => f.Name.Length).First();
                return files.Except(new[] { largestName }).ToList();

            case DeleteStrategy.KeepShortestName:
                var shortestName = files.OrderBy(f => f.Name.Length).First();
                return files.Except(new[] { shortestName }).ToList();

            default:
                return files.Skip(1).ToList();
        }
    }

    public async Task<DeleteResult> DeleteSelectedDuplicatesAsync(
        List<DuplicateGroup> allDuplicates,
        List<int> selectedGroupIndices,
        DeleteStrategy strategy,
        IProgress<int> progress = null,
        CancellationToken cancellationToken = default)
    {
        var selectedGroups = selectedGroupIndices
            .Where(i => i >= 0 && i < allDuplicates.Count)
            .Select(i => allDuplicates[i])
            .ToList();

        return await DeleteDuplicatesAsync(selectedGroups, strategy, progress, cancellationToken);
    }

    public void MoveDuplicatesToFolder(List<DuplicateGroup> duplicateGroups, string targetFolder)
    {
        if (!Directory.Exists(targetFolder))
            Directory.CreateDirectory(targetFolder);

        foreach (var group in duplicateGroups)
        {
            var filesToMove = group.Files.Skip(1);

            foreach (var file in filesToMove)
            {
                try
                {
                    if (File.Exists(file.Path))
                    {
                        var newPath = Path.Combine(targetFolder,
                            $"{Path.GetFileNameWithoutExtension(file.Name)}_dup_{Guid.NewGuid():N}{Path.GetExtension(file.Name)}");

                        File.Move(file.Path, newPath);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при перемещении файла {file.Path}: {ex.Message}");
                }
            }
        }
    }
}

public class DeleteResult
{
    public List<string> DeletedFiles { get; set; } = new List<string>();
    public List<string> NotFoundFiles { get; set; } = new List<string>();
    public List<string> Errors { get; set; } = new List<string>();
    public long TotalFreedSpace { get; set; }

    public bool HasErrors => Errors.Any();
    public bool HasNotFoundFiles => NotFoundFiles.Any();
}