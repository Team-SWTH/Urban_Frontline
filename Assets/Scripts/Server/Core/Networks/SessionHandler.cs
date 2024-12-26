// ========================================
// File: SessionHandler.cs
// Created: 2024-12-27 06:55:17
// Author: LHBM04
// ========================================

using System;
using System.Collections.Concurrent;
using UnityEngine;

namespace UrbanFrontline.Server.Core.Networks
{
    /// <summary>
    /// 세션 핸들러.
    /// </summary>
    public class SessionHandler : MonoBehaviour
    {
        private ConcurrentDictionary<Guid, Session> m_sessions;

        private void Awake()
        {
            m_sessions ??= new ConcurrentDictionary<Guid, Session>();
        }

        public bool Has(Guid sessionId)
        {
            return m_sessions.ContainsKey(sessionId);
        }
    }
}
