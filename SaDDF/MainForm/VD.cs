using System;
using System.Collections.Generic;

public class FileInfoSaDDF
{
    public string Path { get; set; }
    public string Name { get; set; }
    public long Size { get; set; }
    public string Hash { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
}

public class DuplicateGroup
{
    public string Hash { get; set; }
    public long Size { get; set; }
    public List<FileInfoSaDDF> Files { get; set; } = new List<FileInfoSaDDF>();
}