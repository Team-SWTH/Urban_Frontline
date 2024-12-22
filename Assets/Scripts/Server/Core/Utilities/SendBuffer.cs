// ========================================
// File: SendBuffer.cs
// Created: 2024-12-20 20:36:38
// Author: LHBM04
// ========================================

using System;

namespace UrbanFrontline.Server.Core.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    public class SendBuffer : IBuffer
    {
        public byte[] Buffer { get; private set; }
        public int Length => Buffer.Length;

        private int m_writeIndex;

        public SendBuffer(int size)
        {
            Buffer = new byte[size];
            m_writeIndex = 0;
        }

        public void Write(byte[] bytes)
        {
            if (m_writeIndex + bytes.Length > Buffer.Length)
                throw new InvalidOperationException("버퍼 공간이 부족합니다.");

            Array.Copy(bytes, 0, Buffer, m_writeIndex, bytes.Length);
            m_writeIndex += bytes.Length;
        }

        public void Clear ()
        {
            Array.Clear(Buffer, 0, Buffer.Length);
            m_writeIndex = 0;
        }
    }
}
