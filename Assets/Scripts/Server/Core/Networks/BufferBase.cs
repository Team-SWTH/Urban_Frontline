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
    public abstract class BufferBase
    {
        /// <summary>
        /// 데이터를 저장할 버퍼.
        /// </summary>
        private byte[] m_buffer;

        /// <summary>
        /// 버퍼의 사이즈.
        /// </summary>
        public virtual ushort Size
        {
            get { return 1024; }
        }

        /// <summary>
        /// 저장된 데이터.
        /// </summary>
        public ArraySegment<byte> Data
        {
            get { return m_buffer; }
        }
        
        /// <summary>
        /// 저장된 데이터의 길이.
        /// </summary>
        public int Length
        {
            get { return m_buffer.Length; }
        }

        public BufferBase()
        {
            m_buffer = new byte[Size];
        }
    }
}
