using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WindowsAuthenticator.ModelViews
{
    internal abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void RefreshBindings()
        {
            var type = GetType();

            foreach (var propertyInfo in type.GetProperties())
            {
                if (propertyInfo.CanRead)
                {
                    OnPropertyChanged(propertyInfo.Name);
                }
            }
        }
    }
}
