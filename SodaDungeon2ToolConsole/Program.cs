using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SodaDungeon2Tool.Utils;

namespace SodaDungeon2Tool
{
    public class Program
    {
        public static IntPtr sodaGame;
        public static bool shutDownOnFinish = false;

        /// <summary>
        /// The Entry function for the Console Application
        /// </summary>
        /// <param name="args">Ignored</param>
        static void Main(string[] args)
        {
            
            sodaGame = Logic.getGameHandler();
            Configuration config = new Configuration();

            // The Main Menu
            while (true)
            {
                WriteToConsole.Text("What would you like to Do? Type the coresponding Number.");
                string shutdownStatus = (shutDownOnFinish) ? "#Col:Green#Enabled#" : "#Col:Red#Disabled#";
                WriteToConsole.Text($"\t#Col:Green#1# : Start\n\t#Col:Green#2# : change your Configuration\n\t#Col:Green#3# : Change option 'Shutdown on finish' currently set to: {shutdownStatus}\n\t#Col:Green#4# : Exit");
                string userInput = Console.ReadLine();
                if (userInput == "1")
                {
                    RunTool(config);
                }
                else if (userInput == "2")
                {
                    ChangeConfiguration(config);
                }
                else if(userInput == "3")
                {
                    shutDownOnFinish = !shutDownOnFinish;
                }
                else if (userInput == "4")
                {
                    break;
                }
                else
                {
                    WriteToConsole.Error("Could not Read Input!");
                    continue;
                }
                Console.Clear();
            }
        }

        // The Settings Menu
        private static void ChangeConfiguration(Configuration config)
        {
            while (true)
            {
                WriteToConsole.Text("Current Settings:\n\t" + config.ToString());
                WriteToConsole.Text("Which Setting would you like to change?");
                WriteToConsole.Text("\t#Col:Green#1# : Check Interval\n\t#Col:Green#2# : Notify on finish\n\t#Col:Green#3# : Number of Notifications\n\t#Col:Green#4# : Back to Main Menu");
                string userInput = Console.ReadLine();
                if (userInput == "1")
                {
                    Console.WriteLine("Please Enter the Number of Seconds:");
                    try
                    {
                        config.sleepTimerInSeconds = int.Parse(Console.ReadLine());
                    }
                    catch(FormatException ex)
                    {
                        WriteToConsole.Error("Could not Read Input!");
                        continue;
                    }
                }
                else if (userInput == "2")
                {
                    config.notifyOnFinish = !config.notifyOnFinish;
                }
                else if (userInput == "3")
                {
                    Console.WriteLine("Please Enter the Number of Notifications:");
                    try
                    {
                        config.numberOfNotifications = int.Parse(Console.ReadLine());
                    }
                    catch (FormatException ex)
                    {
                        WriteToConsole.Error("Could not Read Input!");
                        continue;
                    }
                }
                else if (userInput == "4")
                {
                    config.Save();
                    return;
                }
                else
                {
                    WriteToConsole.Error("Could not Read Input!"); ;
                    continue;
                }
                Console.Clear();
            }
        }

        //Start the tool and check for the Exit Button
        private static void RunTool(Configuration config)
        {
            while (true)
            {
                Bitmap image = Logic.TakeScreenshot(sodaGame);
                string time = DateTime.Now.ToString("t");
                if (Logic.HasExitButton(image))
                {
                    WriteToConsole.Text($"{time} - The Run #Col:Green#ended!#");
                    if (config.notifyOnFinish == true)
                    {
                        for(int i = 0; i < config.numberOfNotifications; i++)
                        {
                            Console.Beep();
                            Thread.Sleep(300);
                        }
                    }
                    if (shutDownOnFinish == true)
                    {
                        Process.Start("shutdown", "/s /t 0");
                    }
                    break;

                }
                WriteToConsole.Text($"{time} - The Run #Col:Red#did not end yet!#");
                Thread.Sleep(config.sleepTimerInSeconds * 1000);
            }
            WriteToConsole.Text("#Col:Green#Done!# Press any Key to get Back to the Main Menu.");
            Console.ReadKey();
        }
    }
}
