
using System; 

public interface Laptop
{
    public string GetDeviceType();
}

public class IProneLaptop : Laptop
{
    public string GetDeviceType() => "IProneLaptop";
}

public class KiaomiLaptop : Laptop
{
    public string GetDeviceType() => "KiaomiLaptop";
}
 
public class BalaxyLaptop : Laptop
{
    public string GetDeviceType() => "BalaxyLaptop";
}
 
 
public interface Netbook
{
    string GetDeviceType();
}

class IProneNetbook : Netbook
{
    public string GetDeviceType() => "IProneNetbook";
}

class KiaomiNetbook : Netbook
{
    public string GetDeviceType() => "KiaomiNetbook";
}
 
class BalaxyNetbook : Netbook
{
    public string GetDeviceType() => "BalaxyNetbook";
}
 
 
interface EBook
{
    string GetDeviceType();
}

class IProneEBook : EBook
{
    public string GetDeviceType() => "IProneEBook";
}

class KiaomiEBook : EBook
{
    public string GetDeviceType() => "KiaomiEBook";
}
 
class BalaxyEBook : EBook
{
    public string GetDeviceType() => "BalaxyEBook";
}

interface Smartphone
{
    string GetDeviceType();
}

class IProneSmartphone : Smartphone
{
    public string GetDeviceType() => "IProneSmartphone";
}

class KiaomiSmartphone : Smartphone
{
    public string GetDeviceType() => "KiaomiSmartphone";
}
 
class BalaxySmartphone : Smartphone
{
    public string GetDeviceType() => "BalaxySmartphone";
}


interface IDeviceFactory
{
  public  Laptop CreateLaptop();
   public Netbook CreateNetbook();
   public EBook CreateEBook();
   public Smartphone CreateSmartphone();
}

class IProneFactory : IDeviceFactory
{
   public Laptop CreateLaptop()=> new IProneLaptop();
   public Netbook CreateNetbook()=> new IProneNetbook();
   public EBook CreateEBook()=> new IProneEBook();
   public Smartphone CreateSmartphone()=> new IProneSmartphone();
}

class KiaomiFactory : IDeviceFactory
{
     public  Laptop CreateLaptop() => new KiaomiLaptop();
   public Netbook CreateNetbook()=> new KiaomiNetbook();
   public EBook CreateEBook()=> new KiaomiEBook();
   public Smartphone CreateSmartphone()=> new KiaomiSmartphone();
}

class BalaxyFactory : IDeviceFactory
{
     public  Laptop CreateLaptop() => new BalaxyLaptop();
   public Netbook CreateNetbook()=> new BalaxyNetbook();
   public EBook CreateEBook()=> new BalaxyEBook();
   public Smartphone CreateSmartphone()=> new BalaxySmartphone();
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
