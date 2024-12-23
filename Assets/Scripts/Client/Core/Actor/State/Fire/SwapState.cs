// ========================================
// File: SwapState.cs
// Created: 2024-12-22 17:47:57
// Author: leeinhwan0421
// ========================================

using UniRx;

using UnityEngine;

using UrbanFrontline.Client.Core.Actor.State.Base;
using UrbanFrontline.Client.Core.Input;

namespace UrbanFrontline.Client.Core.Actor.State.Fire
{
    /// <summary>
    /// [조준]: 무기 변경 상태
    /// </summary>
    public class SwapState : PlayerStateBase
    {
        /// <summary>
        /// 플레이어 컨트롤러 참조 변수
        /// </summary>
        private readonly PlayerController Player;

        /// <summary>
        /// 생성자
        /// </summary>
        /// /// <param name="player">플레이어 컨트롤러</param>
        /// <param name="inputProvider">입력 제공자</param>
        public SwapState(PlayerController player)
        {
            Player = player;
        }

        /// <summary>
        /// SwapState 상태에 돌입하였을 때 실행 되는 초기화 함수
        /// </summary>
        public override void Enter()
        {
            base.Enter();
        }

        /// <summary>
        /// SwapState 상태에서 빠져 나갔을 때 실행 되는 초기화 함수
        /// </summary>
        public override void Exit()
        {
            base.Exit();
        }
    }
}
