using SodaDungeon2Tool.Util;
using System;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace SodaDungeon2Tool.ViewModel
{
    public class SettingsViewModel : ObservableObject
    {
        private Configuration Config;
        private readonly Regex CheckIntervalRegex = new Regex("[^0-9]+");
        public ICommand ChangeToMainView { get; private set; }

        public int CheckIntervalText
        {
            get { return Config.sleepTimerInSeconds; }
            set
            {
                try
                {
                    Config.sleepTimerInSeconds = value;
                    Config.Save();
                }
                catch (FormatException ex)
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
                Config.Save();
            }
        }

        public bool ShutdownOnFinish
        {
            get { return Config.ShutdownOnFinish; }
            set
            {
                OnPropertyChanged(ref Config.ShutdownOnFinish, value);
                Config.Save();
            }
        }

        public SettingsViewModel(ICommand ChangeToMainView, Configuration Config)
        {
            this.ChangeToMainView = ChangeToMainView;
            this.Config = Config;
        }
    }
}