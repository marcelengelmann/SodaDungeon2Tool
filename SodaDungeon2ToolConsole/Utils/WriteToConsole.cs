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
        /// <summary>
        /// displays an error in red in the console
        /// </summary>
        /// <param name="error">The text of the error</param>
        public static void Error(string error)
        {
            ConsoleColor tmp = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ForegroundColor = tmp;
        }
        /// <summary>
        /// displays text in the console, allowing coloured text
        /// <example>
        /// The String "#Col:Red#Hi# how are you" would be displayed with having the word "hi" in a red font colour
        /// Currently only supports the colours Red and Green
        /// </example>
        /// </summary>
        /// <param name="text">The text to be displayed</param>
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
                        WriteTextInColor(splitted[i], ConsoleColor.Green);
                    }
                    else if(getColor == "Red")
                    {
                        WriteTextInColor(splitted[i], ConsoleColor.Red);
                    }
                }
                else
                {
                    Console.Write(splitted[i]);
                }
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Writes a given text in a given fontcolour
        /// </summary>
        /// <param name="text">The text to be displayed</param>
        /// <param name="col">The colour the text should be displayed in</param>
        private static void WriteTextInColor(string text, ConsoleColor col)
        {
            ConsoleColor tmp = Console.ForegroundColor;
            Console.ForegroundColor = col;
            Console.Write(text);
            Console.ForegroundColor = tmp;
        }
    }
}
