using System;
using System.Reflection.Metadata;

namespace ChainOfModifying
{
    class Creature
    {
        public string Name;
        public int Attack, Defense;

        public Creature(string name, int attack, int defense)
        {
            Name = name;
            Attack = attack;
            Defense = defense;
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Attack)}: {Attack}, {nameof(Defense)}: {Defense}";
        }
    }

    interface ICreatureModifier // IHandler
    {
        void Handle();
        ICreatureModifier AddNext(ICreatureModifier creatureModifier);
    }

    abstract class CreatureModifier : ICreatureModifier
    {
        protected Creature Creature;
        protected ICreatureModifier Next;

        protected CreatureModifier(Creature creature)
        {
            Creature = creature;
        }

        public virtual void Handle() => Next?.Handle();

        public ICreatureModifier AddNext(ICreatureModifier creatureModifier)
        {
            if (Next == null)
                Next = creatureModifier;
            else
                Next.AddNext(creatureModifier);
            return this;
        }
    }

    class DoublingAttack : CreatureModifier
    {
        public DoublingAttack(Creature creature) : base(creature)
        {

        }

        public override void Handle()
        {
            Creature.Attack *= 2;
            base.Handle();
        }
    }

    class IncreaseDefense : CreatureModifier
    {
        public IncreaseDefense(Creature creature) : base(creature)
        {

        }

        public override void Handle()
        {
            Creature.Defense += 1;
            base.Handle();
        }
    }

    class CloseModifying : CreatureModifier
    {
        public CloseModifying(Creature creature) : base(creature)
        {
        }

        public override void Handle()
        {

        }
    }

    class ChainOfModifying
    {
        void Main(string[] args)
        {
            var goblin = new Creature("Goblin", 3, 3);

            Console.WriteLine(goblin);

            var modify = new DoublingAttack(goblin);
            modify.AddNext(new IncreaseDefense(goblin));

            modify.AddNext(new IncreaseDefense(goblin))
                .AddNext(new CloseModifying(goblin)); // stop modifying

            modify.AddNext(new DoublingAttack(goblin)); // don't work because modifying stopped

            modify.Handle();

            Console.WriteLine(goblin);


        }
    }


}
