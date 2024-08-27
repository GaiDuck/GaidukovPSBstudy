using GaidukovPSBstudyCalculator;

internal class Program
{
    private static void Main(string[] args)
    {
        AdditionalFunctions.Greeting();
        
        MainFunctoins main = new MainFunctoins();

        bool modeIsCorrect;

        do
        {
            Console.WriteLine("1 - пошаговый, 2 - строкой, 3 - массив от пользователя, 4 - генерация массива");

            modeIsCorrect = false;

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1: //калькулятор, считающий по шагам
                        main.CalculatingStepByStep();
                        modeIsCorrect = true;
                        break;

                    case ConsoleKey.D2:  //калькулятор, считающий из строки
                        main.CalculatingByString();
                        modeIsCorrect = true;
                        break;

                    case ConsoleKey.D3: //обработка массива, вводимого пользователем
                        main.SeachForNumbersInArray("user");
                        modeIsCorrect = true;
                        break;

                    case ConsoleKey.D4: //обработка массива, заполненного случайными числами
                        main.SeachForNumbersInArray("auto");
                        modeIsCorrect = true;
                        break;

                    default:
                        Console.WriteLine("Эта функция находится в разработке, попробуйте воспользоваться другой функцией.");
                        break;
                }
        }
        while (!AdditionalFunctions.Exit());
    }
}



