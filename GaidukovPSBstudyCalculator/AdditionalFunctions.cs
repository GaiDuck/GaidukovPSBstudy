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
