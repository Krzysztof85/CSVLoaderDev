using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;

namespace CSVLoader.ViewModels
{
    public class LoaderViewModel : PropertyChangedBase
    {
        private bool _resultIsVisible;
        public bool ResultIsVisible
        {
            get { return _resultIsVisible; }
            set
            {
                _resultIsVisible = value;
                NotifyOfPropertyChange(() => ResultIsVisible);
            }
        }

        private IEnumerable<InvalidRow> _invalidRows;
        public IEnumerable<InvalidRow> InvalidRows
        {
            get { return _invalidRows; }
            set
            {
                _invalidRows = value;
                NotifyOfPropertyChange(() => InvalidRows);
            }
        }

        private int _exportedRows;
        public int ExportedRows
        {
            get { return _exportedRows; }
            set
            {
                _exportedRows = value;
                NotifyOfPropertyChange(() => ExportedRows);
            }
        }

        private int _currentStep;
        public int CurrentStep
        {
            get { return _currentStep; }
            set
            {
                _currentStep = value;
                NotifyOfPropertyChange(() => CurrentStep);
            }
        }

        private IOpenFileService _openFileSerice;
        private ILoaderService _loaderService;
        private IDbWriterService _repository;

        public LoaderViewModel(ILoaderService loaderService, IDbWriterService repository, IOpenFileService openFileSerice)
        {
            _openFileSerice = openFileSerice;
            _loaderService = loaderService;
            _repository = repository;
            _repository.DataCopiedPercentage += (sender, args) => { CurrentStep = args.Percentage; };
        }

        public async void Load()
        {
            CurrentStep = 0;
            ResultIsVisible = false;
            if (_openFileSerice.OpenFileDialog())
            {
                var data = _loaderService.LoadData(_openFileSerice.FileName);
                await _repository.InsertData(data.ValidTransactions);
                ExportedRows = data.ValidTransactions.Count();
                InvalidRows = data.InvalidRows;
                ResultIsVisible = true;
            }
        }
    }
}
