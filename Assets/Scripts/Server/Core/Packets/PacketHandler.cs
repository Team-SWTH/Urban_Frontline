// ========================================
// File: PacketHandler.cs
// Created: 2025-01-04 02:33:51
// Author: LHBM04
// ========================================

using System.IO;
using Google.Protobuf;

namespace UrbanFrontline.Server.Core.Packets
{
    /// <summary>
    /// 
    /// </summary>
    public static class PacketHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TPacket"></typeparam>
        /// <param name="packet"></param>
        /// <returns></returns>
        public static byte[] Serialize<TPacket>(IMessage packet) where TPacket : IMessage
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                CodedOutputStream codedStream = new CodedOutputStream(memoryStream);
                packet.WriteTo(memoryStream);
                codedStream.Flush();

                return packet.ToByteArray();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TPacket"></typeparam>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static TPacket Deserialize<TPacket>(byte[] buffer) where TPacket : IMessage, new()
        {
            using (MemoryStream memoryStream = new MemoryStream(buffer))
            {
                CodedInputStream codedStream = new CodedInputStream(memoryStream);
                TPacket packet = new TPacket();
                packet.MergeFrom(memoryStream);

                return packet;
            }
        }
    }
}