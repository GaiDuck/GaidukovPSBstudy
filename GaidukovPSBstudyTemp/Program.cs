using System.Linq;
using System.Text.RegularExpressions;

/*
string input = "(1,0+2,3)*3,6-4,8/5,92 qwerty йцукен ... !!! ::: ;;; ===  11 ___ --- {} [] <> ???";

string numbers = "0-9,";
string letters = "A-Za-zА-Яа-я ";
string punctuation = "`|'|\"|:|;|.|!|?|\\|~|_";
string brackets = "|[|<|{|]|>|}|";
string simbols = "@#№$&%=";
string mathOperators = @"(/)|(\-)|(\*)|(\+)|(\^)|(\()|(\))";

string replacePattern = $"[{punctuation}]";
string tempInput = Regex.Replace(input, replacePattern, "#");

Console.WriteLine(tempInput);
*/

/*
 *
 * {numbers}{letters}{simbols}{mathOperators}
 * 
string replacePattern = "[A-Za-zА-Яа-я .!\"\'@#№;$%:?&=`~<>]";
string splitPattern = @"(/)|(-)|(\*)|(\+)|(\^)|(\()|(\))";

string tempInput = Regex.Replace(input, replacePattern, "");
string[] userInput = Regex.Split(tempInput, splitPattern);

foreach (var match in userInput)
{
    Console.WriteLine(match);
}
*/

/*foreach (string i in splitedInput)
{
    bool parced = double.TryParse(i, out var result);
    if (parced)
        Console.WriteLine(result);
    if (mathOperators.Contains(i))
        Console.WriteLine(i);
}*/

//;


/*
using System.Text.RegularExpressions;

string input = "((1,0+2,1)*3,7-4,9)^5,2/6,4";

Console.WriteLine(input + "\n");

string[] outputNumbers = input.Split
string[] outputOperators = input.Split('0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',', '.', ' ');

List<double> numbers = new List<double>();
List<char> operators = new List<char>();

foreach (string simbol in outputNumbers)
{
    bool parced = double.TryParse(simbol, out var result);
    if (parced)
        numbers.Add(result);
}

foreach (string simbol in outputOperators)
{
    char[] chars = simbol.ToCharArray();
    foreach (char c in chars)
        operators.Add(c);
}



foreach (char oper in operators)
    Console.Write(oper + " ");

Console.WriteLine("\n");

foreach (double num in numbers)
    Console.Write(num + " ");
*/



