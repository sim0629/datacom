using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MathNet.Numerics.Distributions;

namespace Gyumin.Datacom.Models
{
    public class DcfNode : Node
    {
        protected int backoff;
        protected bool isWaitingAck;
        protected int timeToTransmit;
        protected int timeToWait;

        public DcfNode(Medium medium, double lambda)
            : base(medium, lambda)
        {
        }

        protected void WaitForAck()
        {
            timeToWait = Constants.SIFS_TIME + Constants.ACK_TRANS_TIME;
            isWaitingAck = true;
        }

        protected void WaitForTransmit(bool failed)
        {
            backoff = failed ? Math.Min(backoff * 2 + 1, Constants.CW_MAX) : Constants.CW_MIN;
            int sample = DiscreteUniform.Sample(0, backoff);
            timeToWait = !failed && queue.Count > 0 ? Constants.SIFS_TIME : Constants.DIFS_TIME + sample * Constants.SLOT_TIME;
            isWaitingAck = false;
        }

        public override void Next(int elapsed)
        {
            base.Next(elapsed);

            if (queue.Count <= 0) return;
            if (timeToTransmit > 0)
            {
                timeToTransmit--;
                if (timeToTransmit <= 0)
                {
                    WaitForAck();
                }
                return;
            }
            if (isWaitingAck)
            {
                if (timeToWait > 0)
                {
                    timeToWait--;
                    if (timeToWait <= 0)
                    {
                        if (medium.IsAcked)
                        {
                            Dequeue(elapsed);
                            WaitForTransmit(false);
                        }
                        else // fail
                        {
                            WaitForTransmit(true);
                        }
                    }
                }
            }
            else // isWaitingTransmit
            {
                if (medium.IsBusy) return;
                timeToWait--;
                if (timeToWait <= 0)
                {
                    timeToTransmit = Constants.DATA_TRANS_TIME;
                    medium.Request();
                }
            }
        }
    }
}
