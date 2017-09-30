using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TidyUpFilelogs.FileScanning.Interfaces;

namespace TidyUpFilelogs.FileScanning
{
    public class FiveDaysOldFileScannerFilter : IFileScannerFilter
    {
        public FiveDaysOldFileScannerFilter()
        {
            Expression = IsFileOlderThanFiveDays;
        }

        public Func<IFlatFileInformation, bool> Expression { get; private set; }

        private static bool IsFileOlderThanFiveDays(IFlatFileInformation information)
        {
            return information.CreationTime < DateTime.Now.AddDays(-5);
        }
    }
}
