using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using Xunit;

namespace Singleton
{
    public class MonoState
    {
        private static string _name;
        private static int _age;

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public int Age
        {
            get => _age;
            set => _age = value;
        }
    }
    
    public interface IDataBase
    {
        int GetPopulation(string name);
    }
    
    public class Context : IDataBase
    {
        private IDictionary<string, int> _capitals = new Dictionary<string, int>();
        
        private Context()
        {
            string path = "/home/mohsen/RiderProjects/Console_Practice/ConsoleApp1/Singleton/capitals.txt";
            string name = "";
            foreach (var line in File.ReadAllLines(path, Encoding.UTF8))
            {
                if (int.TryParse(line, out int p))
                {
                    _capitals.Add(name, p);
                }
                else
                {
                    name = line.Trim();
                }
            }
        }

        public static Lazy<IDataBase> Instance { get; } =
            new Lazy<IDataBase>(() => new Context());

        public int GetPopulation(string name)
        {
            throw new NotImplementedException();
        }

    }

    public class SingletonTest
    {
        [Fact]
        public void DatabaseInstanceIsReallySingleton()
        {
            var context1 = Context.Instance;
            var context2 = Context.Instance;
            context1.Value.GetPopulation("Tokyo");
            Assert.Same(context1, context2);
        }
    }
}