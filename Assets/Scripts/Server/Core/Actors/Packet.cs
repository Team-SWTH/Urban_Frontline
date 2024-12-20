// ========================================
// File: Packet.cs
// Created: 2024-12-20 03:59:03
// Author: LHBM04
// ========================================

using System;
using System.IO;

namespace UrbanFrontline.Server.Core.Actors
{
    /// <summary>
    /// 클라이언트와 서버 간의 전달 데이터를 담습니다.
    /// </summary>
    [Serializable]
    public abstract class Packet
    {
        /// <summary>
        /// 해당 패킷을 구분하기 위한 열거형.
        /// </summary>
        public enum EType
        {
            None,
            Transform,
            Health,
            Stamina,
        }

        public virtual EType Type => EType.None;

        /// <summary>
        /// 패킷 데이터를 시리얼화하여 바이트 배열로 반환합니다.
        /// </summary>
        public byte[] Serialize()
        {
            using MemoryStream ms = new MemoryStream();
            using BinaryWriter writer = new BinaryWriter(ms);

            writer.Write((int)Type);
            WriteData(writer);
            return ms.ToArray();
        }

        /// <summary>
        /// 직렬화된 데이터를 역직렬화하여 패킷 객체로 변환합니다.
        /// </summary>
        public static Packet Deserialize(byte[] data)
        {
            using MemoryStream ms = new MemoryStream(data);
            using BinaryReader reader = new BinaryReader(ms);

            return CreatePacket((EType)reader.ReadInt32(), reader);
        }

        /// <summary>
        /// 패킷에 데이터를 기록하는 구체적인 구현을 각 파생 클래스에서 합니다.
        /// </summary>
        protected abstract void WriteData(BinaryWriter writer);

        /// <summary>
        /// 패킷 ID에 맞는 적절한 패킷 객체를 생성합니다.
        /// </summary>
        protected static Packet CreatePacket(EType packetType, BinaryReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
