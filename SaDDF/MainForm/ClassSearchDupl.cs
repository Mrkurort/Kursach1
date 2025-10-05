using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

public class DuplicateFinder
{
    private readonly List<string> _searchPaths;
    private readonly List<string> _excludedPaths;
    private readonly List<string> _allowedExtensions;
    private readonly bool _includeSubdirectories;
    private readonly long _minFileSize;
    private readonly long _maxFileSize;
    private readonly int _bufferSize;

    public List<string> SearchPaths => _searchPaths;
    public bool IncludeSubdirectories => _includeSubdirectories;

    public DuplicateFinder(List<string> searchPaths, List<string> excludedPaths = null,
                          List<string> allowedExtensions = null, bool includeSubdirectories = true,
                          long minFileSize = 0, long maxFileSize = long.MaxValue,
                          int bufferSize = 81920)
    {
        _searchPaths = searchPaths;
        _excludedPaths = excludedPaths ?? new List<string>();
        _allowedExtensions = allowedExtensions;
        _includeSubdirectories = includeSubdirectories;
        _minFileSize = minFileSize;
        _maxFileSize = maxFileSize;
        _bufferSize = bufferSize;
    }

    public async Task<List<DuplicateGroup>> FindDuplicatesAsync(IProgress<int> progress = null)
    {
        return await FindDuplicatesAsync(progress, CancellationToken.None);
    }

    public async Task<List<DuplicateGroup>> FindDuplicatesAsync(IProgress<int> progress, CancellationToken cancellationToken)
    {
        var allFiles = await GetAllFilesAsync(progress, cancellationToken);
        cancellationToken.ThrowIfCancellationRequested();

        var duplicateGroups = await FindDuplicateGroupsAsync(allFiles, progress, cancellationToken);
        return duplicateGroups;
    }

    private async Task<List<FileInfoSaDDF>> GetAllFilesAsync(IProgress<int> progress, CancellationToken cancellationToken)
    {
        var files = new List<FileInfoSaDDF>();
        var allFilePaths = new List<string>();

        // Собираем все файлы из указанных путей
        foreach (var path in _searchPaths)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (Directory.Exists(path))
            {
                var searchOption = _includeSubdirectories ?
                    SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

                var filePaths = Directory.GetFiles(path, "*.*", searchOption)
                    .Where(p => !IsExcluded(p))
                    .Where(p => IsExtensionAllowed(p))
                    .Where(p => IsSizeInRange(p))
                    .ToList();

                allFilePaths.AddRange(filePaths);
            }
        }

        // Создаем объекты FileInfoSaDDF
        for (int i = 0; i < allFilePaths.Count; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var filePath = allFilePaths[i];
            try
            {
                var fileInfo = new System.IO.FileInfo(filePath);
                files.Add(new FileInfoSaDDF
                {
                    Path = filePath,
                    Name = fileInfo.Name,
                    Size = fileInfo.Length,
                    Created = fileInfo.CreationTime,
                    Modified = fileInfo.LastWriteTime
                });

                progress?.Report((i * 50) / allFilePaths.Count);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обработке файла {filePath}: {ex.Message}");
            }
        }

        return files;
    }

    private async Task<List<DuplicateGroup>> FindDuplicateGroupsAsync(
        List<FileInfoSaDDF> files,
        IProgress<int> progress,
        CancellationToken cancellationToken)
    {
        var sizeGroups = files
            .GroupBy(f => f.Size)
            .Where(g => g.Count() > 1)
            .ToList();

        var duplicateGroups = new List<DuplicateGroup>();
        int processed = 0;

        // Обрабатываем группы файлов одинакового размера
        foreach (var sizeGroup in sizeGroups)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var hashGroups = await GroupByHashAsync(sizeGroup.ToList(), cancellationToken);

            foreach (var hashGroup in hashGroups.Where(g => g.Files.Count > 1))
            {
                duplicateGroups.Add(hashGroup);
            }

            processed++;
            progress?.Report(50 + (processed * 50 / Math.Max(1, sizeGroups.Count)));
        }

        return duplicateGroups;
    }

    private async Task<List<DuplicateGroup>> GroupByHashAsync(List<FileInfoSaDDF> files, CancellationToken cancellationToken)
    {
        var hashGroups = new Dictionary<string, DuplicateGroup>();
        var tasks = new List<Task>();

        // Ограничиваем количество одновременных операций
        var semaphore = new SemaphoreSlim(Environment.ProcessorCount);

        foreach (var file in files)
        {
            await semaphore.WaitAsync(cancellationToken);

            tasks.Add(Task.Run(async () =>
            {
                try
                {
                    file.Hash = await CalculateFileHashAsync(file.Path, cancellationToken);

                    lock (hashGroups)
                    {
                        if (!hashGroups.ContainsKey(file.Hash))
                        {
                            hashGroups[file.Hash] = new DuplicateGroup
                            {
                                Hash = file.Hash,
                                Size = file.Size
                            };
                        }

                        hashGroups[file.Hash].Files.Add(file);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при вычислении хеша файла {file.Path}: {ex.Message}");
                }
                finally
                {
                    semaphore.Release();
                }
            }, cancellationToken));
        }

        await Task.WhenAll(tasks);
        return hashGroups.Values.ToList();
    }

    private async Task<string> CalculateFileHashAsync(string filePath, CancellationToken cancellationToken)
    {
        using (var md5 = MD5.Create())
        {
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, _bufferSize, true))
            {
                var hash = await Task.Run(() => md5.ComputeHash(stream), cancellationToken);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }
    }

    public bool IsExcluded(string filePath)
    {
        return _excludedPaths.Any(excluded =>
            filePath.StartsWith(excluded, StringComparison.OrdinalIgnoreCase));
    }

    public bool IsExtensionAllowed(string filePath)
    {
        if (_allowedExtensions == null || !_allowedExtensions.Any())
            return true;

        var extension = Path.GetExtension(filePath).ToLowerInvariant();
        return _allowedExtensions.Contains(extension);
    }

    public bool IsSizeInRange(string filePath)
    {
        try
        {
            var fileInfo = new System.IO.FileInfo(filePath);
            return fileInfo.Length >= _minFileSize && fileInfo.Length <= _maxFileSize;
        }
        catch
        {
            return false;
        }
    }
}