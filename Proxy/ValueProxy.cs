using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy
{
    public struct Percentage : IEquatable<Percentage>
    {
        private readonly float _value;

        internal Percentage(float value)
        {
            _value = value;
        }

        public bool Equals(Percentage other)
        {
            return _value.Equals(other._value);
        }

        public override bool Equals(object obj)
        {
            return obj is Percentage other && Equals(other);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public static bool operator ==(Percentage left, Percentage right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Percentage left, Percentage right)
        {
            return !left.Equals(right);
        }

        public static float operator *(float left, Percentage right)
        {
            return left * right._value;
        }

        public static Percentage operator +(Percentage left, Percentage right)
        {
            return new Percentage(left._value + right._value);
        }

        public override string ToString()
        {
            return $"{_value * 100}%";
        }
    }

    public static class PercentageExtensions
    {
        public static Percentage Percentage(this float value)
        {
            return new Percentage(value / 100.0f);
        }

        public static Percentage Percentage(this int value)
        {
            return new Percentage(value / 100.0f);
        }
    }

    class Program
    {
        void Main()
        {
            Console.WriteLine(10 * 5.Percentage());

            Console.WriteLine(5.Percentage() + 7.Percentage());
        }
    }
}
