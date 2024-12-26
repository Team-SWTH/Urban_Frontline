// ========================================
// File: Client.cs
// Created: 2024-12-27 07:07:10
// Author: LHBM04

using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace UrbanFrontline.Server.Core.Networks
{
    /// <summary>
    /// 클라이언트.
    /// </summary>
    [Serializable]
    public class Client : IEquatable<Client>
    {
        /// <summary>
        /// 해당 클라이언트의 소켓.
        /// </summary>
        private Socket m_socket;

        /// <summary>
        /// 해당 클라이언트의 엔드 포인트.
        /// </summary>
        private IPEndPoint m_endPoint;

        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [SerializeField]
        private ReceiveBuffer m_receiveBuffer = new ReceiveBuffer(1024);

        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [SerializeField]
        private SendBuffer m_sendBuffer = new SendBuffer(1024);

        public bool Equals(Client other)
        {
            throw new NotImplementedException();
        }

        public ArraySegment<byte> Receive()
        {
            throw new NotImplementedException();
        }

        public void Send(ArraySegment<byte> buffer)
        {
            throw new NotImplementedException();
        }
    }
}
