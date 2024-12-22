// ========================================
// File: ReceiveBuffer.cs
// Created: 2024-12-20 23:52:32
// Author: LHBM04
// ========================================

using System;

namespace UrbanFrontline.Server.Core.Utilities
{
    public class ReceiveBuffer : IBuffer
    {
        public byte[] Buffer { get; private set; }
        public int Length => Buffer.Length;


        public ReceiveBuffer(int size)
        {
            Buffer = new byte[size];
        }

        public void Read(byte[] source)
        {
            if (source.Length > Buffer.Length)
            {
                throw new InvalidOperationException("읽을 데이터가 부족합니다.");
            }

            Array.Copy(source, Buffer, source.Length);
        }

        public void Clear()
        {
            Array.Clear(Buffer, 0, Buffer.Length);
        }
    }
}
