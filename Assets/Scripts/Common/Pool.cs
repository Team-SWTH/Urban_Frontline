// ========================================
// File: Pool.cs
// Created: 2025-01-02 18:44:20
// Author: LHBM04
// ========================================

using System;
using System.Collections.Concurrent;

namespace UrbanFrontline.Common
{
    /// <summary>
    /// 기본 Pool 패턴을 구현합니다.
    /// </summary>
    /// <typeparam name="TInstance"></typeparam>
    public class Pool<TInstance> : IPool<TInstance> where TInstance : class, new()
    {
        private ConcurrentQueue<TInstance> m_poolQueue;

        /// <summary>
        /// 생성할 인스턴스의 최대 개수.
        /// </summary>
        public virtual int CountAll
        {
            get { return 15; }
        }

        public int CountActive
        {
            get { return CountAll - CountInactive; }
        }

        public int CountInactive
        {
            get { return m_poolQueue.Count; }
        }

        public TInstance Create()
        {
            return Activator.CreateInstance<TInstance>();
        }

        public TInstance Get()
        {
            return m_poolQueue.TryDequeue(out var instance) ? instance : Create();
        }

        public void Release(TInstance usedObj)
        {
            m_poolQueue.Enqueue(usedObj);
        }
    }
}