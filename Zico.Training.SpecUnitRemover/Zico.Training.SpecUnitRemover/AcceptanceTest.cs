using System.IO;
using System.Reflection;
using NUnit.Framework;
using Zico.Training.SpecUnitRemover.App;

namespace Zico.Training.SpecUnitRemover
{
    [TestFixture]
    public class AcceptanceTest
    {
        private const string TEMP_FOLDER = "Temp";
        private const string TEST_CASES_FOLDER = "TestCases";
        private FileTransformer _specUnitRemover;

        [SetUp]
        public void BeforeEachTest()
        {
            _specUnitRemover = new SpecUnitFileTransformer(new DotNetFileReader(), new ContentRemover(), new DotNetFileWriter());
        }

        [TestCase("MatfloAdapterShould.txt", "ExpectedMatfloAdapterShould.txt")]
        public void RemoveSpecUnitUsage(string fileName, string expectedFileName)
        {
            string binFolder = GetBinFolderPath();
            string destinationFile = Path.Combine(binFolder, TEMP_FOLDER, fileName);
            MoveFileToTempFolder(binFolder, destinationFile, fileName);

            _specUnitRemover.Transform(destinationFile);

            var actualFileContent = File.ReadAllText(destinationFile);
            var expectedContent = File.ReadAllText(Path.Combine(binFolder, TEST_CASES_FOLDER, expectedFileName));
            Assert.That(actualFileContent, Is.EqualTo(expectedContent));
        }

        private static string GetBinFolderPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "");
        }

        private static void MoveFileToTempFolder(string binFolder, string destinationFile, string fileName)
        {
            Directory.CreateDirectory(Path.Combine(binFolder, TEMP_FOLDER));
            string filePath = Path.Combine(binFolder, TEST_CASES_FOLDER, fileName);
            File.Copy(filePath, destinationFile, true);
        }
    }
}
