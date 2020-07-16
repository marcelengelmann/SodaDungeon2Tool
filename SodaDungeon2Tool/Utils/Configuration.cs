using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaDungeon2Tool.Utils
{
    /// <summary>
    /// Store the current User Configuration
    /// </summary>
    public class Configuration
    {
        public int sleepTimerInSeconds = 60;
        public bool notifyOnFinish = true;
        public int numberOfNotifications = 3;

        /// <summary>
        /// Constructor, that loads a local config file and their values, if it exists.
        /// </summary>
        public Configuration()
        {
            if (File.Exists("config.txt"))
            {
                string[] config = File.ReadAllText("config.txt").Split(',');
                if(config.Length != 3)
                {
                    WriteToConsole.Error("Could not Read config.txt File. Using default Values!");
                    return;
                }

                try
                {
                    sleepTimerInSeconds = int.Parse(config[0]);
                }
                catch(FormatException ex)
                {
                    WriteToConsole.Error("Could not Read SleepTimer. Using default Value!");
                }
                try
                {
                    notifyOnFinish = bool.Parse(config[1]);
                }
                catch (FormatException ex)
                {
                    WriteToConsole.Error("Could not Read notifyOnFinish. Using default Value!");
                }
                try
                {
                    numberOfNotifications = int.Parse(config[2]);
                }
                catch (FormatException ex)
                {
                    WriteToConsole.Error("Could not Read numberOfNotifications. Using default Value!");
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
