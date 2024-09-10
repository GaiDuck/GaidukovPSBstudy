using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaidukovPSBstudyCalculator
{
    public enum LogMessage
    {
        GreetingMassege,
        CalculatorModMessage,
        FunctionIsDevelopingMessage,

        EnterFirstNumberMessage,
        EnterSecondNumberMessage,
        EnterMathOperatorMessage,

        WaitForAnyButtonPushMessage,
        ExitMessage,
        UnknownCommandMessage,
        EnterIncorrectDataMessage,

        StartCalculateByStringModMessage,
        SucsessOperationMessage,
        DivZeroMessage,
        NegativeRootMessage,

        PositiveMinimum,
        NegativeMaximum,
        OriginalArray,
        EnterArrayOfNumbers,
        ChooseRandomGenerateArrayMod,
        GeneratedArrayOfRandomNumbers,
        ChooseArrayGenerateMod,
        ChooseAreeyGenerateLength,

        DefaultMod,

        Space,
        Empty
    }

    public abstract class Logger()
    {
        protected string LoggerMessage(LogMessage m)
        {
            return m switch
            {
                LogMessage.GreetingMassege => "Добро пожаловать в Калькулятор!" +
                                              "\nМой калькулятор может выполнять следующие операции: " +
                                              "\nСложение: + " +
                                              "\nВычитание: - " +
                                              "\nУмножение: * " +
                                              "\nДеление: / " +
                                              "\nВозведение в степень: ^" +
                                              "\nТак же он может искать максимальное и минимально значение в массиве." +
                                              "\n\nВыберите режим работы:",

                LogMessage.CalculatorModMessage => "1 - пошаговый, 2 - строкой, 3 - массив от пользователя, 4 - генерация массива",

                LogMessage.FunctionIsDevelopingMessage => "Эта функция находится в разработке, попробуйте воспользоваться другой функцией.",

                LogMessage.EnterFirstNumberMessage => "Введите первое число: ",

                LogMessage.EnterSecondNumberMessage => "Введите второе число: ",

                LogMessage.EnterMathOperatorMessage => "Введите символ операции: ",

                LogMessage.WaitForAnyButtonPushMessage => "\nДля продолжения нажмите любую клавишу... \n ",

                LogMessage.ExitMessage => "\nВведите 1 для перезапуска.\nВведите 2 для завершения.",

                LogMessage.UnknownCommandMessage => "Неизвестная команда, попробуйте снова.",

                LogMessage.EnterIncorrectDataMessage => "\nВы ввели не корректные данные.\n",

                LogMessage.StartCalculateByStringModMessage => "\nВведите математическое выражение одной строкой. Используйте запятую для записи чисел с дробной частью.  \n",

                //LogMessage.SucsessOperationMessage => Пока не придумал, как реализовать.

                LogMessage.DivZeroMessage => "Обнаружено деление на ноль, операция не может быть выполнена.",

                LogMessage.NegativeRootMessage => "Обнаружено взятие корняя из отрицательного числа, операция не может быть выполнена.",

                LogMessage.PositiveMinimum => "\nМинимальное положительное число: ",

                LogMessage.NegativeMaximum => "\nМаксимальное отрицательное число: ",

                LogMessage.OriginalArray => "Исходный массив: ",

                LogMessage.EnterArrayOfNumbers => "\nВведите последовательность целых чисел, разделяя их пробелами.",

                LogMessage.ChooseRandomGenerateArrayMod => "Выбран режим случайной генерации чисел.",

                LogMessage.GeneratedArrayOfRandomNumbers => "Сгенерирован массив случайных чисел.",

                LogMessage.ChooseArrayGenerateMod => "Выберите режим генерации чисел.\n" +
                                                     "1 - Все числа;\n" +
                                                     "2 - Только положительные;\n" +
                                                     "3 - Только отрицательные;\n" +
                                                     "4 - Только четные;\n" +
                                                     "5 - Только нечетные.\n",

                LogMessage.ChooseAreeyGenerateLength => "Выберите длину генерируемого массива. Не менее 3 и не более 20 чисел.",

                LogMessage.DefaultMod => "Команда не распознана, выбран режим по умолчанию.",

                LogMessage.Space => " ",

                LogMessage.Empty => ""
            };
        }
    }
}
