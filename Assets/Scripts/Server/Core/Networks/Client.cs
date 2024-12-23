// ========================================
// File: Client.cs
// Created: 2024-12-20 03:19:13
// Author: LHBM04
// ========================================

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
        #region Buffer
        /// <summary>
        /// 수신 버퍼.
        /// </summary>
        public ReceiveBuffer ReceiveBuffer { get; private set; }

        /// <summary>
        /// 송신 버퍼.
        /// </summary>
        public SendBuffer SendBuffer { get; private set; }
        #endregion

        public Client(IPEndPoint endPoint) // 기본 타임아웃 30초
        {
            ClientID = Guid.NewGuid();
            EndPoint = endPoint;
            IsConnected = false;
            ReceiveBuffer = new(1024);  // 예시 버퍼 크기
            SendBuffer = new(1024);     // 예시 버퍼 크기
        }

        /// <summary>
        /// 해당 서버에 클라이언트를 연결합니다.
        /// </summary>
        /// <param name="endPoint"></param>
        public void Connect(IPEndPoint endPoint)
        {
            EndPoint = endPoint;
            IsConnected = true;
            LastPingTime = DateTime.Now;
            Debug.Log($"Client {ClientID} connected to {EndPoint}.");
        }

        /// <summary>
        /// 해당 클라이언트의 접속을 해제합니다.
        /// </summary>
        public void Disconnect()
        {
            IsConnected = false;
            Debug.Log($"Client {ClientID} disconnected.");
        }

        /// <summary>
        /// 클라이언트의 정보를 업데이트합니다.
        /// </summary>
        /// <param name="pingTime"></param>
        public void Update(DateTime pingTime)
        {
            LastPingTime = pingTime;
        }

        /// <summary>
        /// 클라이언트가 타임아웃되었는지 확인합니다.
        /// </summary>
        /// <returns>타임아웃되었으면 true, 그렇지 않으면 false</returns>
        public bool IsTimedOut(float timeoutDuration)
        {
            return (DateTime.Now - LastPingTime).TotalMilliseconds > timeoutDuration;
        }

        public bool Equals(Client other)
        {
            return other is null ? false : ClientID == other.ClientID;
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
            return left is null ? right is null : left.Equals(right);
        }

        public static bool operator !=(Client left, Client right)
        {
            return !(left == right);
        }
    }
}
