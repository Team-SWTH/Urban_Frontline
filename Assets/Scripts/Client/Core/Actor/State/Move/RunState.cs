// ========================================
// File: RunState.cs
// Created: 2024-12-21 17:13:40
// Author: leeinhwan0421
// ========================================

using UniRx;

using UnityEngine;

using UrbanFrontline.Client.Core.Actor.State.Base;
using UrbanFrontline.Client.Core.Input;

namespace UrbanFrontline.Client.Core.Actor.State.Move
{
    /// <summary>
    /// [이동]: 달리는 상태
    /// </summary>
    public class RunState : PlayerStateBase
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
        public RunState(PlayerController player, IInputProvider inputProvider)
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
                                           Player.CharacterMovement.Run(move);
                                       }
                                   })
                                   .AddTo(player);

            inputProvider.RunInput.Where(_ => IsEnable == true)
                                  .Subscribe(run =>
                                  {
                                      if (run == false)
                                      {
                                          Player.SetMoveState(Player.WalkState);
                                      }
                                  }).AddTo(player);

            inputProvider.RollInput.Where(_ => IsEnable == true)
                                   .Where(_ => Player.CharacterMovement.IsGrounded() == true)
                                   .Subscribe(roll =>
                                   {
                                       if (roll)
                                       {
                                           Player.SetMoveState(Player.RollState);
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
        /// Run 상태에 돌입하였을 때 실행 되는 초기화 함수
        /// </summary>
        public override void Enter()
        {
            base.Enter();

            Player.AnimatorController.Play("Run", 0);
            Player.AnimatorController.SetLayerWeight(1.0f, 1);
        }

        /// <summary>
        /// Run 상태에서 빠져 나갔을 때 실행 되는 초기화 함수
        /// </summary>
        public override void Exit()
        {
            base.Exit();
        }

        /// <summary>
        /// Run 상태일 때, 매 프레임마다 실행되는 함수
        /// </summary>
        public override void UpdateState()
        {
            base.UpdateState();
        }
    }
}
