using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MathNet.Numerics.Distributions;

namespace Gyumin.Datacom.Models
{
    public abstract class Node
    {
        protected Medium medium;
        protected Poisson poisson;
        protected readonly Queue<Packet> queue = new Queue<Packet>();
        protected HashSet<int> samples;

        public Node(Medium medium, double lambda)
        {
            this.medium = medium;
            poisson = new Poisson(lambda);
        }

        protected void Generate()
        {
            var amount = poisson.Sample();
            var array = new int[amount];
            DiscreteUniform.Samples(array, 0, Constants.ONE_SECOND_TIME - 1);
            samples = new HashSet<int>(array);
        }

        public virtual void Next(int elapsed)
        {
            var moded = elapsed % Constants.ONE_SECOND_TIME;
            if (moded == 0)
            {
                Generate();
            }
            if (samples.Contains(moded))
            {
                queue.Enqueue(new Packet(elapsed));
            }
        }

        protected void Dequeue(int completedAt)
        {
            var packet = queue.Dequeue();
            var delay = completedAt - packet.CreatedAt;
            Delay += delay;
        }

        #region Statistics

        public double Delay
        {
            get;
            private set;
        }

        #endregion Statistics
    }
}
