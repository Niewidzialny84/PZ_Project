using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protocol
{
    public enum Header : byte
    {
        ACK = 0b0000_0000, 
        ERR = 0b0000_0001,
        DIS = 0b0000_0010,
        LOG = 0b0000_0011,
        SES = 0b0000_0100, 
        REG = 0b0000_0101,
        LIS = 0b0000_0110, 
        ALI = 0b0000_0111, 
        QUI = 0b0000_1000, 
        QUE = 0b0000_1001, 
        NXT = 0b0000_1010, 
        END = 0b0000_1011,
        STA = 0b0000_1100, 
   
    }

    public class HeaderParser
    {
        public static byte[] Encode(Header header, uint size)
        {
            byte[] arr = new byte[3];
            arr[0] = (byte)(((byte)Version.V1) | ((byte)header));
            arr[1] = (byte)((size >> 8) & 0b1111_1111);
            arr[2] = (byte)(size & 0b1111_1111);

            return arr;
        }

        public static Tuple<Header,uint> Decode(byte[] bytes)
        {
            byte version = (byte)(bytes[0] & 0b1100_0000);
            byte headByte = (byte)(bytes[0] & 0b0011_1111);

            Header header = (Header)headByte;
            uint size = (uint)(((bytes[1] << 8) & 0b1111_1111_0000_0000) | ((bytes[2]) & 0b1111_1111));

            return new Tuple<Header, uint>(header, size);
        }
    }
}
