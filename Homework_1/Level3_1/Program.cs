using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

/*
 * Нет обработки неверного ввода
Выходит из программы, после окончания одного выражения,а не после команды выхода
2x3 / 2 \ 3 - не расчитываются
-3--2 - не расчитываются в командом режиме
 */

//todo:bugfix

namespace Level3_1
{
    internal static class Program
    {
        private static void Help()
        {
            Console.WriteLine("This is simple calculator. Input without spaces.\nAvailable commands:\n" +
                              "1. + Example: 5+2 3+2\n" +
                              "2. - Example: 5-2 3-2\n" +
                              "3. % - Remainder \n" +
                              "4. ^ Binary bitwise XOR\n" +
                              "5. | Binary bitwise OR\n" +
                              "6. & Binary bitwise AND\n" +
                              "7. ! Unary bitwise. ONLY one number. Ex: 5!, 24!\n" +
                              "8. Echo mode\n");
        }

        private static int Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                try
                {
                    Console.WriteLine("Calculator,Volokhovych\n");
                    Console.WriteLine("Rules:\n" +
                                      "Write expression (5+2,5+3) and press ENTER\n" +
                                      "Available commands:\n" +
                                      "exit - exits from program\n" +
                                      "help - shows rules and available operations. GLHF!\n");
                    

                        var numbers = new List<double>();
                        var operations = new List<string>();
                        var expr = Console.In.ReadLine();
                        if (expr.ToLower() == "exit")
                            Environment.Exit(0);
                        if (expr.ToLower() == "help")
                        {
                            Help();
                            do
                            {
                                expr = Console.In.ReadLine();
                            } while (expr == "help");
                        }
                        InputNumbers(numbers, expr);
                        foreach (var variable in numbers)
                        {
                            expr = expr.Replace(variable.ToString(), "");
                        }

                        InputOperators(operations, expr);
                        if (numbers.Count > 2 && operations[0] == "!")
                            throw new InvalidDataException();
                        if (numbers.Count == 1 && operations.Count < 1)
                        {
                            Console.WriteLine(numbers[0]);
                            return 0;
                        }
                        if (operations.Count > 1)
                        {

                            throw new InvalidDataException();
                        }

                        Console.WriteLine(Operations(numbers, operations));
                        
                }
                catch (Exception exception) 
                { 
                    Console.WriteLine($"Exception occured!\n {exception.Message}");
                }
            }
            else
            {
                try
                {
                    string input = String.Concat(args);
                    if (args.Length>1)
                        throw new OverflowException();
                    List<double> numbers = new List<double>();
                    List<string> operations = new List<string>();
                    InputNumbers(numbers, input);
                    foreach (var VARIABLE in numbers)
                    {
                        input = input.Replace(VARIABLE.ToString(), "");
                    }

                    InputOperators(operations, input);
                    if (numbers.Count > 2 && operations[0] == "!")
                    {
                        throw new InvalidDataException();
                    }
                        
                    if (numbers.Count == 1 && operations.Count < 1)
                    {
                        Console.WriteLine(numbers[0]);
                        return 0;
                    }

                    if (operations.Count > 1)
                    {

                        throw new InvalidDataException();
                    }

                    Console.WriteLine(Operations(numbers, operations));
                
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Please consider about using HELP command\n");
                    Console.WriteLine($"Exception occured! {exception.Message}");
                }
            }

            return 0;
        }

        private static List<double> InputNumbers(List<double> numbers, string expr)
        {
            try
            {
                foreach (var match in Regex.Matches(expr, @"-?\d+(?:\.\d+)?")) //@"([0-9]+)"
                {
                    numbers.Add(Double.Parse(match.ToString()));
                }

                if (numbers.Count > 2)
                    throw new IndexOutOfRangeException();
                return numbers;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Please consider about using HELP command\n");
                Console.WriteLine($"Exception Occured! {exception.Message}");
                Environment.Exit(-1);
            }

            return null;
        }

        private static List<string> InputOperators(List<string> operations, string expr)
        {
            try
            {
                foreach (var match in Regex.Matches(expr, @"([*+/\!-^%|&])|(\b(pow)\b)"))
                {

                    operations.Add(item: match.ToString());
                }

                if (operations.Count == 3)
                {
                    if (operations[0] != "-" || operations[2] != "-")
                        throw new InvalidDataException();
                }
                else if (operations.Count > 2)
                {
                    throw new InvalidDataException();
                }
                else if (operations.Count == 2)
                {
                    if (operations[0] == "-" && operations[1] != "-")
                        throw new InvalidDataException();
                }
                

                return operations;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Please consider about using HELP command\n");
                Console.WriteLine($"Exception Occured! {exception.Message}");
                Environment.Exit(-1);
            }

            return null;
        }

        private static double Operations(List<double> numbers, List<string> operations)
        {
            try
            {
                switch (operations[0])
                {
                    case "+":
                        return checked(numbers[0] + numbers[1]);
                    case "-":
                        return checked(numbers[0] - numbers[1]);
                    case "*":
                        return (numbers[0] * numbers[1]);
                    case "/":
                        if (numbers[1] == 0)
                        {
                            throw new DivideByZeroException();
                        }
                        else
                        {
                            return checked(numbers[0] / numbers[1]);
                        }
                    case "\'":
                        if (numbers[1] == 0)
                        {
                            throw new DivideByZeroException();
                        }
                        else
                        {
                            return (numbers[0] / numbers[1]);
                        }
                    case "^":
                        for (int i = 0; i < numbers.Count; i++)
                        {
                            if (numbers[i] % 1 != 0)
                                throw new InvalidDataException();
                        }

                        return checked((int) numbers[0] ^ (int) numbers[1]);
                    case "|":
                        for (int i = 0; i < numbers.Count; i++)
                        {
                            if (numbers[i] % 1 != 0)
                                throw new InvalidDataException();
                        }

                        return checked((int) numbers[0] | (int) numbers[1]);
                    case "&":
                        for (int i = 0; i < numbers.Count; i++)
                        {
                            if (numbers[i] % 1 != 0)
                                throw new InvalidDataException();
                        }

                        return (int) numbers[0] & (int) numbers[1];
                    case "!":
                        double result = 1;
                        while ((int) numbers[0] != 1)
                        {
                            result = result * (int) numbers[0];
                            numbers[0] = (int) numbers[0] - 1;
                        }

                        return checked(result);
                    case "%":
                        return checked(numbers[0] % numbers[1]);
                    case "pow":
                        return checked(Math.Pow(numbers[0], numbers[1]));
                    default:
                        throw new InvalidDataException();
                }

            }
            catch (OverflowException e)
            {
                Console.WriteLine($"Overflow! {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine("Please consider about using HELP command\n");
                Console.WriteLine($"Error occured {e.Message}");
            }

            return -1;
        }
    }
}