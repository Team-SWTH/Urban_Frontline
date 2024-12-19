// ========================================
// File: Session.cs
// Created: 2024-12-20 03:18:29
// Author: LHBM04
// ========================================

using System;
using System.Net.Sockets;
using UnityEngine;

namespace UrbanFrontline.Server.Core.Networks
{
    /// <summary>
    /// 세션의 정보를 담습니다.
    /// </summary>
    public sealed class Session
    {
        /// <summary>
        /// 해당 세션의 주인(클라이언트).
        /// </summary>
        private Client m_owner;
        public Client Owner => m_owner;

        /// <summary>
        /// 해당 세션의 아이디.
        /// </summary>
        private Guid m_id;
        public Guid ID => m_id;

        /// <summary>
        /// 해당 세션이 시작된 시간.
        /// </summary>
        private DateTime m_connectedTime;
        public DateTime ConnectedTime => m_connectedTime;

        /// <summary>
        /// 마지막으로 해당 세션이 활성화된 시점.
        /// </summary>
        private DateTime m_lastActiveTime;
        public DateTime LastActiveTime => m_lastActiveTime;

        public Session(Client client)
        {
            m_owner = client;
            m_id = Guid.NewGuid();
            m_connectedTime = DateTime.Now;
            m_lastActiveTime = DateTime.Now;
        }
    }
}
