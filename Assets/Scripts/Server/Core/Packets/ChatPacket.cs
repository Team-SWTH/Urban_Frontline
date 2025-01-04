// ========================================
// File: PlayerChat.cs
// Created: 2025-01-04 06:48:03
// Author: LHBM04
// ========================================

using ProtoBuf;
using System;

namespace UrbanFrontline.Server.Core.Packets
{
    /// <summary>
    /// 플레이어가 채팅을 입력했을 때의 패킷.
    /// </summary>
    [ProtoContract]
    public sealed class C_Chat : PacketBase
    {
        public override EType Type
        {
            get { return EType.C_Chat; }
        }

        /// <summary>
        /// 플레이어의 고유 아이디.
        /// </summary>
        [ProtoMember(2, IsRequired = true)]
        public string id
        {
            get;
            set;
        }

        /// <summary>
        /// 플레이어의 이름.
        /// </summary>
        [ProtoMember(3, IsRequired = true)]
        public string name
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [ProtoMember(4, IsRequired = true)]
        public string message
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        [ProtoMember(5, IsRequired = true)]
        public DateTime TimeStamp
        {
            get;
            private set;
        }

        public override void Handle()
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 서버에서 플레이어가 채팅을 입력했을 때의 패킷.
    /// </summary>
    [ProtoContract]
    public sealed class S_Chat : PacketBase
    {
        public override EType Type
        {
            get { return EType.S_Chat; }
        }

        /// <summary>
        /// 플레이어의 고유 아이디.
        /// </summary>
        [ProtoMember(2, IsRequired = true)]
        public string id
        {
            get;
            set;
        }

        /// <summary>
        /// 플레이어의 이름.
        /// </summary>
        [ProtoMember(3, IsRequired = true)]
        public string name
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [ProtoMember(4, IsRequired = true)]
        public string message
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        [ProtoMember(5, IsRequired = true)]
        public DateTime TimeStamp
        {
            get;
            private set;
        }

        public override void Handle()
        {
            throw new NotImplementedException();
        }
    }
}