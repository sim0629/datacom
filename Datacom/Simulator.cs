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

        public Simulator(int n, int lambda, NodeType type)
        {
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
        }

        static void Main(string[] args)
        {
        }
    }
}
