using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Product
    {
        private string name;
        private double price;

        public Product(string name, double price = 0)
        {
            this.name = name;
            this.price = price;
        }

        public void ReducePrice(double amount)
        {
            CheckAmount(amount);
            price = Math.Max(0, price - amount);
        }

        public void CheckAmount(double amount)
        {
            if (amount < 0)
                throw new ArgumentException("Число повинне бути додатнім.", nameof(amount));
        }

        public void Display()
        {
            Console.WriteLine($"Товар: {name}, Цена: {price:F2}");
        }
    } 
}
