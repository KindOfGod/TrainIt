using TrainIt.Interfaces;
using TrainIt.ViewModel;

namespace TrainIt.Dialogs.DialogViewModels
{
    public class ErrorDialogViewModel : BaseViewModel, IDialog
    {
        #region Fields

        private string _message;
        private string _header;

        #endregion

        #region Properties

        public string Message
        {
            get => _message;
            set
            {
                if (_message == value)
                    return;

                _message = value;
                OnPropertyChange();
            }
        }

        public string Header
        {
            get => _header;
            set
            {
                if (_header == value)
                    return;

                _header = value;
                OnPropertyChange();
            }
        }

        #endregion

        #region Constructors

        public ErrorDialogViewModel(string header, string message)
        {
            Header = header;
            Message = message;
        }

        #endregion
    }
}