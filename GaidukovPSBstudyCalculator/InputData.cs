using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GaidukovPSBstudyCalculator
{
    /// <summary>
    /// Класс, выполняющий функции получения информации от пользователя, её первичной проверки и обработки.
    /// </summary>
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


        /// <summary>
        /// Метод, последовательно вызывающий запись чисел и знака математической операции в пошаговом режиме работы калькулятора.
        /// </summary>
        /// <param name="message"></param>
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

        /// <summary>
        /// Метод, принимающий от пользователя строку и преобразующий её в число.
        /// В случае ошибки метод перезапускается.
        /// </summary>
        double GetPartOfMathExpression()
        {
            bool parsed;
            double input;

            while (true)
            {
                parsed = double.TryParse(Console.ReadLine(), out var result);

                if (parsed)
                {
                    input = result;
                    break;
                }
                else
                    AdditionalFunctions.EnterIncorrectData();
            }
            return input;
        }

        /// <summary>
        /// Метод, принимающий от пользователя строку и преобразующий её в символ математической операции.
        /// В случае ошибки метод перезапускается.
        /// </summary>
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

        /// <summary>
        /// Метод получает от пользователя математическое выражение одной строкой, 
        /// удаляет оттуда лишние символы и разбивает строку на отдельные числа и математические операторы.
        /// </summary>
        /// <returns></returns>
        public string[] SplittingUsersString() 
        {
            string replacePattern = "[A-Za-zА-Яа-я .!\"\'@#№;$%:?&=`~<>]";
            string splitPattern = @"(/)|(-)|(\*)|(\+)|(\^)|(\()|(\))";

            string tempInput = Regex.Replace(Console.ReadLine(), replacePattern, "");
            string[] input = Regex.Split(tempInput, splitPattern);

            return input;
        }

        /// <summary>
        /// Метод записывает раздробленную строку в список строк.
        /// </summary>
        /// <returns></returns>
        public void GettingSplitedUsersString(string[] input)
        {
            foreach (string s in input)
            {
                splitedInput.Add(s);
            }
        }

        /// <summary>
        /// Метод ищет и записывает индексы пары скобок, в которой отсутствуют другие скобки.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Метод копирует часть изначальной строки, находившуюся между ранее найденными скобками.
        /// </summary>
        /// <param name="bracketsAreFound"></param>
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

        /// <summary>
        /// Метод принимает заданный список из чисел и математических операторов, затем распределяет числа в один список, а математические операторы в другой.
        /// </summary>
        /// <param name="splitedList"></param>
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

        /// <summary>
        /// Метод по заданному номеру математического оператора выбирает пару чисел из списка чисел 
        /// и математический оператор из списка математических операторов, после чего записывает их 
        /// в свойстра, которые могут быть считанны методами, выполняющими математические операции.
        /// </summary>
        /// <param name="mathOperatorNumber"></param>
        public void SetNumbersByMathoperator(int mathOperatorNumber)
        {
            FirstNumber = _numbers[mathOperatorNumber];
            SecondNumber = _numbers[mathOperatorNumber + 1];
            MathOperator = _operators[mathOperatorNumber];
        }

        /// <summary>
        /// Метод принимает промежуточный результат математической операции,
        /// удаляет отработанные числа и математический оператор из соответствующих списков
        /// и записывает промежуточный результат на мместо первого числа из пары.
        /// </summary>
        /// <param name="tempResult"></param>
        /// <param name="mathOperatorNumber"></param>
        public void UpdateExpression(double tempResult, int mathOperatorNumber)
        {
            _numbers[mathOperatorNumber] = tempResult;
            _numbers.RemoveAt(mathOperatorNumber+1);
            _operators.RemoveAt(mathOperatorNumber);
        }

        /// <summary>
        /// Метод принимает результат вычисления содержимого скобок и заменяет в изначальном выражении скобку результатом ее вычисления.
        /// </summary>
        /// <param name="tempResult"></param>
        /// <param name="bracketIsFound"></param>
        public void SplitedInputRemoveBracket(double tempResult, bool bracketIsFound)
        {
            if (bracketIsFound)
            {
                splitedInput[OpenBracketIndex] = Convert.ToString(tempResult);
                splitedInput.RemoveRange(OpenBracketIndex + 1, CloseBracketIndex - OpenBracketIndex);
            }
        }

        /// <summary>
        /// Метод последовательно вызывает несколько методов, которые находят наиболее глубоко вложенную пару скобок 
        /// и разделяют ее содержимое на список чисел и список математических операторов. 
        /// </summary>
        public void GetBrackets()
        {
            OpenBracketIndex = 0;
            CloseBracketIndex = 0;
                CompliteBracketList( 
                    SeachForBracketIndex());

            CompliteLists(_bracket);
        }

        /// <summary>
        /// Метод сбрасывает к изначальному состоянию вспомогательные свойства и списки, необходимые для вычисления содержимого скобок.
        /// </summary>
        public void SetBracketIndexes()
        {
            OpenBracketIndex = 0;
            CloseBracketIndex = 0;
            _numbers.Clear();
        }

        /// <summary>
        /// Метод сбрасывает к изначальному состоянию вспомогательные списки и заново заполняет их числами и математическими операторами
        /// из начального выражения, в котором все скобки заменены эквивалентными значениями.
        /// </summary>
        public void SetExpressionAfterOpenBrackets()
        {
            _numbers.Clear();
            _operators.Clear();
            CompliteLists(splitedInput);
        }

        /// <summary>
        /// Метод принимает выражение и символ, затем подсчитывает и возвращает количество заданных символов. 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="bracket"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Метод проверяет соответствие количества открывающих скобок количеству закрывающих скобок.
        /// </summary>
        /// <param name="openBracketCount"></param>
        /// <param name="closeBracketCount"></param>
        /// <returns></returns>
        bool ValidateBrackets(int openBracketCount, int closeBracketCount)
        {
            if (openBracketCount == closeBracketCount)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Метод подсчитывает количество чисел в математическом выражении.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Метод подсчитывает количество математических операций.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Метод проверяет соответствие количества чисел количеству математических операций.
        /// </summary>
        /// <param name="openBracketCount"></param>
        /// <param name="closeBracketCount"></param>
        /// <param name="numbersCount"></param>
        /// <param name="mathOperatorsCount"></param>
        /// <returns></returns>
        bool ValidateMathOperators(int openBracketCount, int closeBracketCount, int numbersCount, int mathOperatorsCount)
        {
            if (numbersCount == mathOperatorsCount - openBracketCount - closeBracketCount + 1)
                return true;
            else 
                return false;
        }

        /// <summary>
        /// Проверяет корректность вводимой пользователем строки, проверяя колличество скобок
        /// и соответствие количества математических операций и чисел. 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
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
