using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaidukovPSBstudyCalculator
{
    internal class ArrayProcessing
    {
        ILogger Logger { get; set; }

        Calculator calc;
        InputConverter convert;
        
        internal ArrayProcessing() : this(new ConsoleLogger())
        {

        }

        internal ArrayProcessing(ILogger logger)
        {
            Logger = logger;

            calc = new Calculator();
            convert = new InputConverter();
        }

        /// <summary>
        /// Метод перебирает входящие в массив числа, выбирая из них максимальное отрицательное и минимальное положительное.
        /// Следом выписывает эти числа в консоль. 
        /// </summary>
        /// <param name="mode"></param>
        public void SeachForNumbersInArray(string[] array)
        {
            int positiveMinimum = int.MaxValue;
            int negativeMaximum = int.MinValue;

            if (convert.splitedInput.Any())
            {
                bool negativeMaximumFound = false;
                bool positiveMinimumFound = false;

                Logger.SendMessage(LogMessage.OriginalArray);

                foreach (string s in convert.splitedInput)
                {
                    Logger.LogString(LogMessage.Space, s);

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
                    Logger.LogResult(LogMessage.PositiveMinimum, positiveMinimum);
                if (negativeMaximumFound)
                    Logger.LogResult(LogMessage.NegativeMaximum, negativeMaximum);
            }
            else
                Logger.SendMessage(LogMessage.EnterIncorrectDataMessage);
        }
    }
}
