using System;

namespace Cw6
{
    public class Zad3
    {
        public static void Main3(string[] args)
        {
            Customer janKowalski = new Customer("Jan Kowalski");
            janKowalski.GetType().GetProperty("Address").SetValue(janKowalski, "Krucza 13");
            janKowalski.GetType().GetProperty("SomeValue").SetValue(janKowalski, 18);
            
            Console.WriteLine("Name: {0}\nAddress: {1}\nSomeValue: {2}", janKowalski.Name, janKowalski.Address, janKowalski.SomeValue);
        }
    }
}