using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaidukovPSBstudyCalculator
{
    public interface ILogger
    {
        void SendMessage(LogMessage m);
        void SendMessage(string m);
        void LogMathOperation(double firstNumber, double secondNumber, char mathOperator, double tempResult);
        void LogResult(LogMessage m, double result);
        void LogString(LogMessage m, string str);
        string ReadMessage();
        string ReadMessage(string input);
        string ReadDigitsOnly();
    }
}
