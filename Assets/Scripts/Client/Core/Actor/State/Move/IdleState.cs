// ========================================
// File: IdleState.cs
// Created: 2024-12-20 12:49:16
// Author: leeinhwan0421
// ========================================

using UniRx;

using UnityEngine;

using UrbanFrontline.Client.Core.Actor.State.Base;
using UrbanFrontline.Client.Core.Input;

namespace UrbanFrontline.Client.Core.Actor.State.Move
{
    /// <summary>
    /// [이동]: 아무것도 하지 않는 상태
    /// </summary>
    public class IdleState : PlayerStateBase
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
        public IdleState(PlayerController player, IInputProvider inputProvider)
        {
            Player = player;

            inputProvider.MoveInput.Where(_ => IsEnable == true)
                                   .Where(move => move.sqrMagnitude > 0f)
                                   .Subscribe(_ =>
                                   {
                                       Player.SetMoveState(Player.WalkState);
                                   }).AddTo(player);

            inputProvider.JumpInput.Where(_ => IsEnable == true)
                                   .Where(_ => Player.CharacterMovement.IsGrounded() == true)
                                   .Where(_ => Player.PlayerStamina.Jumpable == true)
                                   .Where(jump => jump == true)
                                   .Subscribe(_ =>
                                   {
                                       Player.SetMoveState(Player.JumpState);
                                   }).AddTo(player);
        }

        /// <summary>
        /// Idle 상태에 돌입하였을 때 실행 되는 초기화 함수
        /// </summary>
        public override void Enter()
        {
            base.Enter();

            Player.AnimatorController.Play("Idle", "Base Layer");
        }

        /// <summary>
        /// Idle 상태에서 빠져 나갔을 때 실행 되는 초기화 함수
        /// </summary>
        public override void Exit()
        {
            base.Exit();
        }

        /// <summary>
        /// Idle 상태에서 매 프레임 호출되는 함수
        /// </summary>
        public override void Update()
        {
            base.Update();

            Player.PlayerStamina.RegenStamina(Player.PlayerStamina.IdleStaminaRegenPerSecond * Time.deltaTime);
        }
    }
}
