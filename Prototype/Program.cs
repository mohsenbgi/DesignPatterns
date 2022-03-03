using System;
using System.IO;
using System.Xml.Serialization;

namespace Prototype
{

    public static class CopyWithSerializationExtensions
    {
        public static T DeepCopyXml<T>(this T self)
            where T : new()
        {
            using (var stream = new MemoryStream())
            {
                var serialization = new XmlSerializer(typeof(T));
                serialization.Serialize(stream, self);
                // stream.Seek(0, SeekOrigin.Begin);
                stream.Position = 0;
                return (T) serialization.Deserialize(stream);
            }
        }
    }
    
    public interface IDeepCopyable<T>
        where T : new()
    {
        public T DeepCopy()
        {
            T t = new T();
            CopyTo(t);
            return t;
        }
        void CopyTo(T target);
        
    }
    
    public static class DeepCopyExtensions
    {
        public static T DeepCopy<T>(this T deepCopyable)
            where T : IDeepCopyable<T>, new()
        {
            return deepCopyable.DeepCopy();
        }
    }

    public class Person : IDeepCopyable<Person>
    {
        public string[] Names;
        public Address Address;

        public Person()
        {
            
        }

        public Person(string[] names, Address address)
        {
            Names = names;
            Address = address;
        }

        public void CopyTo(Person target)
        {
            target.Names = (string[]) Names.Clone();
            target.Address = Address.DeepCopy();
        }

        public override string ToString()
        {
            return $"Name: {string.Join(" ", Names)}, Address: {Address.ToString()}";
        }

        public Person(Person other) // copy constructor
        {
            this.Names = (string[]) other.Names.Clone(); // Shallow
            this.Address = new Address(other.Address);
        }
    }

    public class Employee : Person, IDeepCopyable<Employee>
    {
        public string Position;

        public Employee(string[] names, Address address, string position)
            : base(names, address)
        {
            Position = position;
        }

        public Employee()
        {
            
        }
        public override string ToString()
        {
            return $"{base.ToString()}, Position: {Position}";
        }
        
        public void CopyTo(Employee target)
        {
            base.CopyTo(target);
            target.Position = Position;
        }
    }
    
    public class Address : IDeepCopyable<Address>
    {
        public string StreetName;
        public int HouseNumber;

        public Address(string streetName, int houseNumber)
        {
            StreetName = streetName;
            HouseNumber = houseNumber;
        }

        public Address()
        {
            
        }

        public Address(Address other) // copy constructor
        {
            this.HouseNumber = other.HouseNumber;
            this.StreetName = other.StreetName;
        }

        public void CopyTo(Address target)
        {
            target.HouseNumber = HouseNumber;
            target.StreetName = StreetName;
            //target = (Address) this.MemberwiseClone(); correct because members are string 
        }

        public override string ToString()
        {
            return $"StreetName: {StreetName}, HouseNumber: {HouseNumber}";
        }
    }


    

    class Programd
    {
        static void Main(string[] args)
        {
            Person person = new Person()
            {
                Names = new[]{"mohsen","baghery"},
                Address = new Address("JafarSadeq",123)
            };
            
            Employee employee = new Employee()
            {
                Names = new[] {"mohsen", "baghery"},
                Address = new Address("JafarSadeq", 123),
                Position = "Developer"
            };
            
            // Person p = person; // copy by reference
            // Person p = new Person(person); // copy constructor
            
            Person p = person.DeepCopy();
            // Person p = person.DeepCopyXml();
            
            Employee e = employee.DeepCopy();
            // Employee e = employee.DeepCopyXml();
            
            e.Names[0] = "mohsenDeveloper";
            p.Names[0] = "Ali";
            p.Address.HouseNumber = 321;

            Console.WriteLine(person);
            Console.WriteLine(p);
            Console.WriteLine(employee);
            Console.WriteLine(e);
            
        }
    }
}