using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaidukovPSBstudyCalculator
{
    internal class InputData
    {
        public double FirstNumber { get; private set; }
        public double SecondNumber { get; private set; }
        public char MathOperator { get; private set; }

        private static char[] mathOperators = { '+', '-', '*', '/', '^', '(', ')' }; //при попытке сделать const выбрасывает две ошибки

        List<string> _splitedInput;
        List<double> _numbers;
        List<char> _operators;
        List<string> _bracket;

        public InputData()
        {
            _splitedInput = new List<string>();
            _numbers = new List<double>();
            _operators = new List<char>();
            _bracket = new List<string>();
        }

        public int MathOperatorCount { get; private set; }  
        public int OpenBracketIndex { get; private set; }
        public int CloseBracketIndex { get; private set; }
        public bool BracketIsFound { get; private set; }
        public double BracketResult { get; private set; }

        //калькулятор с вводом по действиям

        double GetNumber() //метод, парясящий вводимое пользователем число из строки в числовое значение
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
            while (!parsed); //будет запрашивать ввод числа пока пользователь не введет корректное значение

            return 0;
        }

        void GetFirstNumber() //метод, записывающий первое число в свойство
        {
            Console.Write("Введите первое число: ");
            FirstNumber = GetNumber();
        }

        void GetSecondNumber() //метод, записывающий второе число в свойство
        {
            Console.Write("Введите второе число: ");
            SecondNumber = GetNumber();
        }

        void GetMathOperator() //метод, записывающий математический оператор в свойство
        {
            Console.Write("Введите символ операции: ");

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

        public void GetDataV1() //комплекс методов, получающих два числа и математический оператор
        {
            GetFirstNumber();
            GetMathOperator();
            GetSecondNumber();
        }

        //калькулятор с вводом строкой

        public void StringInput() //принимает на вход строку и записывает ее по частям в массив строк
        {
            Console.Write("Введите математическое выражение одной строкой, разделяя все числа и математические операции " +
                          "пробелами. Используйте запятую для записи чисел с дробной частью.  \n\n");

            try
            {
                string[] input = Console.ReadLine().Split(' ');

                foreach (string s in input)
                {
                    _splitedInput.Add(s);
                }
            }
            catch
            {
                _splitedInput.Add("0");
            }
        }

        bool SeachForBrackets()
        {
            bool bracketsAreFound = false;
            bool openBracketIsFound = false;
            bool closeBracketIsFound = false;

            int openBracketIndex = 0;
            int closeBracketIndex = 0;

            for (int i = 0; i < _splitedInput.Count; i++)
            {
                if (_splitedInput[i] == "(")
                {
                    openBracketIndex = i;
                    openBracketIsFound = true;
                }

                if (_splitedInput[i] == ")")
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
                    _bracket.Add(_splitedInput[i]);
                }

                BracketIsFound = true;
            }
        }

        void CompliteLists(List<string> splitedInput)
        {
            for(int i = 0; i < splitedInput.Count; i++)
            {
                bool parced = double.TryParse(splitedInput[i], out var result);

                if (parced)
                    _numbers.Add(result);
                else
                    _operators.Add(Convert.ToChar(splitedInput[i]));
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
                _splitedInput[OpenBracketIndex] = Convert.ToString(tempResult);
                _splitedInput.RemoveRange(OpenBracketIndex + 1, CloseBracketIndex - OpenBracketIndex);
                foreach (var s in _splitedInput) { Console.Write(s + " "); }
                Console.WriteLine(" ");
            }
        }

        public void GetBrackets()
        {
            OpenBracketIndex = 0;
            CloseBracketIndex = 0;
                CompliteBracketList(
                    SeachForBrackets());

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
            CompliteLists(_splitedInput);
        }
    }
}
