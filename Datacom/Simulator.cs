using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gyumin.Datacom
{
    using Models;

    public class Simulator
    {
        public enum NodeType { Backoff, Dcf }

        private Medium medium;
        private List<Node> nodes;

        public Simulator(int n, double lambda, NodeType type)
        {
            Console.WriteLine(n);
            Console.WriteLine(lambda);
            Console.WriteLine(type);

            medium = new Medium();
            nodes = new List<Node>(n);
            for (var i = 0; i < n; i++) {
                Node node = null;
                switch (type)
                {
                    case NodeType.Backoff:
                        node = new BackoffNode(medium, lambda);
                        break;
                    case NodeType.Dcf:
                        node = new DcfNode(medium, lambda);
                        break;
                    default:
                        throw new ArgumentException("Unknown NodeType", "type");
                }
                nodes.Add(node);
            }
        }

        public void Run()
        {
            for (var i = 0; i < Constants.SIMUL_DURATION_TIME; i++)
            {
                nodes.ForEach(node => node.Next(i));
                medium.Next();
            }

            var throughput = (double)medium.AckCount * Constants.ONE_SECOND_TIME / Constants.SIMUL_DURATION_TIME;
            var mean_packet_delay = nodes.Sum(node => node.Delay) / medium.AckCount / Constants.ONE_SECOND_TIME;
            var collision_probability = 1 - (double)medium.AckCount / medium.RequestCount;

            Console.WriteLine(throughput);
            Console.WriteLine(mean_packet_delay);
            Console.WriteLine(collision_probability);
        }

        static void Main(string[] args)
        {
        }
    }
}
