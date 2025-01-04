// ========================================
// File: PacketHandler.cs
// Created: 2025-01-04 02:33:51
// Author: LHBM04
// ========================================

using System.IO;
using ProtoBuf;

namespace UrbanFrontline.Server.Core.Packets
{
    /// <summary>
    /// 송/수신된 패킷을 제어합니다.
    /// </summary>
    public static class PacketHandler
    {
        /// <summary>
        /// 패킷을 직렬화합니다.
        /// </summary>
        /// <typeparam name="TPacket">작렬화할 패킷의 종류.</typeparam>
        /// <param name="packet">직렬화할 패킷.</param>
        /// <returns>직렬화된 패킷.</returns>
        public static byte[] Serialize<TPacket>(TPacket packet) where TPacket : PacketBase
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Serializer.Serialize(memoryStream, packet);
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// 패킷을 역직렬화합니다.
        /// </summary>
        /// <typeparam name="TPacket">역작렬화할 패킷의 종류.</typeparam>
        /// <param name="buffer">역직렬화할 패킷.</param>
        /// <returns>역직렬화된 패킷.</returns>
        public static TPacket Deserialize<TPacket>(byte[] buffer) where TPacket : PacketBase
        {
            using (MemoryStream memoryStream = new MemoryStream(buffer))
            {
                return Serializer.Deserialize<TPacket>(memoryStream);
            }
        }
    }
}
