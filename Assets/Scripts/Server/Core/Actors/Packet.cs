// ========================================
// File: Packet.cs
// Created: 2024-12-20 03:59:03
// Author: LHBM04
// ========================================

using System;
using System.IO;
using UnityEngine;

namespace UrbanFrontline.Server.Core.Actors
{
    /// <summary>
    /// 클라이언트와 서버 간의 전달 데이터를 담습니다.
    /// </summary>
    public abstract class Packet : MonoBehaviour
    {
        /// <summary>
        /// 해당 패킷을 구분하기 위한 열거형.
        /// </summary>
        public enum EType
        {
            // 연결 관련 패킷.
            Connect,
            Deconnect,

            // 플레이어 스탯 관련 패킷.
            Transform,
            Health,
            Stamina,

            // 플레이어 행동 관련 패킷.
            Attack,
            
        }

        public abstract EType Type {  get; }

        /// <summary>
        /// 패킷 데이터를 시리얼화하여 바이트 배열로 반환합니다.
        /// </summary>
        public abstract byte[] Serialize();

        /// <summary>
        /// 직렬화된 데이터를 역직렬화하여 패킷 객체로 변환합니다.
        /// </summary>
        public abstract void Deserialize(byte[] data);
    }
}
