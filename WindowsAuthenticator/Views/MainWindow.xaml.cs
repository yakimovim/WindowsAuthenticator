using WindowsAuthenticator.ModelViews;

namespace WindowsAuthenticator.Views
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel
            {
                Dispatcher = Dispatcher,
                OwnerWindow = this
            };
        }
    }
}
