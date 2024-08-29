using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaidukovPSBstudyCalculator
{
    /// <summary>
    /// Класс для выполнения базовых математических операций, проверки корректности запрашиваемых пользователем математических операций и 
    /// вывода в консоль промежуточных значений.
    /// </summary>
    internal class Calculator
    {
        /// <summary>
        /// Промежуточный результат вычислений.
        /// </summary>
        public double TempResult { get; private set; }
        ILogger Logger { get; set; }

        ConsoleLogger logger;

        internal Calculator() : this(new ConsoleLogger())
        {

        }

        public Calculator(ILogger logger)
        {
            Logger = logger;

            logger = new ConsoleLogger();
        }

        /// <summary>
        /// Метод принимает два числа и возвращает их сумму.
        /// </summary>
        /// <param name="firstNumber"></param>
        /// <param name="secondNumber"></param>
        /// <returns>FirstNumber + SecondNumber </returns>
        double Add(double firstNumber, double secondNumber)
        {
            return firstNumber + secondNumber;
        }

        /// <summary>
        /// Метод принимает два числа и возвращает их разность.
        /// </summary>
        /// <param name="firstNumber"></param>
        /// <param name="secondNumber"></param>
        /// <returns>FirstNumber - SecondNumber </returns>
        double Sub(double firstNumber, double secondNumber)
        {
            return firstNumber - secondNumber;
        }

        /// <summary>
        /// Метод принимает два числа и возвращает их произведение.
        /// </summary>
        /// <param name="firstNumber"></param>
        /// <param name="secondNumber"></param>
        /// <returns>FirstNumber * SecondNumber </returns>
        double Mult(double firstNumber, double secondNumber)
        {
            return firstNumber * secondNumber;
        }

        /// <summary>
        /// Метод принимает два числа и возвращает их частное.
        /// </summary>
        /// <param name="firstNumber"></param>
        /// <param name="secondNumber"></param>
        /// <returns>FirstNumber / SecondNumber </returns>
        double Div(double firstNumber, double secondNumber)
        {
            return firstNumber / secondNumber;
        }

        /// <summary>
        /// Метод принимает два числа и возвращает число в степени.
        /// </summary>
        /// <param name="firstNumber"></param>
        /// <param name="secondNumber"></param>
        /// <returns>FirstNumber ^ SecondNumber </returns>
        double Pow(double firstNumber, double secondNumber)
        {
            return Math.Pow(firstNumber, secondNumber);
        }

        /// <summary>
        /// Метод принимает два числа и знак математического оператора. 
        /// На основании полученных данных вызывает метод, который проверяет корректность операции, затем вызывает метод, выполняющий операцию.
        /// </summary>
        /// <param name="mathOperator"></param>
        /// <param name="firstNumber"></param>
        /// <param name="secondNumber"></param>
        public void Calculate(char mathOperator, double firstNumber, double secondNumber)
        {
            if (Validate(firstNumber, secondNumber, mathOperator))
            {
                switch (mathOperator)
                {
                    case '+':
                        TempResult = Add(firstNumber, secondNumber);
                    break;

                    case '-':
                        TempResult = Sub(firstNumber, secondNumber);
                    break;

                    case '*':
                        TempResult = Mult(firstNumber, secondNumber);
                    break;

                    case '/':
                        TempResult = Div(firstNumber, secondNumber);
                    break;

                    case '^':
                        TempResult = Pow(firstNumber, secondNumber);
                    break;

                    default:
                        logger.SendMessage(LogMessage.EnterIncorrectDataMessage);
                        break;
                }
            }
            logger.LogMathOperation(firstNumber, secondNumber, mathOperator, TempResult);
        }

        /// <summary>
        /// Метод, проверяющий корректность математической операции. 
        /// Возвращает true, если операция может быть выполнена, возвращает false, если операция не может быть выполнена. 
        /// </summary>
        /// <param name="firstNumber"></param>
        /// <param name="secondNumber"></param>
        /// <param name="mathOperator"></param>
        /// <returns></returns>
        bool Validate(double firstNumber, double secondNumber, char mathOperator)
        {
            bool valid = false;

            if (mathOperator == '/' && secondNumber == 0)
            {
                logger.SendMessage(LogMessage.EnterIncorrectDataMessage);
                Console.WriteLine("Обнаружено деление на ноль, операция не может быть выполнена.");
            }
            else if (mathOperator == '^' && firstNumber < 0 && secondNumber > -1 && secondNumber < 1)
            {
                logger.SendMessage(LogMessage.EnterIncorrectDataMessage);
                Console.WriteLine("Обнаружено взятие корняя из отрицательного числа, операция не может быть выполнена.");
            }
            else
            {
                valid = true;
            }
            return valid;
        }
    }
}
