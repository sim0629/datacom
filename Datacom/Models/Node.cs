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
        protected int queue;
        protected int[] samples;

        public Node(Medium medium, int lambda)
        {
            this.medium = medium;
            poisson = new Poisson(lambda);
        }

        protected void Generate()
        {
            int amount = poisson.Sample();
            samples = new int[amount];
            DiscreteUniform.Samples(samples, 0, Constants.ONE_SECOND_TIME - 1);
        }

        public virtual void Next(int elapsed)
        {
            elapsed %= Constants.ONE_SECOND_TIME;
            if (elapsed == 0)
            {
                Generate();
            }
            if (samples.Contains(elapsed))
            {
                queue++;
            }
        }
    }
}
