using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaidukovPSBstudyCalculator
{
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

        public void CalculatingStepByStep()  
        {
            input.GetPartOfMathExpression(message(CalculatorMod.GetFirstNumber));
            input.GetPartOfMathExpression(message(CalculatorMod.GetMathOperator));
            input.GetPartOfMathExpression(message(CalculatorMod.GetSecondNumber));
            calc.Calculate(input.MathOperator, input.FirstNumber, input.SecondNumber);
        }

        public void CalculatingByString()
        {
            input.GettingUsersString();
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

        public void CalculatingPartOfString(int i)
        {
            calc.Calculate(input.MathOperator, input.FirstNumber, input.SecondNumber);
            input.UpdateExpression(calc.TempResult, i);
        }
    }
}
