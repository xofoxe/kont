using System;
using System.Collections.Generic;
class Character
{
    public string Height { get; set; }
    public string BodyType { get; set; }
    public string HairColor { get; set; }
    public string EyeColor { get; set; }
    public string Clothing { get; set; }
    public List<string> Inventory { get; set; } = new List<string>();
    public List<string> Deeds { get; set; } = new List<string>();

    public override string ToString()
    {
        return $"Height: {Height}, Body: {BodyType}, Hair: {HairColor}, " +
               $"Eyes: {EyeColor}, Clothing: {Clothing}, " +
               $"Inventory: {string.Join(", ", Inventory)}, Deeds: {string.Join(", ", Deeds)}";
    }
}

interface ICharacterBuilder
{
    void SetHeight(string height);
    void SetBodyType(string bodyType);
    void SetHairColor(string color);
    void SetEyeColor(string color);
    void SetClothing(string clothing);
    void AddToInventory(string item);
    void AddDeed(string deed);
    Character Build();
}
class HeroBuilder : ICharacterBuilder
{
    private Character _character = new Character();

    public void SetHeight(string height)
    {
        _character.Height = height;
    }
    public void SetBodyType(string bodyType)
    {
        _character.BodyType = bodyType;
    }
    public void SetHairColor(string color)
    {
        _character.HairColor = color;
    }
    public void SetEyeColor(string color)
    {
        _character.EyeColor = color;
    }
    public void SetClothing(string clothing)
    {
        _character.Clothing = clothing;
    }
    public void AddToInventory(string item)
    {
        _character.Inventory.Add(item);
    }
    public void AddDeed(string deed)
    {
        _character.Deeds.Add($"Good: {deed}");
    }
    public Character Build()
    {
        return _character;
    }
}

class EnemyBuilder : ICharacterBuilder
{
    private Character _character = new Character();

    public void SetHeight(string height)
    {
        _character.Height = height;
    }
    public void SetBodyType(string bodyType)
    {
        _character.BodyType = bodyType;
    }
    public void SetHairColor(string color)
    {
        _character.HairColor = color;
    }
    public void SetEyeColor(string color)
    {
        _character.EyeColor = color;
    }
    public void SetClothing(string clothing)
    {
        _character.Clothing = clothing;
    }
    public void AddToInventory(string item)
    {
        _character.Inventory.Add(item);
    }
    public void AddDeed(string deed)
    {
        _character.Deeds.Add($"Evil: {deed}");
    }
    public Character Build()
    {
        return _character;
    }
}

class Director
{
    private ICharacterBuilder _builder;

    public Director(ICharacterBuilder builder) { _builder = builder; }

    public Character Construct()
    {
        _builder.SetHeight("180 cm");
        _builder.SetBodyType("male");
        _builder.SetHairColor("Brown");
        _builder.SetEyeColor("Brown");
        _builder.SetClothing("Armor");
        _builder.AddToInventory("Sword");
        _builder.AddToInventory("Shield");
        _builder.AddDeed("Saved a village");
        return _builder.Build();
    }
}

class Program
{
    static void Main()
    {
        ICharacterBuilder heroBuilder = new HeroBuilder();
        Director heroDirector = new Director(heroBuilder);
        Character hero = heroDirector.Construct();
        Console.WriteLine("Hero:");
        Console.WriteLine(hero);

        ICharacterBuilder enemyBuilder = new EnemyBuilder();
        enemyBuilder.SetHeight("190 cm");
        enemyBuilder.SetBodyType("Slim");
        enemyBuilder.SetHairColor("Black");
        enemyBuilder.SetEyeColor("Red");
        enemyBuilder.SetClothing("Dark Cloak");
        enemyBuilder.AddToInventory("Dagger");
        enemyBuilder.AddToInventory("Poison");
        enemyBuilder.AddDeed("Burned a village");
        Character enemy = enemyBuilder.Build();
        Console.WriteLine("\nEnemy:");
        Console.WriteLine(enemy);
    }
}
