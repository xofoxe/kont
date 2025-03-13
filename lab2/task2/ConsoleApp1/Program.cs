using System;

interface IDevice1
{
    string GetDeviceType();
}
interface IDevice2
{
    string GetDeviceType();
}

class Laptop : IDevice1
{
    public string GetDeviceType() => "Laptop";
}

class Netbook : IDevice1
{
    public string GetDeviceType() => "Netbook";
}

class EBook : IDevice2
{
    public string GetDeviceType() => "EBook";
}

class Smartphone : IDevice2
{
    public string GetDeviceType() => "Smartphone";
}

interface IDeviceFactory
{
    IDevice1 CreateLaptop();
    IDevice1 CreateNetbook();
    IDevice2 CreateEBook();
    IDevice2 CreateSmartphone();
}

class IProneFactory : IDeviceFactory
{
    public IDevice1 CreateLaptop()=>new Laptop();    
    public IDevice1 CreateNetbook() => new Netbook();
    public IDevice2 CreateEBook() => new EBook();
    public IDevice2 CreateSmartphone() => new Smartphone();
}

class KiaomiFactory : IDeviceFactory
{
    public IDevice1 CreateLaptop() => new Laptop();
    public IDevice1 CreateNetbook() => new Netbook();
    public IDevice2 CreateEBook() => new EBook();
    public IDevice2 CreateSmartphone() => new Smartphone();
}

class BalaxyFactory : IDeviceFactory
{
    public IDevice1 CreateLaptop() => new Laptop();
    public IDevice1 CreateNetbook() => new Netbook();
    public IDevice2 CreateEBook() => new EBook();
    public IDevice2 CreateSmartphone() => new Smartphone();
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
