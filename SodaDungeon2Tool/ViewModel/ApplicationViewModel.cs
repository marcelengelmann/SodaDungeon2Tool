using SodaDungeon2Tool.Util;
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
            Configuration Config = new Configuration();
            ChangeToSettingsViewCommand = new RelayCommand(ChangeToSettingsView);
            ChangeToMainViewCommand = new RelayCommand(ChangeToMainView);
            MainVM = new MainViewModel(ChangeToSettingsViewCommand, Config);
            SettingsVM = new SettingsViewModel(ChangeToMainViewCommand, Config);
            CurrentView = MainVM;
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