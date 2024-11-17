using System;

namespace CalculatorApp
{
    // کلاس پایه
    public abstract class Calculator
    {
        public abstract bool IsTrue(int number); // متد مجازی
        public abstract void Calculate(int number); // متد مجازی
    }

    // کلاس برای اعداد ساده
    public class SimpleCalculator : Calculator
    {
        public override bool IsTrue(int number)
        {
            return number > 0; // بررسی مثبت بودن عدد
        }

        public override void Calculate(int number)
        {
            Console.WriteLine($"SimpleCalculator: Number {number} is {(IsTrue(number) ? "positive." : "not positive.")}");
        }
    }

    // کلاس برای اعداد اول
    public class PrimeCalculator : Calculator
    {
        public override bool IsTrue(int number)
        {
            if (number < 2) return false;
            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }

        public override void Calculate(int number)
        {
            Console.WriteLine($"PrimeCalculator: Number {number} is {(IsTrue(number) ? "prime." : "not prime.")}");
        }
    }

    // کلاس برای اعداد پالینروم
    public class PalindromeCalculator : Calculator
    {
        public override bool IsTrue(int number)
        {
            int reversed = 0, original = number;
            while (number > 0)
            {
                reversed = reversed * 10 + number % 10;
                number /= 10;
            }
            return original == reversed;
        }

        public override void Calculate(int number)
        {
            Console.WriteLine($"PalindromeCalculator: Number {number} is {(IsTrue(number) ? "a palindrome." : "not a palindrome.")}");
        }
    }

    // کلاس کارخانه برای تولید اشیاء
    public static class CalculatorFactory
    {
        public static Calculator CreateCalculator(string type)
        {
            switch (type.ToLower())
            {
                case "simple":
                    return new SimpleCalculator();
                case "prime":
                    return new PrimeCalculator();
                case "palindrome":
                    return new PalindromeCalculator();
                default:
                    return null;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string type;
            int number;

            while (true)
            {
                Console.Write("Enter calculator type (simple, prime, palindrome) or 'esc' to exit: ");
                type = Console.ReadLine();

                if (type.ToLower() == "esc") break;

                var calculator = CalculatorFactory.CreateCalculator(type);
                if (calculator == null)
                {
                    Console.WriteLine("Invalid calculator type!");
                    continue;
                }

                Console.Write("Enter a number: ");
                if (int.TryParse(Console.ReadLine(), out number))
                {
                    calculator.Calculate(number);
                }
                else
                {
                    Console.WriteLine("Please enter a valid integer.");
                }
            }
        }
    }
}
