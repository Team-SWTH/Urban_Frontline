// ========================================
// File: Session.cs
// Created: 2024-12-27 06:55:34
// Author: LHBM04
// ========================================

using System;
using System.Net;
using System.Net.Sockets;

namespace UrbanFrontline.Server.Core.Networks
{
    /// <summary>
    /// 세션.
    /// 세션이 곧 클라이언트의 핸들링을 담당할 것.
    /// </summary>
    public class Session : IEquatable<Session>
    {
        /// <summary>
        /// 해당 세션의 ID.
        /// </summary>
        public Guid ID
        {
            get;
            private set;
        }

        /// <summary>
        /// 해당 세션의 연결 여부.
        /// </summary>
        public bool IsConnected
        {
            get;
            private set;
        }

        /// <summary>
        /// 해당 세션의 소켓.
        /// </summary>
        public Socket Socket
        {
            get;
            private set;
        }

        /// <summary>
        /// 해당 세션의 IP 주소.
        /// </summary>
        public IPEndPoint EndPoint 
        { 
            get; 
            private set; 
        }

        public DateTime ConnectedTime
        {
            get;
            private set;
        }

        public DateTime LastActiveTime
        {
            get;
            private set;
        }

        /// <summary>
        /// 세션 연결 이벤트.
        /// </summary>
        public delegate void ConnectEvent();

        public ConnectEvent OnConnected
        {
            get;
            private set;
        }

        public ConnectEvent OnDisconnected
        {
            get;
            private set;
        }

        public void Connect()
        {
            IsConnected = true;
            ConnectedTime = DateTime.Now;
            LastActiveTime = DateTime.Now;
            OnConnected?.Invoke();
        }

        public void NotifyHandlerConnect(SessionHandler handler)
        {
            IsConnected = true;
            
            
        }

        public void Disconnect()
        {
            IsConnected = false;
            OnDisconnected?.Invoke();
        }

        public bool Equals(Session other)
        {
            throw new NotImplementedException();
        }
    }
}
