using SodaDungeon2Tool.Model;
using SodaDungeon2Tool.Util;
using System;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace SodaDungeon2Tool.ViewModel
{
    public class SettingsViewModel : ObservableObject
    {
        private Configuration Config;
        public ICommand ChangeToMainView { get; private set; }
        public ICommand PlayTestSoundCommand { get; private set; }

        public int CheckIntervalText
        {
            get { return Config.sleepTimerInSeconds; }
            set
            {
                try
                {
                    Config.sleepTimerInSeconds = value;
                    LocalDataService.SaveConfiguration(Config);
                }
                catch (FormatException)
                {
                    return;
                }
                OnPropertyChanged(ref Config.sleepTimerInSeconds, value);
            }
        }


        public bool NotificationOnFinish
        {
            get { return Config.notifyOnFinish; }
            set
            {
                OnPropertyChanged(ref Config.notifyOnFinish, value);
                LocalDataService.SaveConfiguration(Config);
            }
        }

        public int NumberOfNotifications
        {
            get { return Config.numberOfNotifications; }
            set
            {
                try
                {
                    Config.numberOfNotifications = value;
                    LocalDataService.SaveConfiguration(Config);
                }
                catch (FormatException)
                {
                    return;
                }
                OnPropertyChanged(ref Config.numberOfNotifications, value);
            }
        }

        public bool ShutdownOnFinish
        {
            get { return Config.shutdownOnFinish; }
            set
            {
                OnPropertyChanged(ref Config.shutdownOnFinish, value);
                LocalDataService.SaveConfiguration(Config);
            }
        }
        
        public bool SaveLastScreenshot
        {
            get { return Config.saveLastScreenshot; }
            set
            {
                OnPropertyChanged(ref Config.saveLastScreenshot, value);
                LocalDataService.SaveConfiguration(Config);
            }
        }

        public string NotificationSoundFilePath
        {
            get { return Config.notificationSoundFileLocation;}
            set
            {
                OnPropertyChanged(ref Config.notificationSoundFileLocation, value);
                LocalDataService.SaveConfiguration(Config);
            }
        }

        public SettingsViewModel(ICommand ChangeToMainView, Configuration Config)
        {
            this.ChangeToMainView = ChangeToMainView;
            this.Config = Config;
            this.PlayTestSoundCommand = new RelayCommand(PlayTestSound);
        }

        public void PlayTestSound()
        {
            Logic.NotifyOnFinish(Config.notificationSoundFileLocation, 1, Config.notificationSoundVolume);
        }
    }
}