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
        /// Останавливает программу и ожидает, пока пользователь не нажмет любую клавишу. 
        /// </summary>
        public static void WaitForAnyButtonPush()
        {
            Console.WriteLine("\nДля продолжения нажмите любую клавишу... \n ");
            Console.ReadKey();
        }

        /// <summary>
        /// Останавливает программу и ожидает, пока пользователь не нажмет клавишу Enter. 
        /// </summary>
        public static void WaitForEnterButtonPush()
        {
            Console.WriteLine("\nНажмите Enter...\n");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
        }

        /// <summary>
        /// Выдает в консоль сообщение "Вы ввели не корректные данные.", выделенное красным цветом. 
        /// </summary>
        public static void EnterIncorrectData()
        {
            ConsoleColor defaltColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\nВы ввели не корректные данные.\n");
            Console.ForegroundColor = defaltColor;
        }

        /// <summary>
        /// Выдает в консоль приветственное сообщение и список поддерживаемых калькулятором математических операций. 
        /// </summary>
        public static void Greeting()
        {
            Console.WriteLine("Добро пожаловать в Калькулятор\n");
            Console.WriteLine("Мой калькулятор может выполнять следующие операции: " +
                              "\nСложение: + " +
                              "\nВычитание: - " +
                              "\nУмножение: * " +
                              "\nДеление: / " +
                              "\nВозведение в степень: ^");
            WaitForEnterButtonPush();
        }

        /// <summary>
        /// Выводит пользователю сообщение, когда он запускает режим рассчета из строки.
        /// </summary>
        public static void StartingCalculateByStringMod()
        {
            Console.Write("Введите математическое выражение одной строкой. Используйте запятую для записи чисел с дробной частью.  \n\n");
        }        
    }
}
