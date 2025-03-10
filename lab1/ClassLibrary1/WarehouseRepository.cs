using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private List<Warehouse> inventory = new List<Warehouse>();
        public void AddProduct(Warehouse product, int quantity)
        {
            var existingProduct = GetProduct(product.Name);
            if (existingProduct != null)
            {
                existingProduct.Quantity += quantity;
                existingProduct.LastDeliveryDate = DateTime.Now;
            }
            else
            {
                product.Quantity = quantity;
                product.LastDeliveryDate = DateTime.Now;
                inventory.Add(product);
            }
        }
        public void RemoveProduct(string productName, int quantity)
        {
            var product = GetProduct(productName);
            ValidateProduct(product, quantity);
            product.Quantity -= quantity;
        }

        private void ValidateProduct(Warehouse product, int quantity)
        {
            if (product == null)
                throw new InvalidOperationException("Товар не знайдено на складі.");
            if (product.Quantity < quantity)
                throw new InvalidOperationException("Недостатньо товарів на складі.");
        }
        public Warehouse GetProduct(string productName)
        {
            return inventory.Find(p => p.Name == productName);
        }
        public List<Warehouse> GetAllProducts()
        {
            return inventory;
        }
    }
}