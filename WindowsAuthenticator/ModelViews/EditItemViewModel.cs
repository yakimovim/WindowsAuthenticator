using System.Diagnostics;

namespace WindowsAuthenticator.ModelViews
{
    internal class EditItemViewModel : BaseViewModel
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string _title;

        public string Title
        {
            [DebuggerStepThrough]
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}