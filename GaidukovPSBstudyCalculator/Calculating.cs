using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaidukovPSBstudyCalculator
{
    internal class Calculating //Класс такого рода - стиль функционального программирования, переименовать в название для объекта, к примеру: Calculator
    {
        public double TempResult { get; private set; }
        
        //Здесь должен быть конструктор хотя бы пустой

        /// <summary>
        /// Переименовать в метод типа Calculator.Add(...), по принципу Объект.Действие(...), сигнатуру можно оставить ту же.
        /// Подумать, точно ли здесь должен бытиь вообще TempResult? Если это из-за того, что он нужен где-то в другом методе с использованием этого метода, 
        /// то лучше уж там сделать TempResult = Add(...), а здесь оставить return firNumber + secondNumber
        /// </summary>
        /// <param name="firstNumber"></param>
        /// <param name="secondNumber"></param>
        /// <returns></returns>
        double CalculateAddiction(double firstNumber, double secondNumber)
        {
            TempResult = firstNumber + secondNumber;
            return TempResult;
        }
        /// <summary>
        /// Аналогично CalculateAddiction
        /// </summary>
        /// <param name="firstNumber"></param>
        /// <param name="secondNumber"></param>
        /// <returns></returns>
        double CalculateSubtraction(double firstNumber, double secondNumber)
        {
            TempResult = firstNumber - secondNumber;
            return TempResult;
        }

        /// <summary>
        /// Аналогично CalculateAddiction
        /// </summary>
        /// <param name="firstNumber"></param>
        /// <param name="secondNumber"></param>
        /// <returns></returns>
        double CalculateMultiplication(double firstNumber, double secondNumber)
        {
            TempResult = firstNumber * secondNumber;
            return TempResult;
        }

        /// <summary>
        /// Аналогично CalculateAddiction
        /// </summary>
        /// <param name="firstNumber"></param>
        /// <param name="secondNumber"></param>
        /// <returns></returns>
        double CalculateDivision(double firstNumber, double secondNumber)
        {
            TempResult = firstNumber / secondNumber;
            return TempResult;
        }

        /// <summary>
        /// Аналогично CalculateAddiction
        /// </summary>
        /// <param name="firstNumber"></param>
        /// <param name="secondNumber"></param>
        /// <returns></returns>
        double CalculatePower(double firstNumber, double secondNumber)
        {
            TempResult = Math.Pow(firstNumber, secondNumber);
            return TempResult;
        }
        //Best: написать краткое саммари для всех методов выше, заменив комментарии ментора
        void PublicLogs(double firstNumber, double secondNumber, char mathOperator, double tempResult, string status)
        {
            if (status == "Ok")
                Console.WriteLine($"{firstNumber} {mathOperator} {secondNumber} = {tempResult}");
            else
            {
                //Исправить: всю логику со статусом перенести в метод Validate 
                //Саму переменную статуса либо убрать, либо заменить на enum вместо строки, если он ТОЧНО нужен (рекомендую подумать сначала, нужен ли)
                //Если хочется менять цвета, то к ILogger интерфейсу можно добавить метод LogError и логику логирования ошибки сделать там, а здесь только звать ILogger.LogError(...)
                switch (status)
                {
                    case "деление на ноль":
                        ConsoleColor defaltColor1 = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Обнаружено деление на ноль, операция не может быть выполнена.");
                        Console.ForegroundColor = defaltColor1;
                        break;

                    case "корень из отрицательного числа":
                        ConsoleColor defaltColor2 = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Обнаружено взятие корняя из отрицательного числа, операция не может быть выполнена.");
                        Console.ForegroundColor = defaltColor2;
                        break;
                };
            }
        }

        //Переименовать в Validate или ValidateOperation
        //OperationIsValid - критерий, подходящий состоянию, скорее название для свойства, чем для метода
        string OperationIsValid(double firstNumber, double secondNumber, char mathOperator)
        {
            if (mathOperator == '/' && secondNumber == 0)
            {
                AdditionalFunctions.EnterIncorrectData();
                return "деление на ноль";
            }
            else if (mathOperator == '^' && firstNumber < 0 && secondNumber > -1 && secondNumber < 1)
            {
                AdditionalFunctions.EnterIncorrectData();
                return "корень из отрицательного числа";
            }
            else
                return "Ok";
        }

        public void Calculate(char mathOperator, double firstNumber, double secondNumber)
        {
            switch (mathOperator)
            {
                case '+':
                    //Исправить: Убрать эту матрешку, должно быть (пример):
                    //ValidateOperation(...); - за пределами свича, а не дубль в каждом варианте свича
                    //CalculateAddiction(...), или другой нужный метод
                    //Всё логирование отдать экземпляру ILogger и перенести в эти методы, PublicLogs упразднить совсем!
                    
                    PublicLogs(firstNumber, secondNumber, mathOperator, 
                        CalculateAddiction(firstNumber, secondNumber),
                        OperationIsValid(firstNumber, secondNumber, mathOperator));
                    break;

                case '-':
                    PublicLogs(firstNumber, secondNumber, mathOperator, 
                        CalculateSubtraction(firstNumber, secondNumber),
                        OperationIsValid(firstNumber, secondNumber, mathOperator));
                    break;

                case '*':
                    PublicLogs(firstNumber, secondNumber, mathOperator, 
                        CalculateMultiplication(firstNumber, secondNumber),
                        OperationIsValid(firstNumber, secondNumber, mathOperator));
                    break;

                case '/':
                    PublicLogs(firstNumber, secondNumber, mathOperator, 
                        CalculateDivision(firstNumber, secondNumber), 
                        OperationIsValid(firstNumber, secondNumber, mathOperator));
                    break;

                case '^':
                    PublicLogs(firstNumber, secondNumber, mathOperator, 
                        CalculatePower(firstNumber, secondNumber),
                        OperationIsValid(firstNumber, secondNumber, mathOperator));
                    break;

                default:
                    AdditionalFunctions.EnterIncorrectData();
                    break;
            }
            //add.WaitForEnterButtonPush();
        }
    }
}
