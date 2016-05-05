using System.IO;
using System.Reflection;
using NUnit.Framework;

namespace Zico.Training.SpecUnitRemover
{
    [TestFixture]
    public class SpecUnitRemoverShould
    {
        private const string TEMP_FOLDER = "Temp";
        private const string TEST_CASES_FOLDER = "TestCases";
        private Remover _specUnitRemover;

        [SetUp]
        public void BeforeEachTest()
        {
            _specUnitRemover = new Remover();
        }

        [TestCase("MatfloAdapterShould.txt", "ExpectedMatfloAdapterShould.txt")]
        public void RemoveSpecUnitUsage(string fileName, string expectedFileName)
        {
            string binFolder = GetBinFolderPath();
            string destinationFile = Path.Combine(binFolder, TEMP_FOLDER, fileName);
            MoveFileToTempFolder(binFolder, destinationFile, fileName);

            _specUnitRemover.Remove(destinationFile);

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
