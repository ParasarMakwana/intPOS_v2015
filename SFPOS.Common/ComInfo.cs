using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace SFPOS.Common
{
    public static class ComInfo
    {
        public static string ComPort = "COM1";
        public static int BaudRate = 9600;
        public static int DataBits = 7;
        public static StopBits StopBits = StopBits.One;
        public static Parity Parity = Parity.Odd;
        public static Handshake Handshake = Handshake.RequestToSendXOnXOff;
    }
}
