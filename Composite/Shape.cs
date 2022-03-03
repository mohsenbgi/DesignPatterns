using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composite_Shape
{
    
    // This example dos not have Component interface 

    // Composite
    class GraphicObject
    {
        public virtual string Name { get; set; } = "Group";

        public string Color;
        private Lazy<List<GraphicObject>> _children = new Lazy<List<GraphicObject>>();
        public List<GraphicObject> Children => _children.Value;


        private void Print(StringBuilder stringBuilder, int depth)
        {

            stringBuilder.Append(new string('\t', depth))
                .AppendLine(this.Name)
                .AppendLine((string.IsNullOrEmpty(Color)) ? string.Empty : $"Color: {Color}");
            foreach (var child in Children)
            {
                child.Print(stringBuilder, depth + 1);
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            Print(stringBuilder, 0);
            return stringBuilder.ToString();
        }

    }

    class Circle : GraphicObject
    {
        public override string Name => "Circle";
    }

    class Rectangle : GraphicObject
    {
        public override string Name => "Rectangle";
    }


    class Program
    {
        void Main()
        {
            var group = new GraphicObject();

            var circle = new Circle();
            var rectangle = new Rectangle();

            group.Children.Add(circle);
            group.Children.Add(rectangle);

            var group2 = new GraphicObject();
            var circle2 = new Circle();
            var rectangle2 = new Rectangle();

            group2.Children.Add(circle2);
            group2.Children.Add(rectangle2);

            group.Children.Add(group2);

            Console.WriteLine(group);
        }
    }
}
