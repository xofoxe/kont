using System;

namespace ClassLibrary1
{
    public class Warehouse
    {
        public string Name { get; set; }
        public string Unit { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public DateTime LastDeliveryDate { get; set; }

        public Warehouse(string name, string unit, double price, int quantity, DateTime lastDeliveryDate)
        {
            Name = name;
            Unit = unit;
            Price = price;
            Quantity = quantity;
            LastDeliveryDate = lastDeliveryDate;
        }
    }
}
