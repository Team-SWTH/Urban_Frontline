// ========================================
// File: MoveState.cs
// Created: 2024-12-20 12:52:38
// Author: leeinhwan0421
// ========================================

using UniRx;

using UnityEngine;

using UrbanFrontline.Client.Core.Input;
using UrbanFrontline.Client.Core.Actor.State.Base;

namespace UrbanFrontline.Client.Core.Actor.State.Move
{
    /// <summary>
    /// [이동]: 걷는 상태
    /// </summary>
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
                                       if (move.sqrMagnitude <= 0f)
                                       {
                                           Player.SetMoveState(Player.IdleState);
                                       }
                                       else
                                       {
                                           Player.CharacterMovement.Walk(move);
                                       }
                                   })
                                   .AddTo(player);

            inputProvider.RunInput.Where(_ => IsEnable == true)
                                  .Subscribe(run =>
                                  {
                                      if (run == true)
                                      {
                                          Player.SetMoveState(Player.RunState);
                                      }
                                  }).AddTo(player);

            inputProvider.JumpInput.Where(_ => IsEnable == true)
                                   .Where(_ => Player.CharacterMovement.IsGrounded() == true)
                                   .Subscribe(jump =>
                                   {
                                       if (jump)
                                       {
                                           Player.SetMoveState(Player.JumpState);
                                       }
                                   }).AddTo(player);
        }

        /// <summary>
        /// Walk 상태에 돌입하였을 때 실행 되는 초기화 함수
        /// </summary>
        public override void Enter()
        {
            base.Enter();

            Player.AnimatorController.Play("Walk", 0);
            Player.AnimatorController.SetLayerWeight(1.0f, 1);
        }

        /// <summary>
        /// Walk 상태에서 빠져 나갔을 때 실행 되는 초기화 함수
        /// </summary>
        public override void Exit()
        {
            base.Exit();
        }

        /// <summary>
        /// Walk 상태일 때, 매 프레임마다 실행되는 함수
        /// </summary>
        public override void UpdateState()
        {
            base.UpdateState();
        }
    }
}
