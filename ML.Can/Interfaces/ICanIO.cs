using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Can.Interfaces
{
    public interface ICanIO
    {
        string OpenCAN(string canName, int baundRate);
        string CloseCan();
        void SendData(List<CanDriver.canmsg_t> data);
        List<CanDriver.canmsg_t> ReceiveMsgBlock(int msgCount);
    }
}
