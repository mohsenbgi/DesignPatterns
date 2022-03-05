using System;

namespace Adapter
{
    public interface IShape
    {
        public int Area();
    }


    // Target : IRectangle
    public interface IRectangle : IShape
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }


    public class Rectangle : IRectangle, IShape
    {

        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Width { get; set; }
        public int Height { get; set; }

        public int Area()
        {
            return Width * Height;
        }

        public override string ToString()
        {
            return $"Width: {Width}, Height: {Height}";
        }
    }


    // Adapter : Square
    public class Square : IShape
    {
        public int Side { get; set; }

        public Square(int side)
        {
            Side = side;
        }

        public int Area()
        {
            return Side * Side;
        }

        public override string ToString()
        {
            return $"Side: {Side}";
        }
    }

    
    // Adapter : Square => Rectangle
    public class SquareToRectangleAdapter : Rectangle, IRectangle
    {
        public SquareToRectangleAdapter(Square square) : base(square.Side, square.Side)
        {

        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Square square = new Square(4);

            IRectangle rectangle = new SquareToRectangleAdapter(square);

            Console.WriteLine("Square: "+square);
            Console.WriteLine("Square Area: " + square.Area());
            
            Console.WriteLine("................................");

            Console.WriteLine("Rectangle: " + rectangle);
            Console.WriteLine("Rectangle Area: " + rectangle.Area());
        }
    }
}
