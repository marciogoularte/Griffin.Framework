﻿using Griffin.Net.Channels;

namespace Griffin.Core.Tests.Net
{
    class SocketBufferFake : ISocketBuffer
    {
        public SocketBufferFake()
        {
            Buffer = new byte[65535];
            Capacity = 65535;
            Offset = 0;
        }

        public int BytesTransferred { get;  set; }
        public int Count { get;  set; }
        public int Capacity { get;  set; }
        public byte[] Buffer { get;  set; }
        public int BaseOffset { get;  set; }
        public int Offset { get;  set; }
        public void SetBuffer(int offset, int count)
        {
            Offset = offset;
            Count = count;
        }

        public void SetBuffer(byte[] buffer, int offset, int count)
        {
            Buffer = buffer;
            Count = count;
            Offset = offset;
        }
    }
}
