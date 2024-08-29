using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaidukovPSBstudyCalculator
{           
        enum CalculatorMod
        {
            GetFirstNumber,
            GetSecondNumber,
            GetMathOperator
        }
    
    /// <summary>
    /// Класс, исполняющий основные сценарии использования приложения
    /// </summary>
    internal class MainFunctions
    {
        ILogger Logger { get; set; }

        Calculator calc;
        InputConverter convert;

        internal MainFunctions() : this(new ConsoleLogger())
        {

        }
        
        internal MainFunctions(ILogger logger) 
        {
            Logger = logger;

            calc = new Calculator();
            convert = new InputConverter();
        }

        string StepMessage (CalculatorMod m)
        {
            return m switch
            {
                CalculatorMod.GetFirstNumber => "Введите первое число: ",
                CalculatorMod.GetSecondNumber => "Введите второе число: ",
                CalculatorMod.GetMathOperator => "Введите символ операции: "
            };
        }

        /// <summary>
        /// Калькулятор с пошаговым рассчетом.
        /// </summary>
        public void CalculatingStepByStep()  
        {
            convert.GetPartOfMathExpression(StepMessage(CalculatorMod.GetFirstNumber));
            convert.GetPartOfMathExpression(StepMessage(CalculatorMod.GetMathOperator));
            convert.GetPartOfMathExpression(StepMessage(CalculatorMod.GetSecondNumber));
            calc.Calculate(convert.MathOperator, convert.FirstNumber, convert.SecondNumber);
        }

        /// <summary>
        /// Калькуляятор, рассчитывающий выражение, записанное одной строкой.
        /// </summary>
        public void CalculatingByString()
        {
            Logger.SendMessage(LogMessage.StartCalculateByStringModMessage);

            convert.GetSplitedUsersString(
                convert.SplitUsersString($"[{AdditionalFunctions.letters}" +
                                          $"{AdditionalFunctions.punctuation}" +
                                          $"{AdditionalFunctions.brackets}" +
                                          $"{AdditionalFunctions.simbols}] ", AdditionalFunctions.mathOperators, Logger.ReadMessage()));

            if (convert.ValidateInput(convert.splitedInput))
            {
                do
                {
                    convert.GetBrackets();
                    CalculateStringWithoutBrackets();
                    convert.SplitedInputRemoveBracket(calc.TempResult, convert.BracketIsFound);

                    if (convert.MathOperatorCount == 0)
                        break;

                    convert.SetBracketIndexes();
                }
                while (convert.BracketIsFound);

                convert.SetExpressionAfterOpenBrackets();
                CalculateStringWithoutBrackets();
            }
            else
                Logger.SendMessage(LogMessage.EnterIncorrectDataMessage);
        }

        /// <summary>
        /// Метод рассчитывает выражение, не содержащее скобок, записанное в одну строку.
        /// </summary>
        public void CalculateStringWithoutBrackets()      //Приоритеты выполнения операций:
        {                                                   //Возведение в степень -> Умножение и деление -> Сложение и вычитание
            int i = convert.MathOperatorCount;

            for (int a = i - 1; a >= 0; a--) //цикл для вычисления степеней
            {
                convert.SetNumbersByMathoperator(a);
                if (convert.MathOperator == '^')
                {
                    CalculatePartOfString(a);
                    i--;
                }
            }

            for (int b = i - 1; b >= 0; b--) //цикл для вычисления умножений и делений
            {
                convert.SetNumbersByMathoperator(b);
                if (convert.MathOperator == '*' || convert.MathOperator == '/')
                {
                    CalculatePartOfString(b);
                    i--;
                }
            }

            for (int c = 0; c < i;) //цикл для вычисления сложений и вычитаний
            {
                convert.SetNumbersByMathoperator(c);

                if (convert.MathOperator == '+' || convert.MathOperator == '-')
                {
                    CalculatePartOfString(c);
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
        public void CalculatePartOfString(int mathOperatorNumber)
        {
            calc.Calculate(convert.MathOperator, convert.FirstNumber, convert.SecondNumber);
            convert.UpdateExpression(calc.TempResult, mathOperatorNumber);
        }









        //Функционал обработчиков массивов не трогал, т.к. их придется значительно рефакторить.



/*


        /// <summary>
        /// Метод перебирает входящие в массив числа, выбирая из них максимальное отрицательное и минимальное положительное.
        /// Следом выписывает эти числа в консоль. 
        /// </summary>
        /// <param name="mode"></param>
        public void SeachForNumbersInArray(string mode) 
        {
            int positiveMinimum = int.MaxValue;
            int negativeMaximum = int.MinValue;

            switch (mode)
            {
                case "user":
                    convert.GetUsersArray();
                    break;

                case "auto":
                    convert.GetRandomArray();
                    break;
            }

            if (convert.splitedInput.Any())
            {
                bool negativeMaximumFound = false;
                bool positiveMinimumFound = false;

                Console.WriteLine("Исходный массив: ");

                foreach (string s in convert.splitedInput)
                {
                    Console.Write(s + " ");

                    bool parced = int.TryParse(s, out var num);
                
                    if (parced)
                    {
                        if (num > negativeMaximum && num < 0)
                        {
                            negativeMaximum = num;
                            negativeMaximumFound = true;
                        }

                        if (num < positiveMinimum && num > 0)
                        {
                            positiveMinimum = num;
                            positiveMinimumFound = true;
                        }
                    }
                }
                
                if (positiveMinimumFound)
                    Console.WriteLine($"\nМинимальное положительное число: {positiveMinimum}");
                if (negativeMaximumFound)
                    Console.WriteLine($"Максимальное отрицательное число: {negativeMaximum}\n");
            }
            else
                AdditionalFunctions.EnterIncorrectDataMessage();
        }

 */


    }
}
