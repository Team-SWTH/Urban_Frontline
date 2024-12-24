// ========================================
// File: ReloadState.cs
// Created: 2024-12-24 19:30:51
// Author: leeinhwan0421
// ========================================

using Cysharp.Threading.Tasks;
using UnityEngine;

using UrbanFrontline.Client.Core.Actor.State.Base;
using UrbanFrontline.Client.Core.Input;

namespace UrbanFrontline.Client.Core.Actor.State.Fire
{
    /// <summary>
    /// [조준]: 아무 행동도 하지 않는 상태
    /// </summary>
    public class ReloadState : PlayerStateBase
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
        public ReloadState(PlayerController player)
        {
            Player = player;
        }

        /// <summary>
        /// ReloadState 상태에 돌입하였을 때 실행 되는 초기화 함수
        /// </summary>
        public override void Enter()
        {
            base.Enter();

            Player.CameraController.SetWeight(1.0f);

            Player.AnimatorController.Play(Player.WeaponController.ReloadStateName, "Upper Layer");
            Player.AnimatorController.SetLayerWeight(1.0f, "Upper Layer");

            WaitOnEndState().Forget();
        }

        /// <summary>
        /// ReloadState 상태에서 빠져 나갔을 때 실행 되는 초기화 함수
        /// </summary>
        public override void Exit()
        {
            base.Exit();
        }

        private async UniTaskVoid WaitOnEndState()
        {
            await UniTask.WaitUntil(() => Player.AnimatorController.IsEndState(Player.WeaponController.ReloadStateName, "Upper Layer"));

            Player.WeaponController.Reload();
            Player.SetAimState(Player.UnaimedState);
        }
    }
}
