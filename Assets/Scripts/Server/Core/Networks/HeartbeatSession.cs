// ========================================
// File: HeartbeatSession.cs
// Created: 2025-01-04 09:41:42
// Author: ※ 작성자 이름을 반드시 기입해주세요.
// ========================================

using System;
using System.Net;
using UnityEngine;

namespace UrbanFrontline.Server.Core.Networks
{
    /// <summary>
    /// 서버와 클라이언트 간의 연결을 확인하고, 이에 대한 처리를 진행합니다.
    /// </summary>
    public class HeartbeatSession : SessionBase
    {
        protected override void OnConnected(EndPoint endPoint)
        {
            throw new NotImplementedException();
        }

        protected override void OnDisconnected()
        {
            throw new NotImplementedException();
        }

        protected override void OnReceivePacket(ArraySegment<byte> payload)
        {
            throw new NotImplementedException();
        }

        protected override void OnSendPacket(int numBytes)
        {
            throw new NotImplementedException();
        }
    }
}
