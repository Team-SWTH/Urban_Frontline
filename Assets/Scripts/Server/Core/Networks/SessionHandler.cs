// ========================================
// File: SessionHandler.cs
// Created: 2024-12-20 03:18:49
// Author: LHBM04
// ========================================

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UrbanFrontline.Server.Core.Networks
{
    /// <summary>
    /// 서버 내 모든 세션을 관리합니다.
    /// </summary>
    public sealed class SessionHandler
    {
        /// <summary>
        /// 현재 서버에서 활성화된 모든 세션을 저장합니다.
        /// </summary>
        private readonly ConcurrentDictionary<Guid, Session> m_sessions;

        public SessionHandler()
        {
            m_sessions = new ConcurrentDictionary<Guid, Session>();
        }

        /// <summary>
        /// 새로운 세션을 등록합니다.
        /// </summary>
        /// <param name="session">등록할 세션</param>
        public void RegisterSession(Session session)
        {
            if (session == null)
            {
                Debug.LogError("세션이 null입니다.");
                return;
            }

            if (!m_sessions.TryAdd(session.ID, session))
            {
                Debug.LogWarning($"세션 {session.ID}는 이미 존재합니다.");
            }
            else
            {
                Debug.Log($"세션 {session.ID} 등록 완료.");
            }
        }

        /// <summary>
        /// 세션 ID로 세션을 조회합니다.
        /// </summary>
        /// <param name="sessionID">조회할 세션 ID</param>
        /// <returns>세션 객체, 없으면 null</returns>
        public Session GetSession(Guid sessionID)
        {
            if (m_sessions.TryGetValue(sessionID, out var session))
            {
                return session;
            }

            Debug.LogWarning($"세션 {sessionID}를 찾을 수 없습니다.");
            return null;
        }

        /// <summary>
        /// 클라이언트에 해당하는 세션을 조회합니다.
        /// </summary>
        /// <param name="client">클라이언트</param>
        /// <returns>세션 객체, 없으면 null</returns>
        public Session GetSession(Client client)
        {
            foreach (var session in m_sessions.Values)
            {
                if (session.Owner.Equals(client))
                {
                    return session;
                }
            }

            Debug.LogWarning($"클라이언트 {client.ID}의 세션을 찾을 수 없습니다.");
            return null;
        }

        /// <summary>
        /// 세션이 이미 존재하는지 확인합니다.
        /// </summary>
        /// <param name="session">세션</param>
        /// <returns>존재하면 true, 없으면 false</returns>
        public bool HasSession(Session session)
        {
            return m_sessions.ContainsKey(session.ID);
        }

        /// <summary>
        /// 세션을 제거합니다.
        /// </summary>
        /// <param name="sessionID">제거할 세션 ID</param>
        public void RemoveSession(Guid sessionID)
        {
            if (m_sessions.TryRemove(sessionID, out var removedSession))
            {
                Debug.Log($"세션 {removedSession.ID} 제거 완료.");
            }
            else
            {
                Debug.LogWarning($"세션 {sessionID}를 제거할 수 없습니다.");
            }
        }

        public bool HasSession(Client client)
        {
            return m_sessions.Any(x => x.Value.Owner == client);
        }

        /// <summary>
        /// 모든 세션을 제거합니다.
        /// </summary>
        public void ClearAllSessions()
        {
            m_sessions.Clear();
            Debug.Log("모든 세션이 제거되었습니다.");
        }

        /// <summary>
        /// 타임아웃된 세션을 주기적으로 확인하고 제거합니다.
        /// </summary>
        /// <param name="timeoutDuration">세션 타임아웃 시간</param>
        public void CleanupSessions(TimeSpan timeoutDuration)
        {
            foreach (var session in new List<Session>(m_sessions.Values))
            {
                if (session.IsTimedOut(timeoutDuration))
                {
                    Debug.Log($"세션 {session.ID}가 타임아웃되었습니다. 제거합니다.");
                    RemoveSession(session.ID);
                }
            }
        }
    }
}
