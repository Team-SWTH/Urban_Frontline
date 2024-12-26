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
        public SendBuffer(int size) : base(size)
        {
        }
    }
}
