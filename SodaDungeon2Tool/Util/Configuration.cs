using System;
using System.IO;

namespace SodaDungeon2Tool.Util
{
    /// <summary>
    /// Store the current User Configuration
    /// </summary>
    public class Configuration
    {
        public int sleepTimerInSeconds = 60;
        public bool notifyOnFinish = true;
        public bool ShutdownOnFinish = false;
        public int numberOfNotifications = 3;

        /// <summary>
        /// Constructor, that loads a local config file and their values, if it exists.
        /// </summary>
        public Configuration()
        {
            if (File.Exists("config.txt"))
            {
                string[] config = File.ReadAllText("config.txt").Split(',');
                if (config.Length != 3)
                    return;
                try
                {
                    sleepTimerInSeconds = int.Parse(config[0]);
                }
                catch (FormatException)
                {
                }
                try
                {
                    notifyOnFinish = bool.Parse(config[1]);
                }
                catch (FormatException)
                {
                }
                try
                {
                    numberOfNotifications = int.Parse(config[2]);
                }
                catch (FormatException)
                {
                }
            }
            Save();
        }

        /// <summary>
        /// Save the current configuration in a simple text-file
        /// </summary>
        public void Save()
        {
            File.WriteAllText("config.txt", $"{sleepTimerInSeconds},{notifyOnFinish},{numberOfNotifications}");
        }

        /// <summary>
        /// Output String for the Console Application
        /// </summary>
        /// <returns></returns>
        override
        public string ToString()
        {
            string notifyCol = (notifyOnFinish) ? "Green" : "Red";
            return $"Interval in Seconds between each Exit-Button check: #Col:Green#{sleepTimerInSeconds}#\n\tNotify On Finish: #Col:{notifyCol}#{notifyOnFinish}#\n\tNumber of Notifications: #Col:Green#{numberOfNotifications}#";
        }
    }
}