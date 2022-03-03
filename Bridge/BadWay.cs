using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge_BadWay
{
    interface IShape
    {
        void Draw();
    }

    interface IColor
    {
        public void SetColor();
    }

    class Circle : IShape
    {
        public void Draw()
        {
            Console.WriteLine("Draw A Circle");
        }
    }

    class Rectangle : IShape
    {
        public void Draw()
        {
            Console.WriteLine("Draw A Rectangle");
        }
    }

    class RedCircle : Circle, IColor
    {
        public RedCircle()
        {
            SetColor();
        }

        public void SetColor()
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
    }



    class YellowCircle : Circle, IColor
    {
        public YellowCircle()
        {
            SetColor();
        }

        public void SetColor()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
    }


    class RedRectangle : Rectangle, IColor
    {
        public RedRectangle()
        {
            SetColor();
        }

        public void SetColor()
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
    }

    class YellowRectangle : Rectangle, IColor
    {

        public YellowRectangle()
        {
            SetColor();
        }
        public void SetColor()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
    }

    class BadWayProgram
    {
        void Main()
        {
            var redcircle = new RedCircle();
            var yellowRectangle = new YellowRectangle();
            redcircle.Draw();
            yellowRectangle.Draw();
        }
    }
}
