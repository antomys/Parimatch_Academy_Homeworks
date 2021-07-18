using System;
using System.Collections.Generic;
using System.Linq;

namespace Task_1._4
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Check brackets for pair, Volokhovych");
            Task task = new Task();
            //Console.WriteLine(exp);
            task.Start();
        }
    }

    internal class Task
    {
        private string Expression { get; set; }

        public void Start()
        {
            try
            {
                Console.WriteLine("\n\nPlease enter expression\n To Exit write \"exit\"");
                Expression = Console.In.ReadLine();
                if (Expression?.Length >= 100)
                    throw new Exception("Exceeded length, try again");
                if (String.IsNullOrEmpty(Expression))
                    throw new Exception("Empty input. Try again");
                if(Expression.ToLower().Equals("exit"))
                    Environment.Exit(0);
                Checking();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Start();
            }
        }

        private void Checking()
        {
            try
            {
                Stack<Char> stack = new Stack<char>();
                for (int i = 0; i < Expression.Length; i++) 
                { 
                    var ch = Expression[i]; 
                    switch (ch) 
                    { 
                        case '{':
                            stack.Push(ch);
                            break;
                        case '(':
                            stack.Push(ch);
                            break;
                        case '<':
                            stack.Push(ch);
                            break;
                        case '[':
                            stack.Push(ch);
                            break;
                        case '}':
                            if(stack.Count==0)
                                throw new Exception($"\nThis {ch} has no pair at index {i}");
                            if (stack.Peek() == '{')
                                stack.Pop();
                            else
                            {
                                throw new Exception($"\nThis {ch} has no pair at index {i}");
                            }
                            break;
                        case ')':
                            if(stack.Count==0)
                                throw new Exception($"\nThis {ch} has no pair at index {i}");
                            if (stack.Peek() == '(')
                                stack.Pop();
                            else
                            {
                                throw new Exception($"\nThis {ch} has no pair at index {i}");
                            }
                            break;
                        case '>':
                            if(stack.Count==0)
                                throw new Exception($"\nThis {ch} has no pair at index {i}");
                            if (stack.Peek() == '<')
                                stack.Pop();
                            else
                            {
                                throw new Exception($"\nThis {ch} has no pair at index {i}");
                            }
                            break;
                        case ']':
                            if(stack.Count==0)
                                throw new Exception($"\nThis {ch} has no pair at index {i}");
                            if (stack.Peek() == '[')
                                stack.Pop();
                            else
                            {
                                throw new Exception($"\nThis {ch} has no pair at index {i}");
                            }
                            break;
                    } 
                }
                if(stack.Count>0)
                    for (int i = 0; i < stack.Count; i++)
                    {
                        throw new Exception($"\nThis {stack.ElementAt(i)} has no pair at index {i}");
                    }
                if(stack.Count==0)
                    throw new Exception($"\nExpression is right");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Start();
            }
            
        }
    }
}