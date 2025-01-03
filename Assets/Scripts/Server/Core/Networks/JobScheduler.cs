// ========================================
// File: JobScheduler.cs
// Created: 2025-01-04 01:58:08
// Author: LHBM04
// ========================================

using System;
using System.Collections.Concurrent;
using UnityEngine;

namespace UrbanFrontline.Server.Core.Networks
{
    /// <summary>
    /// 작업을 Action으로 캡슐화하여 관리합니다.
    /// </summary>
    public class JobScheduler : MonoBehaviour
    {
        /// <summary>
        /// 캡슐화된 작업을 저장하는 Thread-Safe한 Queue.
        /// </summary>
        public ConcurrentQueue<Action> Schedul
        {
            get;
        } = new ConcurrentQueue<Action>();

        private void Update()
        {
            while (Schedul.TryDequeue(out Action action))
            {
                action.Invoke();
            }
        }
    }
}