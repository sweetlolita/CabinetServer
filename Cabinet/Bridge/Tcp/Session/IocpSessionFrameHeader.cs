using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabinet.Bridge.Tcp.Session
{
    public class IocpSessionFrameHeader
    {
        public const int headerLength = 8;

        const string reserved = "CF";
        public short version { get; set; }
        public int payloadLength { get; set; }

        public IocpSessionFrameHeader()
        {
            version = 1;
        }

        public byte[] serialize()
        {
            byte[] netUnit = new byte[headerLength];
            netUnit[0] = (byte)reserved.ElementAt<char>(0);
            netUnit[1] = (byte)reserved.ElementAt<char>(1);
            netUnit[2] = (byte)(version >> 8);
            netUnit[3] = (byte)(version);
            netUnit[4] = (byte)(payloadLength >> 24);
            netUnit[5] = (byte)(payloadLength >> 16);
            netUnit[6] = (byte)(payloadLength >> 8);
            netUnit[7] = (byte)(payloadLength);
            return netUnit;
        }

        public static IocpSessionFrameHeader deserialize(byte[] netUnit)
        {
            if(netUnit.Length < headerLength)
            {
                return null;
            }
            if(netUnit[0] != (byte)reserved.ElementAt<char>(0)
                && netUnit[1] != (byte)reserved.ElementAt<char>(1))
            {
                return null;
            }
            else
            {
                short unitVersion = (short)((((short)netUnit[2]) << 8) + (short)netUnit[3]);
                IocpSessionFrameHeader header = new IocpSessionFrameHeader();
                header.version = unitVersion;
                header.payloadLength = 
                    (int)(((int)netUnit[4]) << 24) + 
                    (int)(((int)netUnit[5]) << 16) + 
                    (int)(((int)netUnit[6]) << 8) + 
                    (int)netUnit[7];
                return header;
            }
        }
    }
}
