using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaDungeon2Tool.Utils
{
    public static class WriteToConsole
    {
        public static void Error(string error)
        {
            ConsoleColor tmp = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ForegroundColor = tmp;
        }

        public static void Text(string text)
        {
            string[] splitted = text.Split('#');
            for(int i = 0; i < splitted.Length; i++)
            {
                if (splitted[i].Contains("Col:"))
                {
                    string getColor = splitted[i].Split(':')[1];
                    i++;
                    if(getColor == "Green"){
                        writeTextInColor(splitted[i], ConsoleColor.Green);
                    }
                    else if(getColor == "Red")
                    {
                        writeTextInColor(splitted[i], ConsoleColor.Red);
                    }
                }
                else
                {
                    Console.Write(splitted[i]);
                }
            }
            Console.WriteLine();
        }

        private static void writeTextInColor(string text, ConsoleColor col)
        {
            ConsoleColor tmp = Console.ForegroundColor;
            Console.ForegroundColor = col;
            Console.Write(text);
            Console.ForegroundColor = tmp;
        }
    }
}
