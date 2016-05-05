namespace Zico.Training.SpecUnitRemover.App
{
    public interface FilesProvider
    {
        string[] GetFilePaths(string solutionFolder);
    }
}