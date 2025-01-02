// ========================================
// File: SendBuffer.cs
// Created: 2024-12-27 08:37:22
// Author: LHBM04
// ========================================

using System;

namespace UrbanFrontline.Server.Core.Networks
{
    /// <summary>
    /// 데이터(패킷)을 송신할 때 사용되는 버퍼.
    /// </summary>
    public class SendBuffer : BufferBase
    {
        public override ushort Size
        {
            get { return 16384; }
        }

        /// <summary>
        /// 현재 쓰고 있는 위치.
        /// </summary>
        private int m_writePosition;

        /// <summary>
        /// 해당 버퍼의 여유 공간.
        /// </summary>
        public int Clearance
        {
            get { return Length - m_writePosition; }
        }

        public SendBuffer() : base()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numOfBytes"></param>
        /// <returns></returns>
        public ArraySegment<byte> Open(int numOfBytes)
        {
            return numOfBytes > Clearance ? new ArraySegment<byte>() : 
                                            new ArraySegment<byte>(Data.Array, m_writePosition, numOfBytes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numOfBytes"></param>
        /// <returns></returns>
        public ArraySegment<byte> Close(int numOfBytes)
        {
            ArraySegment<byte> result = new ArraySegment<byte>(Data.Array, m_writePosition, numOfBytes);
            m_writePosition += numOfBytes;
            return result;
        }
    }
}
