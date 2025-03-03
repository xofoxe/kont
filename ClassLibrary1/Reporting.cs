using System;
using System.Collections.Generic;

namespace ClassLibrary1
{
    public class Reporting
    {
        private readonly IWarehouseRepository warehouseRepository;

        public Reporting(IWarehouseRepository warehouseRepository)
        {
            this.warehouseRepository = warehouseRepository;
        }

        public void RegisterArrival(Warehouse product, int quantity)
        {
            warehouseRepository.AddProduct(product, quantity);
        }

        public void RegisterShipment(string productName, int quantity)
        {
            warehouseRepository.RemoveProduct(productName, quantity);
        }

        public void InventoryReport()
        {
            Console.WriteLine("\nЗвіт по складу:");
            var products = warehouseRepository.GetAllProducts();

            if (products.Count == 0)
            {
                Console.WriteLine("Склад пустий.");
                return;
            }

            foreach (var item in products)
            {
                Console.WriteLine($"Товар: {item.Name}, Кількість: {item.Quantity}, Ціна: {item.Price}, Дата завозу: {item.LastDeliveryDate}");
            }
        }
    }
}
