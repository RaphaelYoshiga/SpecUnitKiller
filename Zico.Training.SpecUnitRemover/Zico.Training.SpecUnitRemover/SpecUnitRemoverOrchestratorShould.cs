using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Zico.Training.SpecUnitRemover.App;

namespace Zico.Training.SpecUnitRemover
{
    [TestFixture]
    public class SpecUnitRemoverOrchestratorShould
    {
        private Mock<FilesProvider> _filesProviderMock;
        private SpecUnitRemoverOrchestrator _orchestrator;
        private Mock<SpecUnitFileRemover> _fileRemoverMock;

        [SetUp]
        public void BeforeEachTest()
        {
            _filesProviderMock = new Mock<FilesProvider>();
            _fileRemoverMock = new Mock<SpecUnitFileRemover>();
            _orchestrator = new SpecUnitRemoverOrchestrator(_filesProviderMock.Object, _fileRemoverMock.Object);
        }

        [Test]
        public void CallFilesProvider()
        {
            string directory = Guid.NewGuid().ToString();

            _orchestrator.RemoveSpecUnitFromTests(directory);

            _filesProviderMock.Verify(p => p.GetFilePaths(directory), Times.Once);
        }


        [Test]
        public void CallRemoverForEachFile()
        {
            string[] filePaths = {
                "test",
                "AnotherFilePath",
                "GuessAnotherFilePath"
            };
            _filesProviderMock.Setup(p => p.GetFilePaths(It.IsAny<string>()))
                .Returns(filePaths);

            _orchestrator.RemoveSpecUnitFromTests("");

            foreach (var filePath in filePaths)
                _fileRemoverMock.Verify(p => p.Remove(filePath), Times.Once);
        }

    }
}
