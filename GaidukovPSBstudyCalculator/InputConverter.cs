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
    internal class InputConverter
    {
        ILogger Logger { get; set; }

        Random random;
        ConsoleLogger logger;

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

        internal InputConverter() : this(new ConsoleLogger()) //Мы кидаем перегрузку конструкторов, чтобы если мы обратимся к Converter через ConsoleLogger,
        {                                                     //то сработает этот, а если, например, через JsonLogger, то обратится он к тому, что будет от JsonLogger унаследован?
            
        }

        internal InputConverter(ILogger logger)
        {
            Logger = logger;

            splitedInput = new List<string>();
            _numbers = new List<double>();
            _operators = new List<char>();
            _bracket = new List<string>();

            random = new Random();
            logger = new ConsoleLogger();
        }

        /// <summary>
        /// Метод, последовательно вызывающий запись чисел и знака математической операции в пошаговом режиме работы калькулятора.
        /// </summary>
        /// <param name="message"></param>
        public void GetPartOfMathExpression(string message) 
        {
            Console.WriteLine($"\n{message}");
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
                    logger.SendMessage(LogMessage.EnterIncorrectDataMessage);
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
                    logger.SendMessage(LogMessage.EnterIncorrectDataMessage);
            }
            while (!mathOperatorFound);
        }

        //калькулятор с вводом строкой

        /// <summary>
        /// Метод получает от пользователя строку, удаляет оттуда символы, указанные в replacePattern 
        /// и разбивает строку по символам, указанным в splitPattern.
        /// </summary>
        /// <returns></returns>
        public string[] SplitUsersString(string replacePattern, string splitPattern, string input) 
        {
            string tempInput = Regex.Replace(input, replacePattern, "");
            string[] inputString = Regex.Split(tempInput, splitPattern);

            return inputString;
        }

        /// <summary>
        /// Метод записывает раздробленную строку в список строк.
        /// </summary>
        /// <returns></returns>
        public void GetSplitedUsersString(string[] input)
        {
            splitedInput.Clear();
            foreach (string s in input)
            {
                splitedInput.Add(s);
            }
        }

        /// <summary>
        /// Метод ищет и записывает индексы пары скобок, в которой отсутствуют другие скобки.
        /// </summary>
        /// <returns></returns>
        bool GetBracketIndex()
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
        void GetBracketList(bool bracketsAreFound)
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
        void GetLists(List<string> splitedList)
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
                GetBracketList( 
                    GetBracketIndex());

            GetLists(_bracket);
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
            GetLists(splitedInput);
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

        /// <summary>
        /// Метод принимает от пользователя произвольный массив чисел, удаляет из него все символы, кроме цифр, запятых и пробелов.
        /// </summary>
        public void GetUsersArray()
        {
            Console.WriteLine("\nВведите последовательность целых чисел, разделяя их пробелами.");

            splitedInput.Clear();

            GetSplitedUsersString(
                SplitUsersString($"[{AdditionalFunctions.letters}" +
                                      $"{AdditionalFunctions.punctuation}" +
                                      $"{AdditionalFunctions.brackets}" +
                                      $"{AdditionalFunctions.simbols}" +
                                      $"{AdditionalFunctions.mathOperators}]", " ", Console.ReadLine()));
        }

        /// <summary>
        /// Метод очищает splitedInput и наполняет его случайными числами на основании выбранного мода и количества чисел.
        /// </summary>
        public void GetRandomArray()
        {
            splitedInput.Clear();

            Console.WriteLine("Выбран режим случайной генерации чисел.");

            RandomMod mod = RandomizerMod();

            int arrayLength = ChooseLengthOfRandomizedArray();

            for (int i = 0; i < arrayLength; i++)
            {
                splitedInput.Add(Convert.ToString(RandomNumber(mod)));
            }

            Console.WriteLine("Сгенерирован массив случайных чисел.");
        }

        /// <summary>
        /// Предлагает пользователю выбрать режим генерации случайных чисел и 
        /// </summary>
        /// <param name="length"></param>
        RandomMod RandomizerMod()
        {
            Console.WriteLine("Выберите режим генерации чисел.\n" +
                              "1 - Все числа;\n" +
                              "2 - Только положительные;\n" +
                              "3 - Только отрицательные;\n" +
                              "4 - Только четные;\n" +
                              "5 - Только нечетные.\n");

            return Console.ReadKey().Key switch
            {
                ConsoleKey.D1 => RandomMod.AllNumbers,
                ConsoleKey.D2 => RandomMod.PositiveNumbers,
                ConsoleKey.D3 => RandomMod.NegativeNumbers,
                ConsoleKey.D4 => RandomMod.EvenNumber,
                ConsoleKey.D5 => RandomMod.OddNumber,
            };
        }

        /// <summary>
        /// Предлагает пользователю определить длину случайно генерируемого массива чисел в диапазоне (3..20).
        /// </summary>
        /// <returns></returns>
        int ChooseLengthOfRandomizedArray(int minLength = 3, int maxLength = 20)
        {
            int length;
            
            while (true)
            {
                Console.WriteLine("Выберите длину генерируемого массива. Не менее 3 и не более 20 чисел.");

                bool parced = int.TryParse(Console.ReadLine(), out var arrayLength);

                if (parced && arrayLength >= minLength && arrayLength <= maxLength)
                {
                    length = arrayLength;
                    break;
                }
                else
                    logger.SendMessage(LogMessage.EnterIncorrectDataMessage);
            }

            return length;
        }

        enum RandomMod
        {
            AllNumbers,
            PositiveNumbers,
            NegativeNumbers,
            EvenNumber,
            OddNumber
        }

        /// <summary>
        /// Метод на основании выбранного мода генерирует случайное число в диапазоне (-100..100).
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        int RandomNumber(RandomMod m)
        {
            return m switch
            {
                RandomMod.AllNumbers => random.Next(-100, 100),
                RandomMod.PositiveNumbers => random.Next(100),
                RandomMod.NegativeNumbers => random.Next(-100, 0),
                RandomMod.EvenNumber => random.Next(-100, 100) * 2 / 2,
                RandomMod.OddNumber => random.Next(-100, 100) * 2 / 2 + 1
            };
        }
    }
}
