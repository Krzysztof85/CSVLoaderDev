using Microsoft.Win32;

namespace CSVLoader
{
    public class OpenFileService : IOpenFileService
    {
        public string FileName { get; private set; }

        public bool OpenFileDialog()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".csv";
            dlg.Filter = "CSV Files (*.csv)|*.csv";
            var result = dlg.ShowDialog();
            if (result.HasValue && result.Value)
            {
                FileName = dlg.FileName;
                return true;
            }
            return false;
        }
    }
}
