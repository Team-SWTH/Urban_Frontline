// ========================================
// File: PacketBase.cs
// Created: 2025-01-04 06:35:10
// Author: LHBM04
// ========================================

using ProtoBuf;

namespace UrbanFrontline.Server.Core.Packets
{
    /// <summary>
    /// 패킷의 기본 정보를 담습니다.
    /// </summary>
    public abstract class PacketBase
    {
        /// <summary>
        /// 패킷이 가질 수 있는 타입을 정의하는 열거형.
        /// </summary>
        public enum EType
        {
            /// <summary>
            /// 더미.
            /// </summary>
            None = -1,

            #region Enter
            /// <summary>
            /// 플레이어가 서버에 입장했을 때의 패킷.
            /// </summary>
            C_Join = 0,

            /// <summary>
            /// 서버에서 플레이어가 입장했을 때의 패킷.
            /// </summary>
            S_Join = 1,
            #endregion

            #region Leave
            /// <summary>
            /// 플레이어가 서버에서 퇴장했을 때의 패킷.
            /// </summary>
            C_Left = 2,

            /// <summary>
            /// 서버에서 플레이어가 퇴장했을 때의 패킷.
            /// </summary>
            S_Left = 3,
            #endregion
            
            #region Chat
            /// <summary>
            /// 플레이어가 서버에 채팅을 보냈을 때의 패킷.
            /// </summary>
            C_Chat = 4,

            /// <summary>
            /// 서버에서 플레이어가 채팅을 받았을 때의 패킷.
            /// </summary>
            S_Chat = 5,
            #endregion

            #region Movements
            #region Transform
            #region Position
            /// <summary>
            /// 오브젝트가 서버에 위치를 보냈을 때의 패킷.
            /// </summary>
            C_ObjectPosition = 6,

            /// <summary>
            /// 서버에서 오브젝트의 위치를 받았을 때의 패킷.
            /// </summary>
            S_ObjectPosition = 7,
            #endregion

            #region Rotation
            /// <summary>
            /// 오브젝트가 서버에 회전을 보냈을 때의 패킷.
            /// </summary>
            C_ObjectRotation = 8,

            /// <summary>
            /// 서버에서 오브젝트의 회전을 받았을 때의 패킷.
            /// </summary>
            S_ObjectRotation = 9,
            #endregion

            #region Scale
            /// <summary>
            /// 오브젝트가 서버에 크기를 보냈을 때의 패킷.
            /// </summary>
            C_ObjectScale = 10,

            /// <summary>
            /// 서버에서 오브젝트의 크기를 받았을 때의 패킷.
            /// </summary>
            S_ObjectScale = 11,
            #endregion
            #endregion
            #endregion

            C_ObjectAnimation = 12,
            S_ObjectAnimation = 13,
        }

        /// <summary>
        /// 해당 패킷의 타입.
        /// </summary>
        [ProtoMember(1, IsRequired = true)]
        public abstract EType Type 
        { 
            get; 
        }
    }
}