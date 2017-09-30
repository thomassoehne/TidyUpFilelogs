using System;

namespace TidyUpFilelogs.FileScanning.Interfaces
{
    public interface IFileScannerFilter
    {
        Func<IFlatFileInformation, bool> Expression { get; }
    }
}
