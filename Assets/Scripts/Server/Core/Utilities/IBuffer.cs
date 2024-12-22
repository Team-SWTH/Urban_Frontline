// ========================================
// File: IBuffer.cs
// Created: 2024-12-20 23:56:41
// Author: LHBM04
// ========================================

namespace UrbanFrontline.Server.Core.Utilities
{
    /// <summary>
    /// 버퍼 인터페이스.
    /// </summary>
    public interface IBuffer
    {
        /// <summary>
        /// 
        /// </summary>
        byte[] Buffer { get; }

        /// <summary>
        /// 버퍼의 크기.
        /// </summary>
        int Length { get; }

        /// <summary>
        /// 버퍼의 데이터를 초기화합니다.
        /// </summary>
        void Clear();
    }
}
