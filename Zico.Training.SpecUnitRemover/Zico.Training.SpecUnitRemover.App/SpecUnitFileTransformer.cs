namespace Zico.Training.SpecUnitRemover.App
{
    public interface FileTransformer
    {
        void Transform(string filePath);
    }

    public class SpecUnitFileTransformer : FileTransformer
    {
        private FileReader _reader;
        private FileWriter _writer;
        private IContentRemover _contentTransformer;

        public SpecUnitFileTransformer(FileReader reader, IContentRemover contentTransformer, FileWriter writer)
        {
            _reader = reader;
            _contentTransformer = contentTransformer;
            _writer = writer;
        }

        public void Transform(string filePath)
        {
            var content = _reader.ReadContent(filePath);
            var transformedContent = _contentTransformer.Remove(content);
            _writer.Write(filePath, transformedContent);
        }
    }
}