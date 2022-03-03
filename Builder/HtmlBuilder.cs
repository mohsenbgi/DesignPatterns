using System;
using System.Collections.Generic;

namespace Builder
{
    class HtmlElement
    {
        public List<HtmlElement> Elements = new List<HtmlElement>();
        public string TagName, Text;
        private int IndentSize = 2;
    
        public HtmlElement()
        {
            
        }
    
        public HtmlElement(string tagName, string text)
        {
            TagName = tagName;
            Text = text;
        }
    
        private string ToStringImpl(int indent)
        {
            string sinking = new string(' ', IndentSize * indent);
            string result = $"{sinking}<{TagName}>\n";
            if (!string.IsNullOrWhiteSpace(Text))
            {
                result += new string(' ', IndentSize * (indent + 1))
                          + Text + "\n";
            }
            foreach (var element in Elements)
            {
                result += element.ToStringImpl(indent + 1);
            }
            result += $"{sinking}</{TagName}>\n";
            
            return result;
        }
    
        public override string ToString()
        {
            return ToStringImpl(0);
        }
    }
    
    class HtmlBuilder
    {
        public readonly string _rootName;
    
        private HtmlElement root = new HtmlElement();
        
        public HtmlBuilder(string rootName)
        {
            _rootName = rootName;
            root.TagName = rootName;
        }
    
        public HtmlBuilder AddChild(string tagName, string text = "")
        {
            HtmlElement element = new HtmlElement(tagName, text);
            root.Elements.Add(element);
            return this;
        }
    
        public HtmlElement Build()
        {
            return root;
        }
    
        public HtmlBuilder AddElement(HtmlElement element)
        {
            root.Elements.Add(element);
            return this;
        }
    
        public override string ToString()
        {
            return root.ToString();
        }
    }
    
    class HtmlBuilderProgram
    {
        private void Main(string[] args)
        {
            var html = new HtmlBuilder("html");
    
            var body = new HtmlBuilder("body");
    
            var ul = new HtmlBuilder("ul")
                .AddChild("li", "mohsen")
                .AddChild("li", "reza")
                .AddChild("li", "amir")
                .Build();
    
            body.AddElement(ul);
    
            html.AddElement(body.Build());
            
            Console.WriteLine(html);
        }
    }

}