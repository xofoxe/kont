using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    abstract class SupportHandler
    {
        protected SupportHandler nextHandler;

        public void SetNext(SupportHandler next)
        {
            nextHandler = next;
        }

        public abstract bool Handle(int level);
    }

    class BasicSupport : SupportHandler
    {
        public override bool Handle(int level)
        {
            if (level == 1)
            {
                Console.WriteLine("Зверніться до базової підтримки.");
                return true;
            }
            else if (nextHandler != null)
            {
                return nextHandler.Handle(level);
            }
            return false;
        }
    }
    class TechnicalSupport : SupportHandler
    {
        public override bool Handle(int level)
        {
            if (level == 2)
            {
                Console.WriteLine("Зверніться до технічної підтримки.");
                return true;
            }
            else if (nextHandler != null)
            {
                return nextHandler.Handle(level);
            }
            return false;
        }
    }
    class BillingSupport : SupportHandler
    {
        public override bool Handle(int level)
        {
            if (level == 3)
            {
                Console.WriteLine("Зверніться до відділу оплати.");
                return true;
            }
            else if (nextHandler != null)
            {
                return nextHandler.Handle(level);
            }
            return false;
        }
    }
    class ManagerSupport : SupportHandler
    {
        public override bool Handle(int level)
        {
            if (level == 4)
            {
                Console.WriteLine("Зверніться до менеджера.");
                return true;
            }
            else if (nextHandler != null)
            {
                return nextHandler.Handle(level);
            }
            return false;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            SupportHandler basic = new BasicSupport();
            SupportHandler tech = new TechnicalSupport();
            SupportHandler billing = new BillingSupport();
            SupportHandler manager = new ManagerSupport();

            basic.SetNext(tech);
            tech.SetNext(billing);
            billing.SetNext(manager);

            while (true)
            {
                Console.WriteLine("\nЛаскаво просимо до служби підтримки.");
                Console.WriteLine("1. У мене загальне питання.");
                Console.WriteLine("2. У мене технічна проблема.");
                Console.WriteLine("3. Питання з оплатою.");
                Console.WriteLine("4. Потрібен менеджер.");
                Console.WriteLine("Введіть ваш вибір (1-4):");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    bool handled = basic.Handle(choice);
                    if (handled)
                        break;
                    else
                        Console.WriteLine("Ваш запит не розпізнано. Повторіть спробу.");
                }
                else
                {
                    Console.WriteLine("Некоректний ввід. Спробуйте ще раз.");
                }
            }
        }
    }
}