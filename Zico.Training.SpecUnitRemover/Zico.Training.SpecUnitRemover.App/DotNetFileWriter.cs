using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zico.Training.SpecUnitRemover.App
{
    public interface FileWriter
    {
        void Write(string filePath, string fileContent);
    }

    public class DotNetFileWriter : FileWriter
    {
        public void Write(string filePath, string fileContent)
        {
            File.WriteAllText(filePath, fileContent);
        }
    }
}
