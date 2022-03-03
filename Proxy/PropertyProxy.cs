using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy
{

    class Property<T> : IEquatable<Property<T>> where T : new()
    {

        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                if (Equals(_value, value)) return;
                Console.WriteLine($"Assigning value to {value}");
                _value = value;
            }
        }

        public Property():this(default(T))
        {
            
        }

        public Property(T value)
        {
            Value = value;
        }

        public static implicit operator Property<T>(T value)
        {
            return new Property<T>(value); // Property<int> p = 20;

            // in c++ we can do Value = value but cause in c# is static method this work is impossible
            
        }

        public static implicit operator T(Property<T> property)
        {
            return property.Value; // int x = Property<int> p 
        }


        public bool Equals(Property<T> other)
        {
            return other is not null && _value.Equals(other._value);
        }

        public override bool Equals(object obj)
        {
            return obj is Property<T> property && Equals(property);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public static bool operator == (Property<T> left, Property<T> right)
        {
            return left is not null && left.Equals(right);
        }

        public static bool operator != (Property<T> left, Property<T> right)
        {
            return left is not null && !left.Equals(right);
        }
    }

    class Creature
    {
        private readonly Property<int> _attack = new Property<int>();

        public int Attack // this should added because assigning method is static in c#
        {
            get => _attack.Value;
            set => _attack.Value = value;
        }
    }

    class PropertyProxy
    {
        public static Property<int> Number { get; set; }
        void Main()
        {
            //Number = 10;
            // this don't work correct because means Number = new Property<int>(10);
            
            var c = new Creature();
            
            c.Attack = 20; // this works
            c.Attack = 20;
        }
    }
}
