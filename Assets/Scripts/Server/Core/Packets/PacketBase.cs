// ========================================
// File: PacketBase.cs
// Created: 2025-01-03 00:20:09
// Author: ※ 작성자 이름을 반드시 기입해주세요.
// ========================================

using Google.Protobuf;

namespace UrbanFrontline.Server.Core.Packets
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class PacketBase
    {
        public enum EType
        {
            /// <summary>
            /// 더미.
            /// </summary>
            None,


        }

        public abstract EType PacketId { get; } // 패킷 ID
        public abstract void Serialize(CodedOutputStream stream); // 직렬화 메서드
        public abstract void Deserialize(CodedInputStream stream); // 역직렬화 메서드
    }
}
