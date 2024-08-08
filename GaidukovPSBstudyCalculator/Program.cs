using GaidukovPSBstudyCalculator;

Calculating calc = new Calculating();
InputData input = new InputData();

AdditionalFunctions.Greeting();

bool modeIsCorrect;

do
{
    Console.WriteLine("1 - пошаговый, 2 - строкой");

    modeIsCorrect = false;

    bool parsed = byte.TryParse(Console.ReadLine(), out var mode);

    if (parsed)
    {
        switch (mode)
        {
            case 1:
                CalculatingStepByStep();
                modeIsCorrect = true;
                break;

            case 2:  //калькулятор, считающий из строки
                input.StringInput();
                do
                {
                    input.GetBrackets();
                    CalculatingByString();
                    input.SplitedInputRemoveBracket(calc.TempResult, input.BracketIsFound);
                    if (input.MathOperatorCount == 0)
                        break;
                    input.SetBracketIndexes();
                } while (input.BracketIsFound); // не хватает перезаписи значения скобок

                input.SetExpressionAfterOpenBrackets();
                CalculatingByString();

                modeIsCorrect = true;
                break;

            default:
                Console.WriteLine("Эта функция находится в разработке, попробуйте воспользоваться другой функцией.");
                break;
        }  
    }
}
while (!modeIsCorrect);

void CalculatingStepByStep()  //калькулятор с пошаговым рассчестом
{
    input.StringInput();
    input.GetDataV1();
    calc.Calculate(input.MathOperator, input.FirstNumber, input.SecondNumber);
}

void CalculatingFromString(int i)
{ 
    calc.Calculate(input.MathOperator, input.FirstNumber, input.SecondNumber);
    input.UpdateExpression(calc.TempResult, i); 
}

void CalculatingByString()      //Приоритеты выполнения операций:
{                               //Возведение в степень -> Умножение и деление -> Сложение и вычитание
    int i = input.MathOperatorCount;


    for (int a = i - 1; a >= 0; a--) //цикл для вычисления степеней
    {
        input.SetNumbersByMathoperator(a);
        if (input.MathOperator == '^')
        {
            CalculatingFromString(a);
            i--;
        }
    }

    for (int b = i - 1; b >= 0; b--) //цикл для вычисления умножений и делений
    {
        input.SetNumbersByMathoperator(b);
        if (input.MathOperator == '*' || input.MathOperator == '/')
        {
            CalculatingFromString(b);
            i--;
        }
    }

    for (int c = 0; c < i;) //цикл для вычисления сложений и вычитаний
    {
        input.SetNumbersByMathoperator(c);

        if (input.MathOperator == '+' || input.MathOperator == '-')
        {
            CalculatingFromString(c);
            i--;
        }
        else
            c++;
    } 
}


