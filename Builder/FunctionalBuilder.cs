using System;
using System.Collections.Generic;

namespace FunctionalBuilder
{
    public class Person
    {
        public string Street, PostCode;
        public string Name, Family;
        public string CompanyName, Position;
    
    
        public static PersonBuilder Builder => new PersonBuilder();
        
        public override string ToString()
        {
            return $"Name: {Name}, Family: {Family}" +
                   $"Address => Street: {Street}, PostCode: {PostCode}" +
                   $"CompanyName: {CompanyName}, Position: {Position}";
        }
    }
    
    public sealed class PersonBuilder
    {
        private List<Action<Person>> _actions = new List<Action<Person>>();
    
        public Person Build()
        {
            Person person = new Person();
            _actions.ForEach(action => action(person));
            return person;
        }
        
        public PersonBuilder WithName(string name)
        {
            _actions.Add(p => p.Name = name);
            return this;
        }
    
        public void AddAction(Action<Person> action)
        {
            _actions.Add(action);
        }
    
        public PersonBuilder WithFamily(string family)
        {
            _actions.Add(p => p.Family = family);
            return this;
        }
        
    }
    
    // JobBuilder
    // Extension Method
    public static class JobBuilder
    {
        
        public static PersonBuilder AsA(this PersonBuilder builder, string position)
        {
            builder.AddAction(p => p.Position = position);
            return builder;
        }
    }
    
    // CompanyBuilder
    // Extension Method
    public static class CompanyBuilder
    {
        public static PersonBuilder At(this PersonBuilder builder, string companyName)
        {
            builder.AddAction(p => p.CompanyName = companyName);
            return builder;
        }
        
        public static PersonBuilder AtStreet(this PersonBuilder builder, string streenName)
        {
            builder.AddAction(p => p.Street = streenName);
            return builder;
        }
    }
    
    
    class FunctionalProgram
    { 
        void Main()
        {
            var person = Person.Builder
                .WithName("mohsen")
                .WithFamily("baghery")
                .At("Tesla")
                .AsA("Engineer")
                .AtStreet("Lower")
                .Build();
            Console.WriteLine(person);
        }
    }
    
}