using System;
using System.Collections.Generic;

class Virus 
{
    public string Name { get; set; }
    public string Type { get; set; }
    public double Weight { get; set; }
    public int Age { get; set; }
    public List<Virus> Children { get; set; }

    public Virus(string name, string type, double weight, int age)
    {
        Name = name;
        Type = type;
        Weight = weight;
        Age = age;
        Children = new List<Virus>();
    }

    public void AddChild(Virus child)
    {
        Children.Add(child);
    }

    public object Clone()
    {
        Virus clonedVirus = new Virus(Name, Type, Weight, Age);
        foreach (var child in Children)
        {
            clonedVirus.AddChild((Virus)child.Clone());
        }
        return clonedVirus;
    }

    public void PrintFamily()
    {
        Console.WriteLine($"Virus: {Name}, Type: {Type}, Weight: {Weight}, Age: {Age}\n");
        foreach (var child in Children)
        {
            child.PrintFamily();
        }
    }
}

class Program
{
    static void Main()
    {
        Virus parent = new Virus("ParentVirus", "TypeA", 1, 3);
        Virus child1 = new Virus("ChildVirus1", "TypeA", 2, 1);
        Virus child2 = new Virus("ChildVirus2", "TypeA", 2, 2);
        Virus grandchild = new Virus("GrandChildVirus", "TypeA", 1, 1);

        child1.AddChild(grandchild);
        parent.AddChild(child1);
        parent.AddChild(child2);

        Console.WriteLine("Original Virus Family:");
        parent.PrintFamily();

        Virus clonedParent = (Virus)parent.Clone();
        Console.WriteLine("\nCloned Virus Family:");
        clonedParent.PrintFamily();
    }
}
