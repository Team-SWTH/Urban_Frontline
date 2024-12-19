// ========================================
// File: Client.cs
// Created: 2024-12-20 03:19:13
// Author: LHBM04
// ========================================

using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace UrbanFrontline.Server.Core.Networks
{
    /// <summary>
    /// 클라이언트의 정보를 담습니다.
    /// </summary>
    public sealed class Client
    {
        /// <summary>
        /// 해당 클라이언트의 고유 아이디.
        /// </summary>
        private Guid m_id;
        public Guid ID => m_id;

        /// <summary>
        /// 해당 클라이언트의 IP와 포트 정보를 포함하는 엔드 포인트.
        /// </summary>
        private IPEndPoint m_ipEndPoint;
        public IPEndPoint IPEndPoint => m_ipEndPoint;

        /// <summary>
        /// 클라이언트의 Socket.
        /// </summary>
        private Socket m_socket;
        public Socket Socket => m_socket;

        public Client(IPEndPoint ipEndPoint)
        {
            m_id = Guid.NewGuid();
            m_ipEndPoint = ipEndPoint;
        }
    }
}
