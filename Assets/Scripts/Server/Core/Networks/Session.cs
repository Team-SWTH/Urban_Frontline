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
    /// 클라이언트와 서버 간의 세션 정보를 관리합니다.
    /// </summary>
    public sealed class Session : IEquatable<Session>
    {
        /// <summary>
        /// 해당 세션의 주인(클라이언트).
        /// </summary>
        public Client Owner 
        { 
            get; 
        }

        /// <summary>
        /// 해당 세션의 아이디.
        /// </summary>
        public Guid ID 
        { 
            get; 
        }

        /// <summary>
        /// 해당 세션이 시작된 시간.
        /// </summary>
        public DateTime ConnectedTime 
        { 
            get; 
        }

        /// <summary>
        /// 마지막으로 해당 세션이 활성화된 시점.
        /// </summary>
        public DateTime LastActiveTime 
        { 
            get; 
            private set; 
        }

        public Session(Client client)
        {
            Owner = client;
            ID = Guid.NewGuid();
            ConnectedTime = DateTime.Now;
            LastActiveTime = DateTime.Now;
        }

        /// <summary>
        /// 세션의 활성 상태를 업데이트합니다.
        /// </summary>
        public void Update()
        {
            LastActiveTime = DateTime.Now;
        }

        /// <summary>
        /// 세션이 유휴 상태인지 확인합니다.
        /// </summary>
        public bool IsTimedOut(TimeSpan timeout)
        {
            return DateTime.Now - LastActiveTime > timeout;
        }

        /// <summary>
        /// 두 Session 객체가 동일한지 여부를 확인합니다.
        /// </summary>
        /// <param name="other">비교할 다른 Session 객체</param>
        /// <returns>동일하면 true, 그렇지 않으면 false</returns>
        public bool Equals(Session? other)
        {
            return other is null ? false : ID == other.ID;
        }

        public override bool Equals(object? obj)
        {
            return obj is Session session ? Equals(session) : false;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public static bool operator ==(Session? left, Session? right)
        {
            return left is null ? right is null : left.Equals(right);
        }

        public static bool operator !=(Session? left, Session? right)
        {
            return !(left == right);
        }
    }
}
