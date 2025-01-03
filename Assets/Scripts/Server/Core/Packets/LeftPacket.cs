// ========================================
// File: PlayerLeave.cs
// Created: 2025-01-04 06:47:48
// Author: LHBM04
// ========================================

using ProtoBuf;
using System;

namespace UrbanFrontline.Server.Core.Packets
{
    /// <summary>
    /// 플레이어가 서버에서 퇴장했을 때의 패킷.
    /// </summary>
    public sealed class C_Left : PacketBase
    {
        public override EType Type
        {
            get { return EType.C_Left; }
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
        /// 플레이어의 퇴장 시간.
        /// </summary>
        [ProtoMember(3, IsRequired = true)]
        public DateTime TimeStamp
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 서버에서 플레이어가 퇴장했을 때의 패킷.
    /// </summary>
    public sealed class S_Left : PacketBase
    {
        public override EType Type
        {
            get { return EType.S_Left; }
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
        /// 플레이어의 퇴장 시간.
        /// </summary>
        [ProtoMember(3, IsRequired = true)]
        public DateTime TimeStamp
        {
            get;
            set;
        }
    }
}