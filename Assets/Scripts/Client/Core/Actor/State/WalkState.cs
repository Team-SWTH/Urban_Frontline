// ========================================
// File: MoveState.cs
// Created: 2024-12-20 12:52:38
// Author: leeinhwan0421
// ========================================

using UniRx;
using UnityEngine;
using UrbanFrontline.Client.Core.Input;

namespace UrbanFrontline.Client.Core.Actor.State
{
    public class WalkState : PlayerStateBase
    {
        /// <summary>
        /// 플레이어 컨트롤러 참조 변수
        /// </summary>
        private readonly PlayerController Player;

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="player">플레이어 컨트롤러</param>
        /// <param name="inputProvider">입력 제공자</param>
        public WalkState(PlayerController player, IInputProvider inputProvider)
        {
            Player = player;

            inputProvider.MoveInput.Where(_ => IsEnable == true)
                                   .Subscribe(move =>
                                   {
                                       HandleMoveInput(move);
                                   })
                                   .AddTo(player);
        }

        /// <summary>
        /// Moce 상태에 돌입하였을 때 실행 되는 초기화 함수
        /// </summary>
        public override void Enter()
        {
            base.Enter();

            Player.AnimationController.Play("Walk", 0);
        }

        /// <summary>
        /// Move 상태에서 빠져 나갔을 때 실행 되는 초기화 함수
        /// </summary>
        public override void Exit()
        {
            base.Exit();
        }

        /// <summary>
        /// Move 상태일 때, 매 프레임마다 실행되는 함수
        /// </summary>
        public override void UpdateState()
        {
            base.UpdateState();
        }

        /// <summary>
        /// 이동 입력 처리 로직
        /// </summary>
        /// <param name="move">이동 벡터</param>
        private void HandleMoveInput(Vector2 move)
        {
            if (move.sqrMagnitude <= 0f)
            {
                Player.SetState(Player.IdleState);
            }
            else
            {
                Player.CharacterMovement.Walk(move);
            }
        }
    }
}
