using System;
using System.Collections.Generic;

interface ISubscription
{
    double MonthlyPay { get; }
    int MinPeriod { get; }
    List<string> Channels { get; }
    List<string> Features { get; }
    void ShowDetails();
}

abstract class BaseSubscription : ISubscription
{
    public double MonthlyPay { get; }
    public int MinPeriod { get; }
    public List<string> Channels { get; }
    public List<string> Features { get; }

    protected BaseSubscription(double monthlyPay, int minPeriod, List<string> channels, List<string> features)
    {
        MonthlyPay = monthlyPay;
        MinPeriod = minPeriod;
        Channels = channels;
        Features = features;
    }

    public void ShowDetails()
    {
        Console.WriteLine($"{GetType().Name}\n" + $"Monthly pay: ${MonthlyPay}\n" +
$"Min Period: {MinPeriod} months\n" + $"Channels: {string.Join(", ", Channels)}\n" +
$"Features: {string.Join(", ", Features)}\n");
    }
}

class DomesticSubscription : BaseSubscription
{
    public DomesticSubscription() : base(100, 6, new List<string> { "News", "Sports" }, new List<string> { "HD Quality"}) { }
}

class EducationalSubscription : BaseSubscription
{
    public EducationalSubscription() : base(180, 3, new List<string> { "Documentaries", "Science", "Kids" }, new List<string> {"Offline Access" }) { }
}

class PremiumSubscription : BaseSubscription
{
    public PremiumSubscription() : base(200, 12, new List<string> { "All Channels" }, new List<string> { "4K Streaming", "Exclusive Content" }) { }
}

interface ISubscriptionFactory
{
    ISubscription CreateSubscription(string type);
}

class WebSite : ISubscriptionFactory
{
    public ISubscription CreateSubscription(string type)
    {
        if (type == "domestic")
            return new DomesticSubscription();
        else if (type == "educational")
            return new EducationalSubscription();
        else if (type == "premium")
            return new PremiumSubscription();
        else
            throw new ArgumentException("Unknown subscription type");
    }
}

class MobileApp : ISubscriptionFactory
{
    public ISubscription CreateSubscription(string type)
    {
        return new WebSite().CreateSubscription(type);
    }
}

class ManagerCall : ISubscriptionFactory
{
    public ISubscription CreateSubscription(string type)
    {
        return new WebSite().CreateSubscription(type);
    }
}

class Program
{
    static void Main()
    {
        ISubscriptionFactory website = new WebSite();
        ISubscriptionFactory mobileApp = new MobileApp();
        ISubscriptionFactory managerCall = new ManagerCall();

        ISubscription sub1 = website.CreateSubscription("domestic");
        ISubscription sub2 = mobileApp.CreateSubscription("educational");
        ISubscription sub3 = managerCall.CreateSubscription("premium");

        sub1.ShowDetails();
        sub2.ShowDetails();
        sub3.ShowDetails();
    }
}
