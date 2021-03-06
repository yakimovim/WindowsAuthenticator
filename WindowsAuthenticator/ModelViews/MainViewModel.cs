﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using WindowsAuthenticator.Models;
using WindowsAuthenticator.Models.Configuration;
using WindowsAuthenticator.Properties;
using WindowsAuthenticator.Views;
using WindowsAuthenticator.Views.Support;

namespace WindowsAuthenticator.ModelViews
{
    internal class MainViewModel : BaseViewModel, IDisposable
    {
        private readonly Timer _timer;

        private readonly ObservableCollection<ItemViewModel> _items = new ObservableCollection<ItemViewModel>();
        private double _time;
        private long _count;

        public Dispatcher Dispatcher { get; set; }

        public Window OwnerWindow { get; set; }

        public MainViewModel()
        {
            _count = CurrentTimeProvider.GetCurrentCounter();
            _time = CurrentTimeProvider.Period - CurrentTimeProvider.GetCurrentTime();

            ConfigurationStorage.Section.Items.OfType<AuthenticationItem>().ToList().ForEach(i => _items.Add(new ItemViewModel(i)));
            _items.ToList().ForEach(i => i.UpdateCode(_count));

            _timer = new Timer(DoOnEachTimerTick, null, TimeSpan.FromMilliseconds(50), TimeSpan.FromMilliseconds(50));
        }

        private void DoOnEachTimerTick(object state)
        {
            try
            {
                Dispatcher.CurrentDispatcher.Invoke(() =>
                {
                    Time = CurrentTimeProvider.Period - CurrentTimeProvider.GetCurrentTime();

                    var count = CurrentTimeProvider.GetCurrentCounter();
                    if (_count != count)
                    {
                        _count = count;
                        _items.ToList().ForEach(i => i.UpdateCode(_count));
                    }
                });
            }
            catch
            {
            }
        }

        public ObservableCollection<ItemViewModel> Items
        {
            [DebuggerStepThrough]
            get { return _items; }
        }

        public double Time
        {
            [DebuggerStepThrough]
            get { return _time; }
            set
            {
                if (_time.Equals(value) == false)
                {
                    _time = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand AddItem
        {
            get
            {
                return new DelegateCommand(arg =>
                {
                    var itemViewModel = new AddItemViewModel();
                    var dialog = new AddItemWindow { Owner = OwnerWindow, DataContext = itemViewModel };
                    dialog.ValidateTextBoxes();
                    
                    if (dialog.ShowDialog() == true)
                    {
                        var authenticationItem = new AuthenticationItem
                        {
                            Title = itemViewModel.Title,
                            Secret = itemViewModel.Secret
                        };

                        ConfigurationStorage.Section.Items.Add(authenticationItem);

                        ConfigurationStorage.Configuration.Save();

                        var viewModel = new ItemViewModel(authenticationItem);
                        viewModel.UpdateCode(_count);
                        _items.Add(viewModel);
                    }
                });
            }
        }

        public ICommand EditItem
        {
            get
            {
                return new DelegateCommand(arg =>
                {
                    var itemViewModel = (ItemViewModel)arg;

                    var editItemViewModel = new EditItemViewModel { Title = itemViewModel.Title };

                    var dialog = new EditItemWindow { Owner = OwnerWindow, DataContext = editItemViewModel };
                    dialog.ValidateTextBoxes();

                    if (dialog.ShowDialog() == true)
                    {
                        itemViewModel.Title = editItemViewModel.Title;

                        ConfigurationStorage.Configuration.Save();
                    }
                });
            }
        }

        public ICommand DeleteItem
        {
            get
            {
                return new DelegateCommand(arg =>
                {
                    var itemViewModel = (ItemViewModel)arg;

                    if (MessageBox.Show(
                        OwnerWindow,
                        string.Format(Resources.DeleteConfirmation, itemViewModel.Title),
                        Resources.DeleteConfirmationTitle,
                        MessageBoxButton.YesNo, 
                        MessageBoxImage.Question) ==
                        MessageBoxResult.Yes)
                    {
                        _items.Remove(itemViewModel);

                        ConfigurationStorage.Section.Items.Remove(itemViewModel.Title);
                        ConfigurationStorage.Configuration.Save();
                    }
                });
            }
        }

        public ICommand CopyCode
        {
            get
            {
                return new DelegateCommand(arg =>
                {
                    var itemViewModel = (ItemViewModel)arg;

                    Clipboard.SetText(itemViewModel.Code);
                });
            }
        }

        public void Dispose()
        {
            _timer.Dispose();
        }
    }
}
