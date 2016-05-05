using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zico.Training.SpecUnitRemover.App
{
    public interface FilesProvider
    {
        string[] GetFilePaths(string solutionFolder);
    }

    public class CSharpFilesProvider : FilesProvider
    {
        public string[] GetFilePaths(string solutionFolder)
        {
            return Directory.GetFiles(solutionFolder, "*.cs", SearchOption.AllDirectories);
        }
    }
}
