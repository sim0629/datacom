using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gyumin.Datacom.Models
{
    public class Medium
    {
        private bool acking;
        private int timeToTransmit;
        private int timeToWait;
        private int voltageLevel;

        public bool IsAcked
        {
            get { return acking; }
        }

        public bool IsBusy
        {
            get { return timeToTransmit > 0; }
        }

        public bool IsIdle
        {
            get { return !IsBusy; }
        }

        public void Next()
        {
            if (timeToWait > 0)
            {
                timeToWait--;
                if (timeToWait <= 0)
                {
                    if (voltageLevel == 1)
                    {
                        timeToTransmit = Constants.ACK_TRANS_TIME;
                        acking = true;
                        AckCount++;
                    }
                    voltageLevel = 0;
                }
                return;
            }

            if (IsIdle)
            {
                if (voltageLevel > 0)
                {
                    timeToTransmit = Constants.DATA_TRANS_TIME;
                    acking = false;
                }
            }
            else if (IsBusy)
            {
                timeToTransmit--;
                if (timeToTransmit <= 0)
                {
                    if (!acking)
                    {
                        timeToWait = Constants.SIFS_TIME;
                    }
                }
            }
        }

        public bool Request()
        {
            if (IsBusy) return false;
            voltageLevel++;
            RequestCount++;
            return true;
        }

        #region Statistics

        public int AckCount
        {
            get;
            private set;
        }

        public int RequestCount
        {
            get;
            private set;
        }

        #endregion Statistics
    }
}
