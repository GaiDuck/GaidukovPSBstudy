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
    internal static class Constants 
    {      
        /// <summary>
        /// Множество всех цифр и запятая, используемая для отделения дробной части от целой.
        /// </summary>
        public static string numbers = "0-9,";

        /// <summary>
        /// Множество всех букв русского и английского алфавита, и пробел.
        /// </summary>
        public static string letters = "A-Za-zА-Яа-я ";

        /// <summary>
        /// Множество всех знаков припинания, исключая наклонный слеш.
        /// </summary>
        public static string punctuation = "`|'|\"|:|;|.|!|?|\\|~|_";

        /// <summary>
        /// Множество всех скобок, исключая круглые скобки.
        /// </summary>
        public static string brackets = "|[|<|{|]|>|}|";

        /// <summary>
        /// Множество всех служебных символов.
        /// </summary>
        public static string simbols = "@#№$&%=";

        /// <summary>
        /// Множество всех математических операторов.
        /// </summary>
        public static string mathOperators = @"(/)|(\-)|(\*)|(\+)|(\^)|(\()|(\))";
    }
}
