using GaidukovPSBstudyCalculator;
using System;
using static GaidukovPSBstudyCalculator.ILogger;

internal class Program
{
    private static void Main(string[] args)
    {
        ConsoleLogger logger = new ConsoleLogger();
        MainFunctions func = new MainFunctions(logger);
        ArrayProcessing arr = new ArrayProcessing(logger);
        InputConverter convert = new InputConverter(logger);


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



