// ========================================
// File: Pool.cs
// Created: 2025-01-02 18:44:20
// Author: LHBM04
// ========================================

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

namespace UrbanFrontline.Common
{
    public class Pool<TInstance> : IPool<TInstance> where TInstance : class, new()
    {
        public ConcurrentQueue<TInstance> poolQueue 
        { 
            get; 
            set; 
        } = new ConcurrentQueue<TInstance>();

        public TInstance Create()
        {
            return Activator.CreateInstance<TInstance>();
        }

        public TInstance Get()
        {
            return poolQueue.TryDequeue(out var instance) ? instance : Create();
        }

        public void Release(TInstance usedObj)
        {
            poolQueue.Enqueue(usedObj);
        }
    }
}