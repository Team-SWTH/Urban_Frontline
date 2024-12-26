// ========================================
// File: BufferBase.cs
// Created: 2024-12-27 08:43:34
// Author: LHBM04
// ========================================

using System;

namespace UrbanFrontline.Server.Core.Networks
{
    /// <summary>
    /// 버퍼의 기본 필드 및 동작을 선언 및 정의합니다.
    /// </summary>
    [Serializable]
    public abstract class BufferBase
    {
        private byte[] m_buffer;

        public ArraySegment<byte> Data
        {
            get { return m_buffer; }
        }

        public int Length
        {
            get { return m_buffer.Length; }
        }

        public BufferBase(int size)
        {
            m_buffer = new byte[size];
        }
    }
}
