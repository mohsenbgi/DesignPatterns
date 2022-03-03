using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChainOfModifying;

namespace ChainWithMediator
{
    public class Query
    {
        public string CreatureName;
        public int Value;

        public enum ModifyType
        {
            Attack,
            Defense
        }

        public ModifyType For;


        public Query(string creatureName, int value, ModifyType @for)
        {
            CreatureName = creatureName;
            Value = value;
            For = @for;
        }
    }

    public class Creature
    {
        private readonly Game _game;
        public string Name;
        private readonly int _attack;
        private readonly int _defense;

        public Creature(Game game, int attack, int defense, string name)
        {
            _game = game;
            this._attack = attack;
            this._defense = defense;
            this.Name = name;
        }

        public int Attack
        {
            get
            {
                Query query = new Query(Name, _attack, Query.ModifyType.Attack);
                _game.OnQueries(this, query);
                return query.Value;
            }
        }

        public int Defense
        {
            get
            {
                Query query = new Query(Name, _defense, Query.ModifyType.Defense);
                _game.OnQueries(this, query);
                return query.Value;
            }
        }

        public override string ToString() // no game
        {
            return $"{nameof(Name)}: {Name}, {nameof(_attack)}: {Attack}, {nameof(_defense)}: {Defense}";
            //                                                 ^^^^^^^^ using a property    ^^^^^^^^^
        }
    }

    //Mediator
    public class Game
    {
        public event EventHandler<Query> Queries;

        public virtual void OnQueries(object sender, Query e)
        {
            Queries?.Invoke(sender, e);
        }
    }

    public abstract class CreatureModifier : IDisposable
    {
        protected readonly Game _game;
        protected readonly Creature _creature;

        public CreatureModifier(Game game, Creature creature)
        {
            _game = game;
            _creature = creature;
            _game.Queries += Handle;
        }

        protected abstract void Handle(object sender, Query query);

        public void Dispose()
        {
            _game.Queries -= Handle;
        }
    }

    class DoubleAttackModifier : CreatureModifier
    {
        public DoubleAttackModifier(Game game, Creature creature) : base(game, creature)
        {
        }

        protected override void Handle(object sender, Query query)
        {
            if (query.CreatureName == _creature.Name
                && query.For == Query.ModifyType.Attack)
            {
                query.Value *= 2;
            }
        }
    }

    class IncreaseDefenseModifier : CreatureModifier
    {
        public IncreaseDefenseModifier(Game game, Creature creature) : base(game, creature)
        {
        }

        protected override void Handle(object sender, Query query)
        {
            if (query.CreatureName == _creature.Name
                && query.For == Query.ModifyType.Defense)
            {
                query.Value += 1;
            }
        }
    }


    class ChainWithMediator
    {
        static void Main()
        {
            var clash = new Game();
            var goblin = new Creature(clash, 3, 3, "Goblin");
            Console.WriteLine(goblin);
            using (new DoubleAttackModifier(clash, goblin))
            {
                Console.WriteLine(goblin);
            }

            using (new IncreaseDefenseModifier(clash, goblin))
            {
                Console.WriteLine(goblin);
            }

            Console.WriteLine(goblin);
        }
    }
}
