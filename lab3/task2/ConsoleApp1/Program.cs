using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    using System;

    interface IHero
    {
        string GetDescription();
        int GetPower();
    }

    class Warrior : IHero
    {      
        public string GetDescription() => "Warrior";
        private static int power = 9;
        public int GetPower() => power;
    }

    class Mage : IHero
    {
        public string GetDescription() => "Mage";
        private static int power = 8;
        public int GetPower() => power;
    }

    class Paladin : IHero
    {
        public string GetDescription() => "Paladin";
        private static int power = 9;
        public int GetPower() => power;
    }

    abstract class HeroDecorator : IHero
    {
        protected IHero _hero;

        public HeroDecorator(IHero hero)
        {
            _hero = hero;
        }

        public virtual string GetDescription()
        {
            return _hero.GetDescription();
        }

        public virtual int GetPower()
        {
            return _hero.GetPower();
        }
    }

    class Sword : HeroDecorator
    {
        public Sword(IHero hero) : base(hero) { }

        public override string GetDescription()
        {
            return _hero.GetDescription() + " with a Sword";
        }

        public override int GetPower()
        {
            return _hero.GetPower() + 5;
        }
    }

    class Armor : HeroDecorator
    {
        public Armor(IHero hero) : base(hero) { }

        public override string GetDescription()
        {
            return _hero.GetDescription() + " wearing Armor";
        }

        public override int GetPower()
        {
            return _hero.GetPower() + 3;
        }
    }

    class MagicRing : HeroDecorator
    {
        public MagicRing(IHero hero) : base(hero) { }

        public override string GetDescription()
        {
            return _hero.GetDescription() + " with a Magic Ring";
        }

        public override int GetPower()
        {
            return _hero.GetPower() + 4;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IHero warrior = new Warrior();
            IHero armoredWarrior = new Armor(warrior);
            IHero fullyEquippedWarrior = new Sword(new MagicRing(armoredWarrior));

            Console.WriteLine($"{fullyEquippedWarrior.GetDescription()} has power {fullyEquippedWarrior.GetPower()}");

            IHero mage = new Mage();
            IHero mageWithRing = new MagicRing(mage);

            Console.WriteLine($"{mageWithRing.GetDescription()} has power {mageWithRing.GetPower()}");
        }
    } 
}
