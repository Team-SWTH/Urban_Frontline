// ========================================
// File: Session.cs
// Created: 2024-12-27 06:55:34
// Author: LHBM04
// ========================================

using System;
using UnityEngine;

namespace UrbanFrontline.Server.Core.Networks
{
    /// <summary>
    /// 세션.
    /// 세션이 곧 클라이언트의 핸들링을 담당할 것.
    /// </summary>
    [Serializable]
    public class Session : IEquatable<Session>
    {
        [SerializeField]
        private string m_id;
        public Guid Id { get { return Guid.Parse(m_id); } }

        [SerializeField]
        private Client m_client;

        [SerializeField]
        private string m_connectedTime;
        public DateTime ConnectedTime { get { return DateTime.Parse(m_connectedTime); } }

        [SerializeField]
        private string m_lastActiveTime;
        public DateTime LastActiveTime { get { return DateTime.Parse(m_lastActiveTime); } }

        public Session(Guid id, Client client)
        {
            m_id = id.ToString();
            m_client = client;
            m_connectedTime = DateTime.Now.ToString();
            m_lastActiveTime = DateTime.Now.ToString();
        }

        public void Connect()
        {

        }

        public void Update()
        {
            // TODO: 데이터(패킷) 처리 로직 작성.

            // 세션의 마지막 활동 시간을 갱신한다.
            m_lastActiveTime = DateTime.Now.ToString();
        }

        public void Disconnect()
        {
        }

        public bool Equals(Session other)
        {
            throw new NotImplementedException();
        }
    }
}
