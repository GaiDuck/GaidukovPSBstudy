﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GaidukovPSBstudyCalculator
{
    public class ConsoleLogger : Logger, ILogger
    {
        public string ReadMessage()
        {
            return Console.ReadLine();
        }

        public string ReadMessage(string input)
        {
            return input;
        }

        public void LogMathOperation(double firstNumber, double secondNumber, char mathOperator, double tempResult)
        {
            string message = $"{Math.Round(firstNumber, 4)} {mathOperator} {Math.Round(secondNumber, 4)} = {Math.Round(tempResult, 4)}";
            Console.WriteLine(message);
        }

        public void SendMessage(LogMessage m)
        {
            Console.WriteLine(LoggerMessage(m));
        }

        public void SendMessage(string str)
        {
            Console.WriteLine(str);
        }

        public void LogResult(LogMessage m, double result)
        {
            string message = LoggerMessage(m) + result.ToString();
            Console.WriteLine(message);
        }
        public void LogString(LogMessage m, string str)
        {
            string message = LoggerMessage(m) + str;
            Console.Write(message);
        }

        public string ReadDigitsOnly()
        {
            string input = Regex.Replace(Console.ReadLine(), @"[^\d]", "");
            
            if (input != "")
            {
                return input;
            }

            Console.WriteLine("Выбранно значение по умолчанию: 1");
            return "1";
        }
    }
}
