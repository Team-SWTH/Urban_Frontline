// ========================================
// File: IPool.cs
// Created: 2025-01-02 19:38:52
// Author: LHBM04
// ========================================

using System.Collections.Concurrent;

namespace UrbanFrontline.Common
{
    public interface IPool<T>
    {
        ConcurrentQueue<T> poolQueue
        { 
            get; 
            set; 
        }

        int CountActive
        {
            get;
        }

        int CountInactive
        {
            get;
        }

        T Create();

        T Get();

        void Release(T usedObj);
    }
}