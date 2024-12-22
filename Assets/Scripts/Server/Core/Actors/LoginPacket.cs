// ========================================
// File: LoginPacket.cs
// Created: 2024-12-21 16:01:24
// Author: ※ 작성자 이름을 반드시 기입해주세요.
// ========================================

using UnityEngine;

namespace UrbanFrontline.Server.Core.Actors
{
    /// <summary>
    /// 로그인 요청 패킷입니다.
    /// </summary>
    public class LoginPacket : Packet
    {
        public override EType Type => EType.Connect;

        public override void Deserialize(byte[] data)
        {
            throw new System.NotImplementedException();
        }

        public override byte[] Serialize()
        {
            throw new System.NotImplementedException();
        }
    }
}
