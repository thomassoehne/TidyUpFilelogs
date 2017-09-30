using System.Collections.Generic;

namespace TidyUpFilelogs.FileScanning.Interfaces
{
    public interface IFileScanner
    {
        List<string> GetFilesFiltered(string directoryName);
    }
}
