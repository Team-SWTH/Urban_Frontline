// ========================================
// File: ClientSession.cs
// Created: 2025-01-04 09:39:46
// Author: LHBM04
// ========================================

using System;
using System.Collections.Generic;
using System.Net;

namespace UrbanFrontline.Server.Core.Networks
{
    public class PacketSession : SessionBase
    {
        protected override void OnConnected(EndPoint endPoint)
        {
            Console.WriteLine($"[ClientSession] Connected to {endPoint}");
            // 초기화 코드 추가 가능.
        }

        protected override void OnDisconnected()
        {
            Console.WriteLine("[ClientSession] Disconnected");
            // 정리 코드 추가 가능.
        }

        protected override void OnReceivePacket(ArraySegment<byte> payload)
        {
            // if (payload.Count < 4)
            // {
            //     Console.WriteLine("[ClientSession] Invalid packet size");
            //     return;
            // }
            // 
            // // 패킷 ID 추출
            // ushort packetId = BitConverter.ToUInt16(payload.Array, payload.Offset);
            // if (m_packetHandlers.TryGetValue(packetId, out var handler))
            // {
            //     handler(new ArraySegment<byte>(payload.Array, payload.Offset + 2, payload.Count - 2));
            // }
            // else
            // {
            //     Console.WriteLine($"[ClientSession] Unknown packet ID: {packetId}");
            // }
        }

        protected override void OnSendPacket(int numBytes)
        {
            Console.WriteLine($"[ClientSession] Sent {numBytes} bytes");
            // 송신 관련 작업 추가 가능.
        }
    }
}
