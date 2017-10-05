using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace helloworld
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var name = Ask();
            var visitors = Load();

            var (greeting, visitors_) = Process(name, visitors);
            
            Store(visitors_);
            Display(greeting);
        }


        public static (string greeting, string[] visitors) Process(string name, string[] visitors) {
            visitors = visitors.Concat(new[] {name}).ToArray();
            var n = visitors.Count(v => v == name);
            var greetingTemplate = Map(n);
            var greeting = string.Format(greetingTemplate, name);
            return (greeting, visitors);

            string Map(int m) {
                switch (m) {
                    case 1: return "Hello, {0}!";
                    case 2: return "Welcome back, {0}!";
                    case 3: return "Hello my good friend, {0}!";
                    default: return "Nice to see you again, {0}!";
                }
            }
        }


        public static string Ask() {
            Console.Write("Name: ");
            return Console.ReadLine();
        }

        public static void Display(string greeting) {
            Console.WriteLine(greeting);
        }


        public static string[] Load() {
            if (!File.Exists("visitors.txt")) return new string[0];
            return File.ReadAllLines("visitors.txt").Where(v => !string.IsNullOrEmpty(v)).ToArray();
        }

        public static void Store(string[] visitors) {
            File.WriteAllLines("visitors.txt", visitors);
        }
    }
}