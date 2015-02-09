using System;
using System.Diagnostics;
using System.Text;
using WindowsAuthenticator.Models;
using WindowsAuthenticator.Models.Configuration;

namespace WindowsAuthenticator.ModelViews
{
    internal class ItemViewModel : BaseViewModel
    {
        private readonly AuthenticationItem _item;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string _code;

        [DebuggerStepThrough]
        public ItemViewModel(AuthenticationItem item)
        {
            if (item == null) throw new ArgumentNullException("item");
            _item = item;
        }

        public string Title
        {
            [DebuggerStepThrough]
            get { return _item.Title; }
            set
            {
                if (_item.Title != value)
                {
                    _item.Title = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Code
        {
            [DebuggerStepThrough]
            get { return _code; }
            set
            {
                if (_code != value)
                {
                    _code = value;
                    OnPropertyChanged();
                }
            }
        }

        public void UpdateCode(long count)
        {
            var secret = Encoding.ASCII.GetString(Base32.Decode(_item.Secret));

            Code = CounterBasedOneTimePassword.GeneratePassword(secret, count);
        }
    }
}