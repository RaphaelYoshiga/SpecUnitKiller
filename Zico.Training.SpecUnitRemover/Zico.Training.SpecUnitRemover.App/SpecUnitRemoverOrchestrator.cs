namespace Zico.Training.SpecUnitRemover.App
{
    public class SpecUnitRemoverOrchestrator
    {
        private FilesProvider _filesProvider;
        private FileTransformer _fileTransformer;

        public SpecUnitRemoverOrchestrator(FilesProvider filesProvider, FileTransformer fileTransformer)
        {
            _filesProvider = filesProvider;
            _fileTransformer = fileTransformer;
        }

        public void RemoveSpecUnitFromTests(string solutionFolder)
        {
            var filePaths = _filesProvider.GetFilePaths(solutionFolder);
            foreach (var file in filePaths)
                _fileTransformer.Transform(file);
        }
    }
}
