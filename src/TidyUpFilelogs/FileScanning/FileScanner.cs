using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using TidyUpFilelogs.FileScanning.Interfaces;

namespace TidyUpFilelogs.FileScanning
{
    public class FileScanner : IFileScanner
    {
        private IFileInfoProvider fileInfoProvider;
        private IFileScannerFilter filescannerFilter;

        public FileScanner(IFileInfoProvider fileInfoProvider, IFileScannerFilter filter)
        {
            this.fileInfoProvider = fileInfoProvider;
            this.filescannerFilter = filter;
        }

        public List<string> GetFilesFiltered(string directoryName)
        {
            var filterExpression = this.filescannerFilter.Expression;

            var result = this.fileInfoProvider.GetFileInfos(directoryName)
                            .Where(filterExpression)
                            .Select(fi => fi.FullName).ToList();

            return result;
        }
    }
}