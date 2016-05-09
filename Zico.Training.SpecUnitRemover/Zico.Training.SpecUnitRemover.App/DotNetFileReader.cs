using System.IO;

namespace Zico.Training.SpecUnitRemover.App
{
    public interface FileReader
    {
        string ReadContent(string filePath);
    }

    public class DotNetFileReader : FileReader
    {
        public string ReadContent(string filePath)
        {
            return File.ReadAllText(filePath);
        }
    }
}
