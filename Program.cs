using System;

namespace DelegateConsoleApp
{
    delegate int Calculation(int a, int b);
    delegate int Calculation2(int a, int b);
    public delegate void valueChangedHandler();


    internal class Program
    {
        public static int Addition(int a, int b)
        {
            Console.WriteLine("Addition: {0}", a + b);
            return (a + b);
        }

        public static int Subtraction(int a, int b)
        {
            Console.WriteLine("Subtraction: {0}", a - b);
            return (a - b);
        }

        public static int Multiplication(int a, int b)
        {
            Console.WriteLine("Multiplication: {0}", a * b);
            return (a * b);
        }
        public static void subscriber()
        {
            Console.WriteLine("in the subscriber");
        }
        static void Main(string[] args)
        {
            Calculation calObj = new(Addition);
            Console.WriteLine("{0} : {1}", calObj.Method, calObj(30, 20));
            calObj = Subtraction;
            Console.WriteLine("{0} : {1}", calObj.Method, calObj(30, 20));
            calObj = Multiplication;
            Console.WriteLine("{0} : {1}", calObj.Method, calObj(30, 20));

            //Multicast delegate
            Console.WriteLine("===========================");
            Calculation2 multiCalc = new(Addition);
            multiCalc += Subtraction;
            multiCalc += Multiplication;
            Console.WriteLine("{0} : {1}", calObj.Method, multiCalc(40, 30));

            //events
            Console.WriteLine("===========================");

            ChangeEventClass c1 = new();
            c1.valueChanged += subscriber;
            Console.WriteLine("value change: {0} ", c1.changeMyValue(89));
            Console.WriteLine("value change: {0} ", c1.changeMyValue(56));
            Console.WriteLine("value change: {0} ", c1.changeMyValue(78));

        }

        private static void C1_valueChanged()
        {
            throw new NotImplementedException();
        }
    }


    public class ChangeEventClass
    {
        public int myValue { get; set; }
        public event valueChangedHandler valueChanged;

        public int changeMyValue(int value)
        {
                myValue = value;
                Console.WriteLine("About to fire");
                valueChanged?.Invoke();
                return myValue;
        }
    }
}