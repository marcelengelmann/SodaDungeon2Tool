using SodaDungeon2Tool.Model;
using SodaDungeon2Tool.Util;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace SodaDungeon2Tool.ViewModel
{
    public class ApplicationViewModel : ObservableObject
    {
        private object _currentView;
        private MainViewModel _mainVM;
        private SettingsViewModel _settingsVM;
        public ICommand ChangeToSettingsViewCommand { get; private set; }
        public ICommand ChangeToMainViewCommand { get; private set; }

        public object CurrentView
        {
            get { return _currentView; }
            set { OnPropertyChanged(ref _currentView, value); }
        }

        public MainViewModel MainVM
        {
            get { return _mainVM; }
            set { OnPropertyChanged(ref _mainVM, value); }
        }

        public SettingsViewModel SettingsVM
        {
            get { return _settingsVM; }
            set { OnPropertyChanged(ref _settingsVM, value); }
        }

        public ApplicationViewModel()
        {
            ResizeWindowAsClientArea();

            Configuration Config = LocalDataService.LoadConfiguration();
            ChangeToSettingsViewCommand = new RelayCommand(ChangeToSettingsView);
            ChangeToMainViewCommand = new RelayCommand(ChangeToMainView);
            MainVM = new MainViewModel(ChangeToSettingsViewCommand, Config);
            SettingsVM = new SettingsViewModel(ChangeToMainViewCommand, Config);
            CurrentView = MainVM;
        }

        private void ResizeWindowAsClientArea()
        {
            var horizontalBorderHeight = SystemParameters.ResizeFrameHorizontalBorderHeight;
            var verticalBorderWidth = SystemParameters.ResizeFrameVerticalBorderWidth;
            var captionHeight = SystemParameters.CaptionHeight;
            Application.Current.MainWindow.Width = Application.Current.MainWindow.Width + 2 * verticalBorderWidth;
            Application.Current.MainWindow.Height = Application.Current.MainWindow.Height + captionHeight + 2 * horizontalBorderHeight;
        }

        private void ChangeToMainView()
        {
            CurrentView = MainVM;
        }

        private void ChangeToSettingsView()
        {
            CurrentView = SettingsVM;
        }
    }
}