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
        public ReceiveBuffer(int size) : base(size)
        {
        }
    }
}
