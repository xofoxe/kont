using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public interface IWarehouseRepository
    {
        void AddProduct(Warehouse product, int quantity);
        void RemoveProduct(string productName, int quantity);
        Warehouse GetProduct(string productName);
        List<Warehouse> GetAllProducts();
    }
}
