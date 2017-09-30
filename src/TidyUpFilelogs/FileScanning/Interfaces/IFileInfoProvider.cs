using System.Collections.Generic;

namespace TidyUpFilelogs.FileScanning.Interfaces
{
    public interface IFileInfoProvider
    {
        List<IFlatFileInformation> GetFileInfos(string directoryName);
    }
}
