using System;
using System.Threading;

public class Authenticator
{
    private static Authenticator instance;
    private static readonly object lockObject = new object();

    private Authenticator()
    {
        Console.WriteLine("Authenticator instance created");
    }

    public static Authenticator GetInstance()
    {
        if (instance == null)
        {
            lock (lockObject)
            {
                if (instance == null)
                {
                    instance = new Authenticator();
                }
            }
        }
        return instance;
    }

    public void info()
    {
        Console.WriteLine("Hello!");
    }
}

class Program
{
    static void Main()
    {
        Authenticator foo = Authenticator.GetInstance();
        foo.info();
       
        Authenticator bar = Authenticator.GetInstance();
        bar.info();
    }
}
