using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
