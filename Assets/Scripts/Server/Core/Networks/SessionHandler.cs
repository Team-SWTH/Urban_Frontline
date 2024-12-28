// ========================================
// File: SessionHandler.cs
// Created: 2024-12-27 06:55:17
// Author: LHBM04
// ========================================

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;

namespace UrbanFrontline.Server.Core.Networks
{
    /// <summary>
    /// 세션 핸들러.
    /// </summary>
    public class SessionHandler : MonoBehaviour
    {
        /// <summary>
        /// 세션 목록.
        /// </summary>
        private ConcurrentDictionary<Guid, Session> m_sessions;

        private void Awake()
        {
            m_sessions = new ConcurrentDictionary<Guid, Session>();
        }

        public void HandleSession()
        {
            List<Guid> disconnectedSessions = new List<Guid>();
            foreach (Session session in m_sessions.Values)
            {
                if (!session.IsConnected)
                {
                    disconnectedSessions.Add(session.ID);
                    continue;
                }

                // TODO: 세션 
            }

            if (disconnectedSessions.Count > 0)
            {
                foreach (Guid sessionId in disconnectedSessions)
                {
                    m_sessions.TryRemove(sessionId, out _);
                }
                disconnectedSessions.Clear();
            }
        }

        #region Session Helper
        public bool AddSession(Guid sessionId, Session session)
        {
            return m_sessions.TryAdd(sessionId, session);
        }

        public bool Remove(Guid sessionId)
        {
            return m_sessions.TryRemove(sessionId, out _);
        }
        
        public bool Has(Guid sessionId)
        {
            return m_sessions.ContainsKey(sessionId);
        }

        public bool Has(IPAddress address)
        {
            return m_sessions.Any(pair => pair.Value.EndPoint.Address == address);
        }

        public bool Has(IPEndPoint endPoint)
        {
            return m_sessions.Any(pair => pair.Value.EndPoint == endPoint);
        }

        public bool Has(Func<KeyValuePair<Guid, Session>, bool> predicate)
        {
            return m_sessions.Any(predicate);
        }
        #endregion

    }
}
