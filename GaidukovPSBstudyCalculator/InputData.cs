using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GaidukovPSBstudyCalculator
{
    internal class InputData
    {
        public double FirstNumber { get; private set; }
        public double SecondNumber { get; private set; }
        public char MathOperator { get; private set; }
        public int MathOperatorCount { get; private set; }  //Тут не нужен set, проще отдать свойство _operators.Count в get
        public int OpenBracketIndex { get; private set; }
        public int CloseBracketIndex { get; private set; }
        public bool BracketIsFound { get; private set; } //Можно тоже без set
        public double BracketResult { get; private set; }

        private static char[] mathOperators = { '+', '-', '*', '/', '^', '(', ')' }; 

        public List<string> splitedInput;
        List<double> _numbers;
        List<char> _operators;
        List<string> _bracket;

        public InputData()
        {
            splitedInput = new List<string>();
            _numbers = new List<double>();
            _operators = new List<char>();
            _bracket = new List<string>();
        }


        //калькулятор с вводом по действиям 
        public void GetPartOfMathExpression(string message) 
        {
            Console.Write(message);
            switch (message)
            {
                case "Введите первое число: ":
                    FirstNumber = GetPartOfMathExpression();
                break;
                
                case "Введите второе число: ":
                    SecondNumber = GetPartOfMathExpression();
                break;

                case "Введите символ операции: ":
                    GetMathOperator();
                break;

                default: break;
            }
        }

        double GetPartOfMathExpression()
        {
            bool parsed;

            do
            {
                parsed = double.TryParse(Console.ReadLine(), out var input);

                if (parsed)
                    return input;
                else
                    AdditionalFunctions.EnterIncorrectData();
            }
            while (!parsed);

            return 0; //Сударь, вы из C++ сбежали или из JS? Здесь нужно Exception бросить, потому что логика программы не подразумевает, что попасть сюда возможно
        }

        void GetMathOperator() 
        {
            bool mathOperatorFound = false;

            do
            {
                bool parsed = char.TryParse(Console.ReadLine(), out var input);

                if (parsed)
                {
                    if(mathOperators.Contains(input))
                    {
                        MathOperator = input;
                        mathOperatorFound = true;
                    }
                }

                if (!(mathOperatorFound && parsed))
                    AdditionalFunctions.EnterIncorrectData();
            }
            while (!mathOperatorFound);
        }

        //калькулятор с вводом строкой

        //Исправить: Переименовать, чтобы любой человек по названию метода мог определить, что здесь происходит
        //Существуют практики наименования методов и свойств, гугл в помощь

        public string GettingUsersString()
        {
            Console.Write("Введите математическое выражение одной строкой, разделяя все числа и математические операции " +
                          "пробелами. Используйте запятую для записи чисел с дробной частью.  \n\n");
            
            string usersInput = Console.ReadLine();

            return usersInput;
        }

        public string[] SplittingUsersString(string usersInput) 
        {
            string replacePattern = "[A-Za-zА-Яа-я .!\"\'@#№;$%:?&=`~<>]";
            string splitPattern = @"(/)|(-)|(\*)|(\+)|(\^)|(\()|(\))";

            string tempInput = Regex.Replace(usersInput, replacePattern, "");
            string[] input = Regex.Split(tempInput, splitPattern);

            return input;
        }

        public void GettingSplitedUsersString(string[] input)
        {
            foreach (string s in input)
            {
                splitedInput.Add(s);
            }
        }

        bool SeachForBracketIndex()
        {
            bool bracketsAreFound = false;
            bool openBracketIsFound = false;
            bool closeBracketIsFound = false;

            int openBracketIndex = 0;
            int closeBracketIndex = 0;

            for (int i = 0; i < splitedInput.Count; i++)
            {
                if (splitedInput[i] == "(")
                {
                    openBracketIndex = i;
                    openBracketIsFound = true;
                }

                if (splitedInput[i] == ")")
                {
                    closeBracketIndex = i;
                    closeBracketIsFound = true;
                }

                if (openBracketIsFound && closeBracketIsFound)
                {
                    bracketsAreFound = true;
                    OpenBracketIndex = openBracketIndex;
                    CloseBracketIndex = closeBracketIndex;
                    break;
                }
            }

            return bracketsAreFound;
        }

        void CompliteBracketList(bool bracketsAreFound)
        {
            BracketIsFound = false;
            _bracket.Clear();

            if (bracketsAreFound)
            {
                for (int i = OpenBracketIndex + 1; i < CloseBracketIndex; i++)
                {
                    _bracket.Add(splitedInput[i]);
                }

                BracketIsFound = true;
            }
        }

        void CompliteLists(List<string> splitedList)
        {
            for(int i = 0; i < splitedList.Count; i++)
            {
                bool parcedNumber = double.TryParse(splitedList[i], out var number);
                bool parcedMathOperator = char.TryParse(splitedList[i], out var mathOperator);

                if (parcedNumber)
                    _numbers.Add(number);
                else if (parcedMathOperator && mathOperators.Contains(mathOperator))
                    _operators.Add(mathOperator);
            }
            MathOperatorCount = _operators.Count;
        }

        public void SetNumbersByMathoperator(int mathOperatorNumber)
        {
            FirstNumber = _numbers[mathOperatorNumber];
            SecondNumber = _numbers[mathOperatorNumber + 1];
            MathOperator = _operators[mathOperatorNumber];
        }

        public void UpdateExpression(double tempResult, int mathOperatorNumber)
        {
            _numbers[mathOperatorNumber] = tempResult;
            _numbers.RemoveAt(mathOperatorNumber+1);
            _operators.RemoveAt(mathOperatorNumber);
        }
        public void SplitedInputRemoveBracket(double tempResult, bool bracketIsFound)
        {
            if (bracketIsFound)
            {
                splitedInput[OpenBracketIndex] = Convert.ToString(tempResult);
                splitedInput.RemoveRange(OpenBracketIndex + 1, CloseBracketIndex - OpenBracketIndex);
/*
                foreach (var s in _splitedInput) { Console.Write(s); }
                Console.WriteLine(" ");
*/
            }
        }

        public void GetBrackets()
        {
            OpenBracketIndex = 0;
            CloseBracketIndex = 0;
                CompliteBracketList( 
                    SeachForBracketIndex());

            CompliteLists(_bracket);
        }

        public void SetBracketIndexes()
        {
            OpenBracketIndex = 0;
            CloseBracketIndex = 0;
            _numbers.Clear();
        }

        public void SetExpressionAfterOpenBrackets()
        {
            _numbers.Clear();
            _operators.Clear();
            CompliteLists(splitedInput);
        }

        int BracketsCout(List<string> input, string bracket)
        {
            int bracketCount = 0;

            foreach (var s in input)
            {
                if (s == bracket)
                    bracketCount++;
            }
            return bracketCount;
        }

        bool ValidateBrackets(int openBracketCount, int closeBracketCount)
        {
            if (openBracketCount == closeBracketCount)
                return true;
            else
                return false;
        }

        int NumbersCount(List<string> input)
        {
            int numbersCount = 0;

            foreach (var s in input)
            {
                if (double.TryParse(s, out var result))
                    numbersCount++;
            }
            return numbersCount;
        }

        int MathOperatorsCount(List<string> input)
        {
            int mathOperatorsCount = 0;

            foreach (var s in input)
            {
                bool parced = char.TryParse(s, out var result);
                if (parced && mathOperators.Contains(result))
                    mathOperatorsCount++;
            }
            return mathOperatorsCount;
        }

        bool ValidateMathOperators(int openBracketCount, int closeBracketCount, int numbersCount, int mathOperatorsCount)
        {
            if (numbersCount == mathOperatorsCount - openBracketCount - closeBracketCount + 1)
                return true;
            else 
                return false;
        }

        public bool ValidateInput(List<string> input)
        {
            if (ValidateBrackets(BracketsCout(input, "("), BracketsCout(input, ")")) 
                && ValidateMathOperators(BracketsCout(input, "("), BracketsCout(input, ")"), NumbersCount(input), MathOperatorsCount(input)))
                return true;
            else
                return false;
        }
    }
}
