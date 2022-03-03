using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Composite_Neural
{

    public static class NeuronExtensions
    {
        public static void ConnectTo(this IEnumerable<Neuron> self, IEnumerable<Neuron> other)
        {
            if (ReferenceEquals(self, other)) return;

            foreach (var from in self)
            {
                foreach (var to in other)
                {
                    from.Out.Add(to);
                    to.In.Add(from);
                }
            }
        }
    }

    // Composite
    public class Neuron : IEnumerable<Neuron>
    {
        public float Value;
        public Collection<Neuron> In, Out = new Collection<Neuron>();

        public IEnumerator<Neuron> GetEnumerator()
        {
            yield return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return this;
        }



    }

    class NeuronLayer : Collection<Neuron>
    {

    }


    class Program
    {
        static void Main()
        {
            var n1 = new Neuron();
            var n2 = new Neuron();
            var n3 = new Neuron();


            var layer1 = new NeuronLayer();
            var layer2 = new NeuronLayer();

            // neuron to neuron
            n1.ConnectTo(n2);

            // neuron to layer or unlike
            layer1.ConnectTo(n2);
            n3.ConnectTo(layer2);

            // layer to layer
            layer2.ConnectTo(layer1);


        }
    }
}
