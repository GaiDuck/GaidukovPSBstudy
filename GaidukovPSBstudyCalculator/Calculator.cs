﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaidukovPSBstudyCalculator
{
    /// <summary>
    /// Класс для выполнения базовых математических операций, проверки корректности запрашиваемых пользователем математических операций и 
    /// вывода в консоль промежуточных значений.
    /// </summary>
    internal class Calculator
    {
        /// <summary>
        /// Промежуточный результат вычислений.
        /// </summary>
        public double TempResult { get; private set; }
        
        public Calculator()
        {

        }

        /// <summary>
        /// Метод принимает два числа и возвращает их сумму.
        /// </summary>
        /// <param name="firstNumber"></param>
        /// <param name="secondNumber"></param>
        /// <returns>FirstNumber + SecondNumber </returns>
        double Add(double firstNumber, double secondNumber)
        {
            TempResult = firstNumber + secondNumber;
            return TempResult;
        }

        /// <summary>
        /// Метод принимает два числа и возвращает их разность.
        /// </summary>
        /// <param name="firstNumber"></param>
        /// <param name="secondNumber"></param>
        /// <returns>FirstNumber - SecondNumber </returns>
        double Sub(double firstNumber, double secondNumber)
        {
            TempResult = firstNumber - secondNumber;
            return TempResult;
        }

        /// <summary>
        /// Метод принимает два числа и возвращает их произведение.
        /// </summary>
        /// <param name="firstNumber"></param>
        /// <param name="secondNumber"></param>
        /// <returns>FirstNumber * SecondNumber </returns>
        double Mult(double firstNumber, double secondNumber)
        {
            TempResult = firstNumber * secondNumber;
            return TempResult;
        }

        /// <summary>
        /// Метод принимает два числа и возвращает их частное.
        /// </summary>
        /// <param name="firstNumber"></param>
        /// <param name="secondNumber"></param>
        /// <returns>FirstNumber / SecondNumber </returns>
        double Div(double firstNumber, double secondNumber)
        {
            TempResult = firstNumber / secondNumber;
            return TempResult;
        }

        /// <summary>
        /// Метод принимает два числа и возвращает число в степени.
        /// </summary>
        /// <param name="firstNumber"></param>
        /// <param name="secondNumber"></param>
        /// <returns>FirstNumber ^ SecondNumber </returns>
        double Pow(double firstNumber, double secondNumber)
        {
            TempResult = Math.Pow(firstNumber, secondNumber);
            return TempResult;
        }

        /// <summary>
        /// Метод принимает два числа и знак математического оператора. 
        /// На основании полученных данных вызывает метод, который проверяет корректность операции, затем вызывает метод, выполняющий операцию.
        /// </summary>
        /// <param name="mathOperator"></param>
        /// <param name="firstNumber"></param>
        /// <param name="secondNumber"></param>
        public void Calculate(char mathOperator, double firstNumber, double secondNumber)
        {
            if (Validate(firstNumber, secondNumber, mathOperator))
            {
                switch (mathOperator)
                {
                    case '+':
                        Add(firstNumber, secondNumber);
                    break;

                    case '-':
                        Sub(firstNumber, secondNumber);
                    break;

                    case '*':
                        Mult(firstNumber, secondNumber);
                    break;

                    case '/':
                        Div(firstNumber, secondNumber);
                    break;

                    case '^':
                        Pow(firstNumber, secondNumber);
                    break;

                    default:
                        AdditionalFunctions.EnterIncorrectData();
                    break;
                }
            }
            Logger(mathOperator, firstNumber, secondNumber);
        }

        /// <summary>
        /// Метод, проверяющий корректность математической операции. 
        /// Возвращает true, если операция может быть выполнена, возвращает false, если операция не может быть выполнена. 
        /// </summary>
        /// <param name="firstNumber"></param>
        /// <param name="secondNumber"></param>
        /// <param name="mathOperator"></param>
        /// <returns></returns>
        bool Validate(double firstNumber, double secondNumber, char mathOperator)
        {
            bool valid = false;

            if (mathOperator == '/' && secondNumber == 0)
            {
                AdditionalFunctions.EnterIncorrectData();
                Console.WriteLine("Обнаружено деление на ноль, операция не может быть выполнена.");
            }
            else if (mathOperator == '^' && firstNumber < 0 && secondNumber > -1 && secondNumber < 1)
            {
                AdditionalFunctions.EnterIncorrectData();
                Console.WriteLine("Обнаружено взятие корняя из отрицательного числа, операция не может быть выполнена.");
            }
            else
            {
                valid = true;
            }
            return valid;
        }

        /// <summary>
        /// Метод, выводящий в консоль математическую операцию и ее результат, округленный до 4 знаков после запятой.
        /// </summary>
        /// <param name="mathOperator"></param>
        /// <param name="firstNumber"></param>
        /// <param name="secondNumber"></param>
        void Logger(char mathOperator, double firstNumber, double secondNumber)
        {   
            Console.WriteLine($"{Math.Round(firstNumber, 4)} {mathOperator} {Math.Round(secondNumber, 4)} = {Math.Round(TempResult, 4)}");
        }
    }
}
