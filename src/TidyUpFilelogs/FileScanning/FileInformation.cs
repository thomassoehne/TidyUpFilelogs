using System;
using TidyUpFilelogs.FileScanning.Interfaces;

namespace TidyUpFilelogs.FileScanning
{
    public class FlatFileInformation : IFlatFileInformation
    {
        public FlatFileInformation(string fullName, DateTime creationTime)
        {
            this.FullName = fullName;
            this.CreationTime = creationTime;
        }

        public DateTime CreationTime { get; }

        public string FullName { get; }
    }
}
