using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IWarehouseRepository warehouseRepo = new WarehouseRepository();
            Reporting reporting = new Reporting(warehouseRepo);

            Warehouse product1 = new Warehouse("Молоко", "літр", 25.50, 0, DateTime.Now);
            reporting.RegisterArrival(product1, 100);

            Warehouse product2 = new Warehouse("Хліб", "штука", 12.75, 0, DateTime.Now);
            reporting.RegisterArrival(product2, 50);

            reporting.InventoryReport();

            reporting.RegisterShipment("Молоко", 20);
            reporting.InventoryReport();
        }
    }
}
