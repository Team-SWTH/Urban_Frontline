// ========================================
// File: ICharacterMovement.cs
// Created: 2024-12-20 15:23:52
// Author: leeinhwan0421
// ========================================

using UnityEngine;

namespace UrbanFrontline.Client.Core.Actor.Movement
{
    /// <summary>
    /// 클래스는 미구현 상태
    /// </summary>
    public interface ICharacterMovement
    {
        /// <summary>
        /// 입력된 방향으로 걷는 기능
        /// </summary>
        /// <param name="direction">입력된 방향</param>
        void Walk(Vector2 direction);

        /// <summary>
        /// 입력된 방향으로 뛰는 기능
        /// </summary>
        /// <param name="direction">입력된 방향</param>
        void Run(Vector2 direction);

        /// <summary>
        /// 캐릭터 점프 기능
        /// </summary>
        void Jump();

        /// <summary>
        /// 캐릭터 구르기 기능
        /// </summary>
        /// <param name="time">구르고 난 뒤 흐른 시간</param>
        void Roll(float time);

        /// <summary>
        /// 현재 땅인지를 체크하는 기능
        /// </summary>
        /// <returns>현재 땅에 닿아있다면 true, 아니라면 false</returns>
        bool IsGrounded();
    }
}
