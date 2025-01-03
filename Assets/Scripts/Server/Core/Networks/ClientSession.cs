// ========================================
// File: ClientSession.cs
// Created: 2025-01-03 05:52:26
// Author: LHBM04
// ========================================

using System;
using System.Net;
using UrbanFrontline.Server.Core.Utilities;

namespace UrbanFrontline.Server.Core.Networks
{
    // Step 10: ClientSession 생성
    // 클라이언트와의 세션 관리
    public class ClientSession : SessionBase
    {
        // 연결 성공 시 호출
        protected override void OnConnected(EndPoint endPoint)
        {
            Logger.LogNotice($"Connected to {endPoint}");
        }

        // 패킷 수신 시 호출
        protected override void OnReceivePacket(ArraySegment<byte> payload)
        {
            // PacketManager.Instance.HandlePacket(this, packetId, payload);
        }

        // 데이터 전송 완료 시 호출
        protected override void OnSend(int numBytes)
        {
            Logger.Log($"Sent {numBytes} bytes");
        }

        // 연결 해제 시 호출
        protected override void OnDisconnected()
        {
            Logger.LogNotice("Disconnected");
        }
    }
}