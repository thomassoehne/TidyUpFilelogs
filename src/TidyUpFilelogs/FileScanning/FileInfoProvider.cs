using System.Collections.Generic;
using System.IO;
using System.Linq;
using TidyUpFilelogs.FileScanning.Interfaces;

namespace TidyUpFilelogs.FileScanning
{
    public class FileInfoProvider : IFileInfoProvider
    {
        public List<IFlatFileInformation> GetFileInfos(string directoryName)
        {
            var result = Directory.GetFiles(directoryName).Select(s =>
            {
                var fileinfo = new FileInfo(s);
                return new FlatFileInformation(fileinfo.FullName, fileinfo.CreationTime);
            })
            .ToList<IFlatFileInformation>();

            return result;
        }
    }
}