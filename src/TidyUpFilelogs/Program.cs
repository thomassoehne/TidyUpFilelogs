using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.Practices.Unity;
using TidyUpFilelogs.FileScanning;
using TidyUpFilelogs.FileScanning.Interfaces;

namespace TidyUpFilelogs
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                MainInternal(args);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            if (Debugger.IsAttached)
            {
                Console.WriteLine("Fertig - Beenden mit Tastendruck.");
                Console.ReadLine();
            }
        }

        private static void MainInternal(string[] args)
        {
            var workingFolder = GetWorkingfolder(args);
            var unityContainer = CreateUnityContainer();
            var fileScanner = unityContainer.Resolve<IFileScanner>();

            GetFilesAndWork(fileScanner, workingFolder);
        }

        private static void GetFilesAndWork(IFileScanner fileScanner, string workingFolder)
        {
            var filenames = fileScanner.GetFilesFiltered(workingFolder);

            filenames.ToList().ForEach(Console.WriteLine);
        }

        private static UnityContainer CreateUnityContainer()
        {
            var unityContainer = new UnityContainer();
            unityContainer.RegisterType<IFileScanner, FileScanner>();
            unityContainer.RegisterType<IFileInfoProvider, FileInfoProvider>();
            return unityContainer;
        }

        private static string GetWorkingfolder(string[] args)
        {
            CheckForExpectedArguments(args);
            var workingFolder = args.First();
            if (!Directory.Exists(workingFolder)) throw new Exception("Workingfolder cannot be found.");
            return workingFolder;
        }

        private static void CheckForExpectedArguments(string[] args)
        {
            if (args == null) throw new ArgumentNullException(nameof(args));
            if (args.Length == 0) throw new Exception("You must specify a working folder.\r\nTidyUpFileLogs <workingFolder>");
        }
    }
}
