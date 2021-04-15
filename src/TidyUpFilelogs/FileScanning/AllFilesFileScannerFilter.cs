using System;
using TidyUpFilelogs.FileScanning.Interfaces;

namespace TidyUpFilelogs.FileScanning
{
    public class AllFilesFileScannerFilter : IFileScannerFilter
    {
        public AllFilesFileScannerFilter()
        {
            this.Expression = SimpleReturnTrue;
        }

        public Func<IFlatFileInformation, bool> Expression { get; private set; }

        private static bool SimpleReturnTrue(IFlatFileInformation information)
        {
            return true;
        }
    }
}