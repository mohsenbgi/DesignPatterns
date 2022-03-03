using Autofac;
using System;

namespace Bridge
{
    interface IColor
    {
        public void SetColor();
        public void ResetColor();
    }

    abstract class Color : IColor
    {
        public void ResetColor()
        {
            Console.ResetColor();
        }

        public abstract void SetColor();
    }

    class Red : Color, IColor
    {

        public override void SetColor()
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }


    }

    class Yellow : Color, IColor
    {
        
        public override void SetColor()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
    }

    interface IShape
    {
        public void Draw();
    }


    // This is A Bridge Between IShape And IColor
    abstract class Shape : IShape
    {

        protected IColor _color;

        public Shape(IColor color)
        {
            _color = color;
        }

        public abstract void Draw();
    }

    class Rectangle : Shape, IShape
    {
        public Rectangle(IColor color) : base(color)
        {
        }

        public override void Draw()
        {
            _color.SetColor();
            Console.WriteLine("Draw A Rectangle");
            _color.ResetColor();
        }
    }

    class Circle : Shape, IShape
    {

        public Circle(IColor color) : base(color) 
        {

        }

        public override void Draw()
        {
            _color.SetColor();
            Console.WriteLine("Draw A Circle");
            _color.ResetColor();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IColor color = new Red();
            IShape rectangle = new Rectangle(color);
            rectangle.Draw();

            color = new Yellow();
            rectangle = new Rectangle(color);
            rectangle.Draw();
        }
    }
}
