using System;

namespace TidyUpFilelogs.FileScanning.Interfaces
{
    public interface IFlatFileInformation
    {
        string FullName { get; }
        DateTime CreationTime { get; }
    }
}
