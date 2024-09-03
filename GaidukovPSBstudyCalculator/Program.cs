using GaidukovPSBstudyCalculator;
using System;
using static GaidukovPSBstudyCalculator.ILogger;

internal class Program
{
    private static void Main(string[] args)
    {
        MainFunctions func = new MainFunctions();
        ArrayProcessing arr = new ArrayProcessing();
        InputConverter convert = new InputConverter();
        ConsoleLogger logger = new ConsoleLogger();

        bool modeIsCorrect;

        logger.SendMessage(LogMessage.GreetingMassege);

        do
        {
            do
            {
                logger.SendMessage(LogMessage.CalculatorModMessage);

                modeIsCorrect = false;

                switch (logger.ReadMessage())
                {
                    case "1": //калькулятор, считающий по шагам
                        func.CalculatingStepByStep();
                        modeIsCorrect = true;
                        break;

                    case "2":  //калькулятор, считающий из строки
                        func.CalculatingByString();
                        modeIsCorrect = true;
                        break;

                    case "3": //обработка массива, вводимого пользователем
                        arr.SeachForNumbersInArray(convert.GetArray("user"));
                        modeIsCorrect = true;
                        break;

                    case "4": //обработка массива, заполненного случайными числами
                        arr.SeachForNumbersInArray(convert.GetArray("auto"));
                        modeIsCorrect = true;
                        break;

                    default:
                        logger.SendMessage(LogMessage.FunctionIsDevelopingMessage);
                        break;
                }
            }
            while (!modeIsCorrect);
        }
        while (func.Exit());
    }
}



