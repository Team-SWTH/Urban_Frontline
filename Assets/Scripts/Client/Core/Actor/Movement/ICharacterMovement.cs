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
        void Roll();
    }
}
