using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaidukovPSBstudyCalculator
{
    internal class Calculating
    {
        public double TempResult { get; private set; }

        double CalculateAddiction(double firstNumber, double secondNumber)
        {
            TempResult = firstNumber + secondNumber;
            return TempResult;
        }

        double CalculateSubtraction(double firstNumber, double secondNumber)
        {
            TempResult = firstNumber - secondNumber;
            return TempResult;
        }

        double CalculateMultiplication(double firstNumber, double secondNumber)
        {
            TempResult = firstNumber * secondNumber;
            return TempResult;
        }

        double CalculateDivision(double firstNumber, double secondNumber)
        {
            TempResult = firstNumber / secondNumber;
            return TempResult;
        }

        double CalculatePower(double firstNumber, double secondNumber)
        {
            TempResult = Math.Pow(firstNumber, secondNumber);
            return TempResult;
        }

        void PublicLogs(double firstNumber, double secondNumber, char mathOperator, double tempResult, string status)
        {
            if (status == "Ok")
                Console.WriteLine($"{firstNumber} {mathOperator} {secondNumber} = {tempResult}");
            else
            {
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
