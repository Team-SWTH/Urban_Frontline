// ========================================
// File: PlayerEnter.cs
// Created: 2025-01-04 06:34:10
// Author: LHBM04
// ========================================

using ProtoBuf;

namespace UrbanFrontline.Server.Core.Packets
{
    /// <summary>
    /// 플레이어가 서버에 입장했을 때의 패킷.
    /// </summary>
    [ProtoContract]
    public sealed class C_Join : PacketBase
    {
        public override EType Type
        {
            get { return EType.S_Join; }
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

        public override void Handle()
        {
            throw new System.NotImplementedException();
        }
    }

    /// <summary>
    /// 서버에서 플레이어가 입장했을 때의 패킷.
    /// </summary>
    [ProtoContract]
    public sealed class S_Join : PacketBase
    {
        public override EType Type
        {
            get { return EType.S_Join; }
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

        public override void Handle()
        {
            throw new System.NotImplementedException();
        }
    }
}
