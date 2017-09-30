using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using TidyUpFilelogs.FileScanning;
using TidyUpFilelogs.FileScanning.Interfaces;

namespace ClassLibrary1
{
    [TestFixture]
    public class FileScanningTests
    {
        private UnityContainer unityContainer;
        private IFileScanner filescanner;
        private Mock<IFileInfoProvider> fileInfoProviderMock;
        private Mock<IFileScannerFilter> fileScannerFilterMock;
        private Func<IFlatFileInformation, bool> alwaysTrueExpression;

        [SetUp]
        public void SetupFixture()
        {
            this.fileScannerFilterMock = new Mock<IFileScannerFilter>();
            alwaysTrueExpression = fi => true;
            this.fileScannerFilterMock.Setup(m => m.Expression).Returns(alwaysTrueExpression);

            this.fileInfoProviderMock = new Mock<IFileInfoProvider>();
            var emptyFlatFileList = new List<IFlatFileInformation>();
            this.fileInfoProviderMock.Setup(m => m.GetFileInfos("")).Returns(emptyFlatFileList);

            this.unityContainer = new UnityContainer();
            this.unityContainer.RegisterType<IFileScanner, FileScanner>();
            this.unityContainer.RegisterInstance<IFileScannerFilter>(new FiveDaysOldFileScannerFilter());
            this.unityContainer.RegisterInstance<IFileInfoProvider>(this.fileInfoProviderMock.Object);
            
            this.filescanner = this.unityContainer.Resolve<IFileScanner>();
        }

        [Test]
        public void Call_GetFilesWithFilter_WithFilter_Works()
        {

            var filter = this.fileScannerFilterMock.Object;
            
            var result = this.filescanner.GetFilesFiltered("");

            Assert.IsNotNull(result);
        }

        [Test]
        public void Call_GetFilesWithFilter_withoutFilter_Works()
        {
            var result = this.filescanner.GetFilesFiltered("");

            Assert.IsNotNull(result);
        }

        [Test]
        public void Call_GetFilesWithFilter_withoutFilter_returnsFiles()
        {
            SetFileScannerToReturnListWith10DayOldHit();
            var result = this.filescanner.GetFilesFiltered("");

            Assert.IsTrue(result.Any());
        }

        [Test]
        public void Call_GetFilesWithFilter_withoutFilterAndANewFile_returnsnoFiles()
        {
            SetFileScannerToReturnAListWithNewHit();

            var result = this.filescanner.GetFilesFiltered("");

            Assert.IsFalse(result.Any());
        }

        private void SetFileScannerToReturnAListWithNewHit()
        {
            var listWithNewHit = new List<IFlatFileInformation> {new FlatFileInformation("aFileName", DateTime.Now.AddDays(-1))};
            this.fileInfoProviderMock.Setup(m => m.GetFileInfos("")).Returns(listWithNewHit);
            this.unityContainer.RegisterInstance<IFileInfoProvider>(this.fileInfoProviderMock.Object);

            this.filescanner = this.unityContainer.Resolve<IFileScanner>();
        }

        private void SetFileScannerToReturnListWith10DayOldHit()
        {
            var listWithOneHit =
                new List<IFlatFileInformation> {new FlatFileInformation("aFileName", DateTime.Now.AddDays(-10))};
            this.fileInfoProviderMock.Setup(m => m.GetFileInfos("")).Returns(listWithOneHit);
            this.unityContainer.RegisterInstance<IFileInfoProvider>(this.fileInfoProviderMock.Object);

            this.filescanner = this.unityContainer.Resolve<IFileScanner>();
        }
    }
}
