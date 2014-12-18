using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gyumin.Datacom.Models
{
    public static class Constants
    {
        private const int UNIT_TIME_US = 10;
        private const int SIFS_TIME_US = 10;
        private const int DIFS_TIME_US = 50;
        private const int SLOT_TIME_US = 20;
        private const int DATA_PACKET_SIZE_B = 1024;
        private const int ACK_PACKET_SIZE_B = 32;
        private const int BITS_PER_10US = 100; // 10 Mbps
        private const int SIMUL_DURATION_TIME_S = 60 * 60; // 1 hour

        public const int ONE_SECOND_TIME = 1000 * 1000 / UNIT_TIME_US;
        public const int SIFS_TIME = SIFS_TIME_US / UNIT_TIME_US;
        public const int DIFS_TIME = DIFS_TIME_US / UNIT_TIME_US;
        public const int SLOT_TIME = SLOT_TIME_US / UNIT_TIME_US;
        public const int DATA_TRANS_TIME = (DATA_PACKET_SIZE_B * 8 + BITS_PER_10US - 1) / BITS_PER_10US;
        public const int ACK_TRANS_TIME = (ACK_PACKET_SIZE_B * 8 + BITS_PER_10US - 1) / BITS_PER_10US;
        public const int SIMUL_DURATION_TIME = SIMUL_DURATION_TIME_S / UNIT_TIME_US * 1000 * 1000;
        public const int CW_MIN = 0;
        public const int CW_MAX = 255;
    }
}
