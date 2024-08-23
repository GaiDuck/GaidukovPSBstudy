using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaidukovPSBstudyCalculator
{
    /// <summary>
    /// Класс, исполняющий основные сценарии использования приложения
    /// </summary>
    internal class MainFunctoins
    {
        Calculator calc = new Calculator();
        InputData input = new InputData();

        enum CalculatorMod
        {
            GetFirstNumber,
            GetSecondNumber,
            GetMathOperator
        }

        string message (CalculatorMod m)
        {
            switch (m)
            {
                case CalculatorMod.GetFirstNumber:
                    return "Введите первое число: ";
                case CalculatorMod.GetSecondNumber:
                    return "Введите второе число: ";
                case CalculatorMod.GetMathOperator:
                    return "Введите символ операции: ";
                default:
                    return "Ошибка";
            }
        }

        /// <summary>
        /// Калькулятор с пошаговым рассчетом.
        /// </summary>
        public void CalculatingStepByStep()  
        {
            input.GetPartOfMathExpression(message(CalculatorMod.GetFirstNumber));
            input.GetPartOfMathExpression(message(CalculatorMod.GetMathOperator));
            input.GetPartOfMathExpression(message(CalculatorMod.GetSecondNumber));
            calc.Calculate(input.MathOperator, input.FirstNumber, input.SecondNumber);
        }

        /// <summary>
        /// Калькуляятор, рассчитывающий выражение, записанное одной строкой.
        /// </summary>
        public void CalculatingByString()
        {
            AdditionalFunctions.StartingCalculateByStringMod();

            input.GettingSplitedUsersString(
                input.SplittingUsersString());

            if (input.ValidateInput(input.splitedInput))
            {
                do
                {
                    input.GetBrackets();
                    CalculatingStringWithoutBrackets();
                    input.SplitedInputRemoveBracket(calc.TempResult, input.BracketIsFound);

                    if (input.MathOperatorCount == 0)
                        break;

                    input.SetBracketIndexes();
                }
                while (input.BracketIsFound);

                input.SetExpressionAfterOpenBrackets();
                CalculatingStringWithoutBrackets();
            }
            else
                AdditionalFunctions.EnterIncorrectData();
        }

        /// <summary>
        /// Метод рассчитывает выражение, не содержащее скобок, записанное в одну строку.
        /// </summary>
        public void CalculatingStringWithoutBrackets()      //Приоритеты выполнения операций:
        {                                                   //Возведение в степень -> Умножение и деление -> Сложение и вычитание
            int i = input.MathOperatorCount;

            for (int a = i - 1; a >= 0; a--) //цикл для вычисления степеней
            {
                input.SetNumbersByMathoperator(a);
                if (input.MathOperator == '^')
                {
                    CalculatingPartOfString(a);
                    i--;
                }
            }

            for (int b = i - 1; b >= 0; b--) //цикл для вычисления умножений и делений
            {
                input.SetNumbersByMathoperator(b);
                if (input.MathOperator == '*' || input.MathOperator == '/')
                {
                    CalculatingPartOfString(b);
                    i--;
                }
            }

            for (int c = 0; c < i;) //цикл для вычисления сложений и вычитаний
            {
                input.SetNumbersByMathoperator(c);

                if (input.MathOperator == '+' || input.MathOperator == '-')
                {
                    CalculatingPartOfString(c);
                    i--;
                }
                else
                    c++;
            }
        }

        /// <summary>
        /// Метод, принимающий номер математического оператора, вызывает методы, 
        /// обсчитывающие часть строки и заменяющие ее результатом вычисления.
        /// </summary>
        /// <param name="i"></param>
        public void CalculatingPartOfString(int mathOperatorNumber)
        {
            calc.Calculate(input.MathOperator, input.FirstNumber, input.SecondNumber);
            input.UpdateExpression(calc.TempResult, mathOperatorNumber);
        }
    }
}
