using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaidukovPSBstudyCalculator
{
    public class JsonLogger : Logger, ILogger
    {

        public string ReadMessage()
        {
            throw new NotImplementedException();
        }

        public void LogMathOperation(double firstNumber, double secondNumber, char mathOperator, double tempResult)
        {
            throw new NotImplementedException();
        }

        public void SendMessage(LogMessage m)
        {
            throw new NotImplementedException();
        }

        public void LogResult(LogMessage m, double result)
        {
            throw new NotImplementedException();
        }

        public void LogString(LogMessage m, string str)
        {
            throw new NotImplementedException();
        }

        public void SendMessage(string m)
        {
            throw new NotImplementedException();
        }
    }
}
