using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaidukovPSBstudyCalculator
{
    /// <summary>
    /// Класс, содержащий методы, которые не влияют на прямой функционал программы.
    /// </summary>
    internal static class AdditionalFunctions 
    {
        /// <summary>
        /// Останавливает программу и ожидает, пока пользователь не нажмет клавишу Enter для продолжения или Escape для завершения. 
        /// </summary>
        public static bool Exit()
        {
            bool exit;
            ConsoleKey button;

            while (true)
            {
                Console.WriteLine("\nНажмите Enter для перезапуска.\nНажмите Escape для завершения.");

                button = Console.ReadKey().Key;

                if (button == ConsoleKey.Escape)
                {
                    exit = true;
                    break;
                }
                else if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    exit = false;
                    break;
                }
                else
                {
                    Console.WriteLine("Неизвестная команда, попробуйте снова.");
                }
            }

            if (exit) 
                return true;
            else
                return false;
        }
        
        /// <summary>
        /// Множество всех цифр и запятая, используемая для от
        /// </summary>
        public static string numbers = "0-9,";
        public static string letters = "A-Za-zА-Яа-я";
        public static string punctuation = "`|'|\"|:|;|.|!|?|\\|~|_";
        public static string brackets = "|[|<|{|]|>|}|";
        public static string simbols = "@#№$&%=";
        public static string mathOperators = @"(/)|(\-)|(\*)|(\+)|(\^)|(\()|(\))";
    }
}
