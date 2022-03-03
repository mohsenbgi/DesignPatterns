using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Microsoft.VisualBasic;

namespace FacetedBuilder
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

    public class PersonBuilder
    {
        private Person _person;

        public PersonBuilder()
        {
            _person = new Person();
            
        }

        public Person Build() => _person;

        public JobBuilder Works() => new JobBuilder(_person);
        
        public PersonBuilder WithName(string name)
        {
            _person.Name = name;
            return this;
        }
            
        public PersonBuilder WithFamily(string family)
        {
            _person.Family = family;
            return this;
        }
    }

    public class JobBuilder : PersonBuilder
    {
        private Person _person;

        public JobBuilder(Person person)
        {
            _person = person;
        }

        public JobBuilder AsA(string position)
        {
            _person.Position = position;
            return this;
        }
    }


    public class Program
    {
        void Main()
        {
            var person = Person.Builder
                .WithName("mohsen")
                .WithFamily("baghery")
                .Works()
                .AsA("Engineer")
                .Build();
        }
    
    }
}