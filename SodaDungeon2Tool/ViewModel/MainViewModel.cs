using SodaDungeon2Tool.Util;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace SodaDungeon2Tool.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        public ICommand ChangeToSettingsView { get; private set; }
        public ICommand StartRunCommand { get; private set; }
        public ICommand CheckGameRunningCommand { get; private set; }
        private readonly Configuration Config;
        private string _timerText;
        private string _startStopButtonText;
        private bool _showGameNotFoundError;
        private CancellationTokenSource StopTimer;
        private ImageSource _screenshotImage;

        public ImageSource ScreenshotImage
        {
            get { return _screenshotImage; }
            set { OnPropertyChanged(ref _screenshotImage, value); }
        }

        public string TimerText
        {
            get { return _timerText; }
            set { OnPropertyChanged(ref _timerText, value); }
        }

        public string StartStopButtonText
        {
            get { return _startStopButtonText; }
            set { OnPropertyChanged(ref _startStopButtonText, value); }
        }

        public bool ShowGameNotFoundError
        {
            get { return _showGameNotFoundError; }
            set { OnPropertyChanged(ref _showGameNotFoundError, value); }
        }

        public MainViewModel(ICommand ChangeToSettingsView, Configuration Config)
        {
            Task.Run(async () => await CheckForUpdates.InformLatestRelease());
            this.ChangeToSettingsView = ChangeToSettingsView;
            StartRunCommand = new RelayCommand(StartStopTimer);
            CheckGameRunningCommand = new RelayCommand(CheckGameRunningButton);
            this.Config = Config;
            TimerText = "00:00:00";
            StartStopButtonText = "Start";
            CheckGameRunning();
        }

        /// <summary>
        /// Either Starts the new Timer or Cancles the currently running one
        /// </summary>
        private async void StartStopTimer()
        {
            if (StopTimer != null)
            {
                StopTimer.Cancel();
                StopTimer = null;
                StartStopButtonText = "Start";
                TimerText = "00:00:00";
            }
            else
            {
                StartStopButtonText = "Stop";
                StopTimer = new CancellationTokenSource();
                await TimerTick(StopTimer.Token);
            }
        }

        /// <summary>
        /// Function that will execute in a 1 second interval until it is canceled
        /// </summary>
        /// <param name="CT">CancellationToken that can be canceled</param>
        /// <returns></returns>
        private async Task TimerTick(CancellationToken CT)
        {
            TimeSpan checkInterval = TimeSpan.FromSeconds(Config.sleepTimerInSeconds);
            ExitCheck();
            while (true)
            {
                checkInterval = checkInterval.Add(TimeSpan.FromSeconds(-1));
                TimerText = checkInterval.ToString("c");
                if (checkInterval.TotalSeconds <= 0)
                {
                    ExitCheck();
                    checkInterval = TimeSpan.FromSeconds(Config.sleepTimerInSeconds);
                }
                Task delay = Task.Delay(1000, CT);
                try
                {
                    await delay;
                }
                catch (TaskCanceledException)
                {
                    return;
                }
            }
        }

        /// <summary>
        /// Checks whether the ExitButton has been found and executes Actions setted inside the Settings
        /// </summary>
        private void ExitCheck()
        {
            
            IntPtr? sodaGame = CheckGameRunning();
            if (sodaGame == null)
                return;

            Bitmap Image = Logic.TakeScreenshot((IntPtr)sodaGame);
            ScreenshotImage = ScreenCapture.ImageSourceFromBitmap(Image);
            if (Logic.HasExitButton(Image))
            {
                StartStopTimer();
                if (Config.notifyOnFinish == true)
                {
                    Task.Run(() => NotifyOnFinish());
                }
                if (Config.ShutdownOnFinish == true)
                {
                    Process.Start("shutdown", "/s /t 0");
                }
            }
        }

        /// <summary>
        /// Checks whether the Soda Dungeon 2 Game is currently running
        /// </summary>
        /// <returns>The handler of the game process or null if the game is not running</returns>
        private IntPtr? CheckGameRunning()
        {
            IntPtr? sodaGame = Logic.GetGameHandler();
            if (sodaGame == null)
            {
                ShowGameNotFoundError = true;
                if (StopTimer != null)
                    StartStopTimer(); // cancels timer and sets the startbutton text correctly
            }
            else
                ShowGameNotFoundError = false;
            return sodaGame;
        }

        //Helperfuntion to allow usage for an ICommand Binding with the View
        private void CheckGameRunningButton()
        {
            CheckGameRunning();
        }

        private void NotifyOnFinish()
        {
            for (int i = 0; i < Config.numberOfNotifications; i++){
                Thread.Sleep(300);
                Console.Beep();
            }
        }

    }
}