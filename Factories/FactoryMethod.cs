using System;
using System.Threading.Tasks;

namespace FactoryMethod
{
    // enum CoordinateSystemType
    // {
    //     Cartesian,
    //     Polar
    // }

    class Point
    {
        private double _x, _y;
        private Point(double x, double y)
        {
            _x = x;
            _y = y;
        }
        
        public static Point Origin = new Point(0, 0); //better than bottom property

        // public static Point Origin => new Point(0, 0); // top field is better

        // if we need to creating new instance of factory everytime
        // private static PointFactory Factory => new PointFactory();
        //
        // class PointFactory
        // {
        //     // TODO
        // }

        public static class Factory
        {
            public static Point CreateCartesianPoint(double x, double y)
            {
                return new Point(x, y);
            }

            public static Point CreatePolarPoint(double x, double y)
            {
                x = x * Math.Cos(y);
                y = y * Math.Sin(x);
                return new Point(x, y);
            }
        
            public static async Task<Point> CreateAsyncPoint(double x, double y)
            {
                await Task.Delay(1000);
                return new Point(x, y);
            }
        }
    }

    class FactoryMethodProgram
    {
        async void Main()
        {
            double x = 1.2;
            double y = Math.PI;
            Point cartesianPoint = Point.Factory.CreateCartesianPoint(x, y);
            Point polarPoint = Point.Factory.CreatePolarPoint(x, y);
            Point asyncPoint = await Point.Factory.CreateAsyncPoint(x, y);
        }
    }

}