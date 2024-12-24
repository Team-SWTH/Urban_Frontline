// ========================================
// File: RollState.cs
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
    /// [이동]: 구를 경우의 상태
    /// </summary>
    public class RollState : PlayerStateBase
    {
        /// <summary>
        /// 플레이어 컨트롤러 참조 변수
        /// </summary>
        private readonly PlayerController Player;

        /// <summary>
        /// 생성자
        /// </summary>
        /// /// <param name="player">플레이어 컨트롤러</param>
        public RollState(PlayerController player)
        {
            Player = player;
        }

        /// <summary>
        /// Roll 상태에 돌입하였을 때 실행 되는 초기화 함수
        /// </summary>
        public override void Enter()
        {
            base.Enter();

            Player.AnimatorController.Play("Roll", "Base Layer");
            Player.AnimatorController.SetLayerWeight(0.0f, "Upper Layer");

            WaitOnEndState().Forget();
        }

        /// <summary>
        /// Roll 상태에서 빠져 나갔을 때 실행 되는 초기화 함수
        /// </summary>
        public override void Exit()
        {
            base.Exit();
        }

        /// <summary>
        /// Roll 상태일 때, 매 프레임마다 실행되는  함수
        /// </summary>
        private async UniTaskVoid WaitOnEndState()
        {
            float rollTime = 0.0f;

            while (!Player.AnimatorController.IsEndState("Roll", "Base Layer"))
            {
                Player.AnimatorController.SetLayerWeight(0.0f, "Upper Layer");
                Player.SetAimState(Player.UnaimedState);

                rollTime += Time.deltaTime;
                Player.CharacterMovement.Roll(rollTime);

                await UniTask.WaitForEndOfFrame();
            }

            Player.SetMoveState(Player.IdleState);
        }
    }
}
