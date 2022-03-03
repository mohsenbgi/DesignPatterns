using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;
using JetBrains.dotMemoryUnit;
using NUnit.Framework;

namespace FlyWeight
{

    public class User // Bad Way
    {
        private string FullName;

        public User(string fullName)
        {
            FullName = fullName;
        }
    }

    public class User2 // Good Way
    {
        public static List<string> Names = new List<string>();
        public int[] IndexofName;

        public User2(string name)
        {
            IndexofName = name.Split(' ').Select(GetOrAddIndex).ToArray();
        }

        private int GetOrAddIndex(string name)
        {
            int index = Names.IndexOf(name);
            if (index == -1)
            {
                Names.Add(name);
                index = Names.Count - 1;
            }

            return index;
        }

        public string Name => string.Join(' ', IndexofName.Select(i => Names[i]));
    }


    [TestFixture]
    class UserFullNames
    {
        [Test]
        public void Test()
        {
            //var names = Enumerable.Range(0, 100).Select(_ => GenerateRandomName());
            //var families = Enumerable.Range(0, 100).Select(_ => GenerateRandomName());


            var firstNames = new List<string>();
            var lastNames = new List<string>();

            for (int i = 0; i < 300; i++)
            {
                firstNames.Add(GenerateRandomName());
                lastNames.Add(GenerateRandomName());
            }

            var users = new List<User>();

            foreach (string name in firstNames)
            {
                foreach (string family in lastNames)
                {
                    users.Add(new User($"{name} {family}"));
                }
            }

            dotMemory.Check(m =>
            {
                Console.WriteLine(m.SizeInBytes);
            });
        }

        [Test]
        public void Test2()
        {
            var firstNames = new List<string>();
            var lastNames = new List<string>();

            for (int i = 0; i < 300; i++)
            {
                firstNames.Add(GenerateRandomName());
                lastNames.Add(GenerateRandomName());
            }

            var users = new List<User2>();

            foreach (string name in firstNames)
            {
                foreach (string family in lastNames)
                {
                    users.Add(new User2($"{name} {family}"));
                }
            }

            //GC.Collect();
            //GC.WaitForPendingFinalizers();
            //GC.Collect();

            dotMemory.Check(m =>
            {
                Console.WriteLine(m.SizeInBytes);
            });
        }

        public string GenerateRandomName()
        {
            var rand = new Random();
            return new string(
                Enumerable.Range(0, 10)
                    .Select(_ =>(char) ('a' + rand.Next(20))).ToArray()
                );
        }
    }
}
