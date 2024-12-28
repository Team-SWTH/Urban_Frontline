// ========================================
// File: ReceiveBuffer.cs
// Created: 2024-12-27 08:37:08
// Author: LHBM04
// ========================================

using System;

namespace UrbanFrontline.Server.Core.Networks
{
    /// <summary>
    /// 데이터(패킷)를 수신받을 때 사용되는 버퍼.
    /// </summary>
    public class ReceiveBuffer : BufferBase
    {
        /// <summary>
        /// 현재 읽고 있는 위치.
        /// </summary>
        private int m_readPosition;

        /// <summary>
        /// 현재 쓰고 있는 위치.
        /// </summary>
        private int m_writePosition;

        public int DataSize
        {
            get { return m_writePosition - m_readPosition; }
        }

        public int FreeSize
        {
            get { return Length - m_writePosition; }
        }

        public ArraySegment<byte> ReadSegment
        {
            get { return new ArraySegment<byte>(Data.Array, Data.Offset + m_readPosition, DataSize); }
        }

        public ArraySegment<byte> WriteSegment
        {
            get { return new ArraySegment<byte>(Data.Array, Data.Offset + m_writePosition, FreeSize); }
        }

        public ReceiveBuffer(int size) : base(size)
        {
            m_readPosition = 0;
            m_writePosition = 0;
        }

        public bool OnRead(int numOfBytes)
        {
            if (numOfBytes > DataSize)
            {
                return false;
            }

            m_readPosition += numOfBytes;
            return true;
        }

        public bool OnWrite(int numOfBytes)
        {
            if (numOfBytes > FreeSize)
            {
                return false;
            }

            m_writePosition += numOfBytes;
            return true;
        }

        /// <summary>
        /// 버퍼를 정리합니다.
        /// </summary>
        public void Clear()
        {
            if (DataSize <= 0)
            {
                m_readPosition = 0;
                m_writePosition = 0;
            }
            else
            {
                Array.Copy(Data.Array, Data.Offset + m_readPosition, Data.Array, Data.Offset, DataSize);
                m_readPosition = 0;
                m_writePosition = DataSize;
            }
        }
    }
}
