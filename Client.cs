// ========================================
// File: Client.cs
// Created: 2024-12-20 03:19:13
// Author: LHBM04
// ========================================

using NaughtyAttributes;
using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UrbanFrontline.Server.Core.Utilities;

namespace UrbanFrontline.Server.Core.Networks
{
    /// <summary>
    /// 서버에 연결된 클라이언트의 네트워크 정보를 관리합니다.
    /// </summary>
    public class Client : IEquatable<Client>
    {
        /// <summary>
        /// 클라이언트 고유 ID.
        /// </summary>
        public Guid ClientID { get; private set; }

        /// <summary>
        /// 클라이언트 IP 주소 및 포트.
        /// </summary>
        public IPEndPoint EndPoint { get; private set; }

        /// <summary>
        /// 클라이언트 이름.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 클라이언트 연결 상태.
        /// </summary>
        public bool IsConnected { get; private set; }

        /// <summary>
        /// 클라이언트 마지막 핑 시간.   
        /// </summary>
        public DateTime LastPingTime { get; private set; }

        /// <summary>
        /// 클라이언트 핑.
        /// </summary>
        public float Ping => (float)(DateTime.Now - LastPingTime).TotalMilliseconds;

        /// <summary>
        /// 수신 버퍼.
        /// </summary>
        public ReceiveBuffer ReceiveBuffer { get; private set; }

        /// <summary>
        /// 송신 버퍼.
        /// </summary>
        public SendBuffer SendBuffer { get; private set; }

        public Client(IPEndPoint endPoint)
        {
            ClientID = Guid.NewGuid();
            EndPoint = endPoint;
            IsConnected = false;
            ReceiveBuffer = new(1024);  // 예시 버퍼 크기
            SendBuffer = new(1024);     // 예시 버퍼 크기
        }

        public void Connect(IPEndPoint endPoint)
        {
            EndPoint = endPoint;
            IsConnected = true;
            Debug.Log($"Client {ClientID} connected to {EndPoint}.");
        }

        public void Disconnect()
        {
            IsConnected = false;
            Debug.Log($"Client {ClientID} disconnected.");
        }

        public void Update(DateTime pingTime)
        {
            LastPingTime = pingTime;
        }

        public bool Equals(Client other)
        {
            if (other is null) return false;
            return ClientID == other.ClientID;
        }

        public override bool Equals(object obj)
        {
            return obj is Client client && Equals(client);
        }

        public override int GetHashCode()
        {
            return ClientID.GetHashCode();
        }

        public static bool operator ==(Client left, Client right)
        {
            if (left is null) return right is null;
            return left.Equals(right);
        }

        public static bool operator !=(Client left, Client right)
        {
            return !(left == right);
        }
    }
}
