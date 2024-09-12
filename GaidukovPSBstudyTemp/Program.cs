using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;




        sortedProduct = product.OrderBy(s => s.Cost);




/*
var weatherForecast = new WeatherForecast
{
    Date = DateTime.Parse("2019-08-01"),
    TemperatureCelsius = 25,
    Summary = "Hot"
};

string fileName = "WeatherForecast.json";
string jsonString = JsonSerializer.Serialize(weatherForecast);
File.WriteAllText(fileName, jsonString);

Console.WriteLine(File.ReadAllText(fileName));

public class WeatherForecast
{
    public DateTimeOffset Date { get; set; }
    public int TemperatureCelsius { get; set; }
    public string? Summary { get; set; }
}

*/






/*
// сохранение данных
using (FileStream fs = new FileStream(@"GaidukovPSBstudyCalculator\user.json", FileMode.OpenOrCreate))
{
Person tom = new Person("Tomas", 37);
await JsonSerializer.SerializeAsync<Person>(fs, tom);
Console.WriteLine("Data has been saved to file");
}


// чтение данных
using (FileStream fs = new FileStream(@"GaidukovPSBstudyCalculator\user.json", FileMode.OpenOrCreate))
{
Person? person = await JsonSerializer.DeserializeAsync<Person>(fs);
Console.WriteLine($"Name: {person?.Name}  Age: {person?.Age}");
}

class Person
{
    public string Name { get; }
    public int Age { get; set; }
    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
}
*/

/*
string[] array = { "kjd456", "dkj503", "abs123", "abs234", "jfh123" };
Array.Sort(array);

foreach (var item in array)
    Console.WriteLine(item);
*/

/*
Random random = new Random();

for (int i = 0; i < 10; i++)
{
    int a = random.Next(0, 3);
    Console.WriteLine(a);
}
*/

/*
char[] chars = new char[7];
int j = 4;

for (int i = 0; i < chars.Length - j; i++)
{
    chars[i] = Convert.ToChar(random.Next(65, 90));
}

for (int i = chars.Length - j; i < chars.Length; i++)
{
    chars[i] = Convert.ToChar(random.Next(48, 57));
}

string s = new string(chars);
Console.WriteLine(s);
*/

/*
char c = Convert.ToChar(65);
Console.WriteLine(c);
*/


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

foreach (var match in userInput)cha
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



