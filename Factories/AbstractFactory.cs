using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace AbstractFactory
{
    public enum AvailableDrinks
    {
        Coffee, Tea
    }
    
    public interface IHotDrink
    {
        public void Consume();
    }

    internal class Tea : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("This tea is great. tanks for tea");
        }
    }
    
    internal class Coffee : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("This coffee is very good. ofcourse tanks for milk and sugar");
        }
    }

    public interface IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount);
    }
    
    internal class TeaFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"Put tea bag in boil water, {amount} tea, enjoy of drinking");
            return new Tea();
        }
    }

    internal class CoffeeFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"Put Coffee in boil water, mix milk and sugar, {amount} Coffee, enjoy of drinking");
            return new Coffee();
        }
    }

    public class ConfigHotDrink
    {
        private Dictionary<AvailableDrinks, IHotDrinkFactory> _factories;

        public ConfigHotDrink()
        {
            _factories = new Dictionary<AvailableDrinks, IHotDrinkFactory>();
            foreach (AvailableDrinks drink in Enum.GetValues(typeof(AvailableDrinks)))
            {
                string typeName = "AbstractFactory." +
                                  Enum.GetName(typeof(AvailableDrinks), drink) + "Factory";
                
                IHotDrinkFactory factory = (IHotDrinkFactory) 
                    Activator.CreateInstance(Type.GetType(typeName) 
                        ?? throw new InvalidOperationException($"no available type named : {typeName}"));
                _factories.Add(drink, factory);
            }
        }
        
        public IHotDrink MakeDrink(int amount, AvailableDrinks availableDrinks)
        {
            return _factories[availableDrinks].Prepare(amount);
        }
    }
    
    
    public class Program
    {
        static void Main()
        {
            var configHotDrink = new ConfigHotDrink();
             var drink = configHotDrink.MakeDrink(3, AvailableDrinks.Coffee);
             drink.Consume();

            // var machine = new HotDrinkMachine();
            // var drink = machine.MakeDrink();
            // drink.Consume();
            
        }
    }

    #region OpenClosedWithAbstractFactory

    public class HotDrinkMachine
    {
        private List<(string name, IHotDrinkFactory factory)> _factories;
        
        public HotDrinkMachine()
        {
            _factories = new List<(string name, IHotDrinkFactory factory)>();
            
            foreach (var t in typeof(HotDrinkMachine).Assembly.GetTypes())
            {
                if (t.IsAssignableTo(typeof(IHotDrinkFactory))
                    && !t.IsInterface)
                {
                    IHotDrinkFactory factory = (IHotDrinkFactory) Activator.CreateInstance(t);
                    string nameOfHotDrink = t.Name.Replace("Factory", String.Empty);
                    _factories.Add(new (nameOfHotDrink,factory));
                }
            }
        }

        public IHotDrink MakeDrink()
        {
            Console.WriteLine("Available Drinks : ");
            for (int i = 0; i < _factories.Count; i++)
            {
                Console.WriteLine($"{i+1}- {_factories[i].name}");
            }

            string str;
            while (true)
            {
                if ((str = Console.ReadLine()) != null
                    && int.TryParse(str, out int i)
                    && i > 0
                    && i <= _factories.Count)
                {
                    i--;
                    Console.WriteLine("Specify amount : ");
                    if ((str = Console.ReadLine()) != null
                        && int.TryParse(str, out int amount)
                        && amount > 0)
                    {
                        return _factories[i].factory.Prepare(amount);
                    }
                }
                else
                {
                    Console.WriteLine("Input is not valid, try again");
                }
            }
        }
    }

    #endregion
}