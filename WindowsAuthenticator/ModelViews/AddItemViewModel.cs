using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace WindowsAuthenticator.ModelViews
{
    internal class AddItemViewModel : BaseViewModel, IDataErrorInfo
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string _title;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string _secret;

        private HashSet<string> _existingTitles;

        public AddItemViewModel(IEnumerable<string> existingTitles)
        {
            _existingTitles = new HashSet<string>(existingTitles, StringComparer.OrdinalIgnoreCase);
        }

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

        public string Secret
        {
            [DebuggerStepThrough]
            get { return _secret; }
            set
            {
                if (_secret != value)
                {
                    _secret = value;
                    OnPropertyChanged();
                }
            }
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Title")
                {
                    if (!string.IsNullOrEmpty(Title) && _existingTitles.Contains(Title))
                    {
                        return "This title is already ocupied";
                    }
                }
                return null;
            }
        }

        public string Error
        {
            get { return null; }
        }
    }
}