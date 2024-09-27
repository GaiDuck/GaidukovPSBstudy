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

            calc = new Calculator(Logger);
            convert = new InputConverter(Logger);
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
                convert.SplitUsersString($"[{Constants.letters}" +
                                          $"{Constants.punctuation}" +
                                          $"{Constants.brackets}" +
                                          $"{Constants.simbols}] ", Constants.mathOperators, Logger.ReadMessage()));

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

        /// <summary>
        /// Метод останавливает программу и ожидает, пока пользователь не введет 1 для продолжения или 2 для завершения. 
        /// </summary>
        public bool Exit()
        {
            string button;
            bool exit;

            while (true)
            {
                Logger.SendMessage(LogMessage.ExitMessage);

                button = Logger.ReadMessage();

                if (button == "1")
                {
                    exit = true;
                    break;
                }
                else if (button == "2")
                {
                    exit = false;
                    break;
                }
                else
                {
                    Logger.SendMessage(LogMessage.UnknownCommandMessage);
                }
            }

            if (exit)
                return true;
            else
                return false;
        }
    }
}
