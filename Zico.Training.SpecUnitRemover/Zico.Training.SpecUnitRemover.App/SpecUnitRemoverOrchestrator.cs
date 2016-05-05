using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zico.Training.SpecUnitRemover.App
{
    public class SpecUnitRemoverOrchestrator
    {
        private FilesProvider _filesProvider;
        private SpecUnitFileRemover _fileRemover;

        public SpecUnitRemoverOrchestrator(FilesProvider filesProvider, SpecUnitFileRemover fileRemover)
        {
            _filesProvider = filesProvider;
            _fileRemover = fileRemover;
        }

        public void RemoveSpecUnitFromTests(string solutionFolder)
        {
            var filePaths = _filesProvider.GetFilePaths(solutionFolder);
            foreach (var file in filePaths)
                _fileRemover.Remove(file);
        }
    }
}
