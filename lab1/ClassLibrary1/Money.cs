using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Money
    {
        private int wholePart;  
        private int fractionalPart;
        private const int FractionalUnit = 100;

        public Money(int whole, int fractional)
        {
            SetMoney(whole, fractional);
        }

        public void SetMoney(int whole, int fractional)
        {
            if (whole < 0 || fractional < 0)
               return;

            wholePart = whole + fractional / FractionalUnit;
            fractionalPart = fractional % FractionalUnit;
        }

        public void Display()
        {
            Console.WriteLine($"Сумма: {wholePart}.{fractionalPart:D2}");
        }

        public int GetWholePart() => wholePart;
        public int GetFractionalPart() => fractionalPart;
    }
}
