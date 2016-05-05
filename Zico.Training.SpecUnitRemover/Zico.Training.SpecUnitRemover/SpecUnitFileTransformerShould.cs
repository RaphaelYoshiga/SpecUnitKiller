using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Zico.Training.SpecUnitRemover.App;
using Moq;

namespace Zico.Training.SpecUnitRemover
{
    [TestFixture]
    public class SpecUnitFileTransformerShould
    {
        private Mock<IContentRemover> _contentRemover;
        private Mock<FileReader> _fileReader;
        private Mock<FileWriter> _fileWriter;
        private SpecUnitFileTransformer _fileTransformer;

        [SetUp]
        public void BeforeEachTest()
        {
            _fileReader = new Mock<FileReader>();
            _contentRemover = new Mock<IContentRemover>();
            _fileWriter = new Mock<FileWriter>();
            _fileTransformer = new SpecUnitFileTransformer(_fileReader.Object, _contentRemover.Object, _fileWriter.Object);
        }

        [Test]
        public void CallReader()
        {
            string filePath = Guid.NewGuid().ToString();

            _fileTransformer.Transform(filePath);

            _fileReader.Verify(p => p.ReadContent(filePath), Times.Once);
        }


        [Test]
        public void CallContentTransformer()
        {
            string fileContent = Guid.NewGuid().ToString();
            _fileReader.Setup(p => p.ReadContent(It.IsAny<string>()))
                .Returns(fileContent);

            _fileTransformer.Transform("");

            _contentRemover.Verify(p => p.Remove(fileContent));
        }


        [Test]
        public void CallFileWriter()
        {
            string filePath = Guid.NewGuid().ToString();
            string transformedFileContent = Guid.NewGuid().ToString();
            _contentRemover.Setup(p => p.Remove(It.IsAny<string>()))
                .Returns(transformedFileContent);

            _fileTransformer.Transform(filePath);

            _fileWriter.Verify(p => p.Write(filePath, transformedFileContent), Times.Once);
        }
    }
}
