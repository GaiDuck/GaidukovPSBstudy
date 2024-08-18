using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaidukovPSBstudyCalculator
{
    internal class Calculator
    {
        public double TempResult { get; private set; }
        
        //Здесь должен быть конструктор хотя бы пустой
        public Calculator()
        {

        }

        /// <summary>
        /// Переименовать в метод типа Calculator.Add(...), по принципу Объект.Действие(...), сигнатуру можно оставить ту же.
        /// Подумать, точно ли здесь должен бытиь вообще TempResult? Если это из-за того, что он нужен где-то в другом методе с использованием этого метода, 
        /// то лучше уж там сделать TempResult = Add(...), а здесь оставить return firNumber + secondNumber
        /// </summary>
        /// <param name="firstNumber"></param>
        /// <param name="secondNumber"></param>
        /// <returns></returns>
        double Add(double firstNumber, double secondNumber)
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
        double Sub(double firstNumber, double secondNumber)
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
        double Mult(double firstNumber, double secondNumber)
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
        double Div(double firstNumber, double secondNumber)
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
        double Pow(double firstNumber, double secondNumber)
        {
            TempResult = Math.Pow(firstNumber, secondNumber);
            return TempResult;
        }
        //Best: написать краткое саммари для всех методов выше, заменив комментарии ментора

        public void Calculate(char mathOperator, double firstNumber, double secondNumber)
        {
            if (Validate(firstNumber, secondNumber, mathOperator))
            {
                switch (mathOperator)
                {
                    case '+':
                        //Исправить: Убрать эту матрешку, должно быть (пример):
                        //ValidateOperation(...); - за пределами свича, а не дубль в каждом варианте свича
                        //CalculateAddiction(...), или другой нужный метод
                        //Всё логирование отдать экземпляру ILogger и перенести в эти методы, PublicLogs упразднить совсем!

                        Add(firstNumber, secondNumber);
                    break;

                    case '-':
                        Sub(firstNumber, secondNumber);
                    break;

                    case '*':
                        Mult(firstNumber, secondNumber);
                    break;

                    case '/':
                        Div(firstNumber, secondNumber);
                    break;

                    case '^':
                        Pow(firstNumber, secondNumber);
                    break;

                    default:
                        AdditionalFunctions.EnterIncorrectData();
                    break;
                }
            }
            ILogger();
            //add.WaitForEnterButtonPush();
        }

        bool Validate(double firstNumber, double secondNumber, char mathOperator)
        {
            bool valid = false;

            if (mathOperator == '/' && secondNumber == 0)
            {
                AdditionalFunctions.EnterIncorrectData();
                Console.WriteLine("Обнаружено деление на ноль, операция не может быть выполнена.");
            }
            else if (mathOperator == '^' && firstNumber < 0 && secondNumber > -1 && secondNumber < 1)
            {
                AdditionalFunctions.EnterIncorrectData();
                Console.WriteLine("Обнаружено взятие корняя из отрицательного числа, операция не может быть выполнена.");
            }
            else
            {
                valid = true;
            }
            return valid;
        }

        void ILogger()
        {

        }
    }
}
