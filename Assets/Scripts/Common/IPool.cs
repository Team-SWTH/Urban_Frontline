// ========================================
// File: IPool.cs
// Created: 2025-01-02 19:38:52
// Author: ※ 작성자 이름을 반드시 기입해주세요.
// ========================================

using System.Collections.Concurrent;
using UnityEngine;

namespace UrbanFrontline.Common
{
    public interface IPool<T>
    {
        ConcurrentQueue<T> poolQueue
        { 
            get; 
            set; 
        }

        T Create();

        T Get();

        void Release(T usedObj);
    }
}