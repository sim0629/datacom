using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gyumin.Datacom.Models
{
    public class Packet
    {
        public int CreatedAt
        {
            get;
            private set;
        }

        public Packet(int createdAt)
        {
            CreatedAt = createdAt;
        }
    }
}
