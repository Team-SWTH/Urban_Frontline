// ========================================
// File: IPool.cs
// Created: 2025-01-02 19:38:52
// Author: LHBM04
// ========================================

namespace UrbanFrontline.Common
{
    /// <summary>
    /// Pool 패턴의 동작 시그니처를 정의합니다.
    /// </summary>
    /// <typeparam name="T">Pooling할 인스턴스의 타입.</typeparam>
    public interface IPool<T>
    {
        /// <summary>
        /// 현재 사용되고 있는 인스턴스의 개수.
        /// </summary>
        int CountActive
        {
            get;
        }

        /// <summary>
        /// 현재 저장되어 있는 인스턴스의 개수.
        /// </summary>
        int CountInactive
        {
            get;
        }

        /// <summary>
        /// 인스턴스를 생성합니다.
        /// </summary>
        /// <returns>생성한 인스턴스.</returns>
        T Create();

        /// <summary>
        /// 사용할 인스턴스를 가져옵니다.
        /// </summary>
        /// <returns>사용할 인스턴스.</returns>
        T Get();

        /// <summary>
        /// 사용한 인스턴스를 반환합니다.
        /// </summary>
        /// <param name="usedObj">사용한 인스턴스.</param>
        void Release(T usedObj);
    }
}