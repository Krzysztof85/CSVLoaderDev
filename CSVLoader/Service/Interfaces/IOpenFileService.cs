 
namespace CSVLoader
{
    public interface IOpenFileService
    {
        string FileName { get; }
        bool OpenFileDialog();
    }
}
