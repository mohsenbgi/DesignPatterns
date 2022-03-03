using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositeProxy
{
    public class Creature
    {
        public byte Age;
        public int X, Y;

        public Creature(byte age, int x, int y)
        {
            Age = age;
            X = x;
            Y = y;
        }
    }

    public class Creatures
    {
        private readonly byte[] _age;
        private readonly int[] _x, _y;
        private readonly int _size;

        public Creatures(int size)
        {
            _size = size;
            _age = new byte[size];
            _x = new int[size];
            _y = new int[size];
        }

        public struct CreatureProxy
        {
            private readonly Creatures _creatures;
            private readonly int _index;

            public CreatureProxy(Creatures creatures, int index)
            {
                _creatures = creatures;
                _index = index;
            }

            public ref int X => ref _creatures._x[_index];
            public ref int Y => ref _creatures._y[_index];
            public ref byte Age => ref _creatures._age[_index];

        }

        public IEnumerator<CreatureProxy> GetEnumerator()
        {
            for (int pos = 0; pos < _size; pos++)
                yield return new CreatureProxy(this, pos);
        }
    }

    class CompositeProxySoAAoS
    {
        void Main()
        {
            var creatures = new Creature[100];
            foreach (Creature creature in creatures)
            {
                creature.X++;
                // Age X Y Age X Y Age X Y
                // unhelpful jump is 2 for access to X
            }

            var creatures2 = new Creatures(100);
            foreach (var creature in creatures2)
            {
                creature.X++;
                // X X X X 
                // Y Y Y Y
                // Age Age Age
                // unhelpful jump is 0 for access to X so faster

            }
        }


    }
}
