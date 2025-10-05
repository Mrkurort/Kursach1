using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public class DuplicateManager
{
    public enum DeleteStrategy
    {
        KeepOldest,
        KeepNewest,
        KeepFirstFound,
        KeepSpecificPath
    }

    public async Task<List<string>> DeleteDuplicatesAsync(
        List<DuplicateGroup> duplicateGroups,
        DeleteStrategy strategy,
        string keepPath = null,
        IProgress<int> progress = null)
    {
        return await DeleteDuplicatesAsync(duplicateGroups, strategy, keepPath, progress, System.Threading.CancellationToken.None);
    }

    public async Task<List<string>> DeleteDuplicatesAsync(
        List<DuplicateGroup> duplicateGroups,
        DeleteStrategy strategy,
        string keepPath,
        IProgress<int> progress,
        System.Threading.CancellationToken cancellationToken)
    {
        var deletedFiles = new List<string>();
        int totalFiles = duplicateGroups.Sum(g => g.Files.Count - 1);
        int processed = 0;

        foreach (var group in duplicateGroups)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var filesToDelete = GetFilesToDelete(group, strategy, keepPath);

            foreach (var file in filesToDelete)
            {
                cancellationToken.ThrowIfCancellationRequested();

                try
                {
                    File.Delete(file.Path);
                    deletedFiles.Add(file.Path);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при удалении файла {file.Path}: {ex.Message}");
                }

                processed++;
                progress?.Report((processed * 100) / Math.Max(1, totalFiles));
            }
        }

        return deletedFiles;
    }

    private List<FileInfoSaDDF> GetFilesToDelete(DuplicateGroup group, DeleteStrategy strategy, string keepPath)
    {
        var files = group.Files;

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

            case DeleteStrategy.KeepSpecificPath:
                if (!string.IsNullOrEmpty(keepPath))
                {
                    var keepFile = files.FirstOrDefault(f =>
                        f.Path.StartsWith(keepPath, StringComparison.OrdinalIgnoreCase));
                    if (keepFile != null)
                    {
                        return files.Except(new[] { keepFile }).ToList();
                    }
                }
                goto case DeleteStrategy.KeepOldest;

            default:
                return files.Skip(1).ToList();
        }
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
                    var newPath = Path.Combine(targetFolder,
                        $"{Path.GetFileNameWithoutExtension(file.Name)}_dup_{Guid.NewGuid():N}{Path.GetExtension(file.Name)}");

                    File.Move(file.Path, newPath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при перемещении файла {file.Path}: {ex.Message}");
                }
            }
        }
    }
}