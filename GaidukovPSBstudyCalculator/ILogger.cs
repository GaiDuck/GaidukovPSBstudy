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
        NegativeRootMessage
    }

    public interface ILogger
    {
        void SendMessage(LogMessage m);
        void LogMathOperation(double firstNumber, double secondNumber, char mathOperator, double tempResult);
        string ReadMessage();
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
                LogMessage.ExitMessage => "\nНажмите Enter для перезапуска.\nНажмите Escape для завершения.",
                LogMessage.UnknownCommandMessage => "Неизвестная команда, попробуйте снова.",
                LogMessage.EnterIncorrectDataMessage => "\nВы ввели не корректные данные.\n",
                LogMessage.StartCalculateByStringModMessage => "\nВведите математическое выражение одной строкой. Используйте запятую для записи чисел с дробной частью.  \n",
                //LogMessage.SucsessOperationMessage => Пока не придумал, как реализовать.
                LogMessage.DivZeroMessage => "Обнаружено деление на ноль, операция не может быть выполнена.",
                LogMessage.NegativeRootMessage => "Обнаружено взятие корняя из отрицательного числа, операция не может быть выполнена."
            };
        }
    }

    public class ConsoleLogger : Logger, ILogger
    {
        public string ReadMessage()
        {
            return Console.ReadLine();
        }

        public void LogMathOperation(double firstNumber, double secondNumber, char mathOperator, double tempResult)
        {
            string message = $"{Math.Round(firstNumber, 4)} {mathOperator} {Math.Round(secondNumber, 4)} = {Math.Round(tempResult, 4)}";
            Console.WriteLine(message);
        }

        public void SendMessage(LogMessage m)
        {
            Console.WriteLine(LoggerMessage(m));
        }
    }


    public class JsonLogger : Logger, ILogger
    {

        public string ReadMessage()
        {
            throw new NotImplementedException();
        }

        public void LogMathOperation(double firstNumber, double secondNumber, char mathOperator, double tempResult)
        {
            throw new NotImplementedException();
        }

        public void SendMessage(LogMessage m)
        {
            throw new NotImplementedException();
        }
    }
}
