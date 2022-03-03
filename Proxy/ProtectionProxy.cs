using System;

namespace ProtectionProxy
{
    interface ICar
    {
        void Drive();
    }

    class Car : ICar
    {
        public void Drive()
        {
            Console.WriteLine("Car is driving now");
        }
    }

    class Driver
    {
        public int Age;

        public Driver(int age)
        {
            Age = age;
        }
    }

    class ProxyProtectionCar : ICar
    {
        private readonly Driver _driver;
        private readonly ICar _car = new Car();

        public ProxyProtectionCar(Driver driver)
        {
            _driver = driver;
        }

        public void Drive()
        {
            if(_driver.Age >= 18)
                _car.Drive();
            else
            {
                Console.WriteLine("Too young");
            }
        }
    }

    class ProtectionProxy
    {
        void Main(string[] args)
        {
            ICar car = new ProxyProtectionCar(new Driver(22));
            car.Drive();
        }
    }
}
