using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaidukovPSBstudyCalculator
{
    internal class Running
    {
        public Calculator calc = new Calculator();
        public InputData input = new InputData();


        public void CalculatingStepByStep()  //калькулятор с пошаговым рассчестом
        {
            input.GetUsersInput();
            input.GetDataV1();
            calc.Calculate(input.MathOperator, input.FirstNumber, input.SecondNumber);
        }

        void CalculatingPartOfString(int i)
        {
            calc.Calculate(input.MathOperator, input.FirstNumber, input.SecondNumber);
            input.UpdateExpression(calc.TempResult, i);
        }

        public void CalculatingByString()      //Приоритеты выполнения операций:
        {                               //Возведение в степень -> Умножение и деление -> Сложение и вычитание
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
    }
}
