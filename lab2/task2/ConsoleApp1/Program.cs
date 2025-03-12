using System;

interface IDevice
{
    string GetDeviceType();
}

class Laptop : IDevice
{
    public string GetDeviceType() => "Laptop";
}

class Netbook : IDevice
{
    public string GetDeviceType() => "Netbook";
}

class EBook : IDevice
{
    public string GetDeviceType() => "EBook";
}

class Smartphone : IDevice
{
    public string GetDeviceType() => "Smartphone";
}

interface IDeviceFactory
{
    IDevice CreateLaptop();
    IDevice CreateNetbook();
    IDevice CreateEBook();
    IDevice CreateSmartphone();
}

class IProneFactory : IDeviceFactory
{
    public IDevice CreateLaptop() => new Laptop();
    public IDevice CreateNetbook() => new Netbook();
    public IDevice CreateEBook() => new EBook();
    public IDevice CreateSmartphone() => new Smartphone();
}

class KiaomiFactory : IDeviceFactory
{
    public IDevice CreateLaptop() => new Laptop();
    public IDevice CreateNetbook() => new Netbook();
    public IDevice CreateEBook() => new EBook();
    public IDevice CreateSmartphone() => new Smartphone();
}

class BalaxyFactory : IDeviceFactory
{
    public IDevice CreateLaptop() => new Laptop();
    public IDevice CreateNetbook() => new Netbook();
    public IDevice CreateEBook() => new EBook();
    public IDevice CreateSmartphone() => new Smartphone();
}

class Program
{
    static void Main()
    {
        var brands = new (string, IDeviceFactory)[]
        {
            ("IProne", new IProneFactory()),
            ("Kiaomi", new KiaomiFactory()),
            ("Balaxy", new BalaxyFactory())
        };

        foreach (var (brandName, factory) in brands)
        {
            Console.WriteLine($"\n{brandName} produces:");
            Console.WriteLine($" - {factory.CreateLaptop().GetDeviceType()}");
            Console.WriteLine($" - {factory.CreateNetbook().GetDeviceType()}");
            Console.WriteLine($" - {factory.CreateEBook().GetDeviceType()}");
            Console.WriteLine($" - {factory.CreateSmartphone().GetDeviceType()}");
        }
    }
}
