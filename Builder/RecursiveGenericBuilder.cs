using System;

namespace RecursiveGenericBuilder
{
    class Person
    {
        public string Street, PostCode;
        public string Name, Family;
        public string CompanyName, Position;

        public class Builder : PersonCompanyBuilder<Builder>
        {
        }

        public Builder New() => new Builder();


        public override string ToString()
        {
            return $"Name: {Name}, Family: {Family}" +
                   $"Address => Street: {Street}, PostCode: {PostCode}" +
                   $"CompanyName: {CompanyName}, Position: {Position}";
        }
    }

    abstract class PersonBuilder
    {
        protected Person Person;

        public PersonBuilder()
        {
            Person = new Person();
        }

        public Person Build()
        {
            return Person;
        }
    }

    class PersonInfoBuilder<TSelf> : PersonBuilder
        where TSelf : PersonInfoBuilder<TSelf>
    {
        public TSelf WithName(string name)
        {
            Person.Name = name;
            return (TSelf) this;
        }

        public TSelf WithFamily(string family)
        {
            Person.Family = family;
            return (TSelf) this;
        }
    }

    class PersonJobBuilder<TSelf> : PersonInfoBuilder<TSelf>
        where TSelf : PersonJobBuilder<TSelf>
    {
        public TSelf AsA(string position)
        {
            Person.Position = position;
            return (TSelf) this;
        }
    }

    class PersonCompanyBuilder<TSelf> : PersonJobBuilder<TSelf>
        where TSelf : PersonCompanyBuilder<TSelf>
    {
        public TSelf At(string companyName)
        {
            Person.CompanyName = companyName;
            return (TSelf) this;
        }

        public TSelf AtStreet(string streetName)
        {
            Person.Street = streetName;
            return (TSelf) this;
        }

        public TSelf ByPostCode(string postCode)
        {
            Person.PostCode = postCode;
            return (TSelf) this;
        }
    }

    public class RecursiveGenericBuilderProgram
    {
        static void Main(string[] args)
        {
            var person = new Person()
                .New()
                .WithName("mohsen")
                .At("Tesla")
                .AsA("Engineer")
                .WithFamily("baghery")
                .Build();
            Console.WriteLine(person);
        }
    }
}