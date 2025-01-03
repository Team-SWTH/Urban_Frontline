// ========================================
// File: JumpState.cs
// Created: 2024-12-20 12:49:16
// Author: leeinhwan0421
// ========================================

using Cysharp.Threading.Tasks;
using System;
using UniRx;
using UnityEngine;
using UrbanFrontline.Client.Core.Actor.State.Base;
using UrbanFrontline.Client.Core.Input;

namespace UrbanFrontline.Client.Core.Actor.State.Move
{
    /// <summary>
    /// [이동]: 점프 및 착지까지의 상태
    /// </summary>
    public class JumpState : PlayerStateBase
    {
        /// <summary>
        /// 플레이어 컨트롤러 참조 변수
        /// </summary>
        private readonly PlayerController Player;

        /// <summary>
        /// 생성자
        /// </summary>
        public JumpState(PlayerController player, IInputProvider inputProvider)
        {
            Player = player;

            inputProvider.MoveInput.Where(_ => IsEnable == true)
                                   .Subscribe(move =>
                                   {
                                        Player.CharacterMovement.Walk(move);
                                   })
                                   .AddTo(player);
        }

        /// <summary>
        /// Jump 상태에 돌입하였을 때 실행되는 초기화 함수
        /// </summary>
        public override void Enter()
        {
            base.Enter();

            Player.AnimatorController.Play("Jump", "Base Layer");

            Player.CharacterMovement.Jump();

            Player.PlayerStamina.DrainStamina(Player.PlayerStamina.JumpStaminaDrainPerFrame);

            WaitOnGround().Forget();
        }

        /// <summary>
        /// Jump 상태에서 빠져나갔을 때 실행되는 초기화 함수
        /// </summary>
        public override void Exit()
        {
            base.Exit();
        }

        /// <summary>
        /// Jump 상태에서 매 프레임 호출되는 함수
        /// </summary>
        public override void Update()
        {
            base.Update();

            Player.PlayerStamina.RegenStamina(Player.PlayerStamina.WalkStaminaRegenPerSecond * Time.deltaTime);
        }

        public async UniTaskVoid WaitOnGround()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
            await UniTask.WaitUntil(() => Player.CharacterMovement.IsGrounded());

            Player.SetMoveState(Player.IdleState);
        }
    }
}
