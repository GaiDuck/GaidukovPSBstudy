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
            Console.WriteLine("1 - пошаговый, 2 - строкой");

            modeIsCorrect = false;

            bool parsed = char.TryParse(Console.ReadLine(), out var mode);

            if (parsed)
            {
                switch (mode)
                {
                    case '1':
                        main.CalculatingStepByStep();
                        modeIsCorrect = true;
                        break;

                    case '2':  //калькулятор, считающий из строки
                        main.CalculatingByString();
                        modeIsCorrect = true;
                        break;

                    default:
                        Console.WriteLine("Эта функция находится в разработке, попробуйте воспользоваться другой функцией.");
                        break;
                }
            }
            else
                AdditionalFunctions.EnterIncorrectData();
        }
        while (!AdditionalFunctions.Exit());
    }
}



