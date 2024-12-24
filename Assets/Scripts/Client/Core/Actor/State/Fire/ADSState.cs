// ========================================
// File: ADSState.cs
// Created: 2024-12-22 17:48:12
// Author: leeinhwan0421
// ========================================

using R3;

using UnityEngine;

using UrbanFrontline.Client.Core.Actor.State.Base;
using UrbanFrontline.Client.Core.Input;

namespace UrbanFrontline.Client.Core.Actor.State.Fire
{
    /// <summary>
    /// [조준]: 정조준 상태
    /// </summary>
    public class ADSState : PlayerStateBase
    {
        /// <summary>
        /// 플레이어 컨트롤러 참조 변수
        /// </summary>
        private readonly PlayerController Player;

        /// <summary>
        /// ADSState에서의 fov 가중치
        /// </summary>
        private readonly float fovWeight = 0.6f;

        /// <summary>
        /// 생성자
        /// </summary>
        /// /// <param name="player">플레이어 컨트롤러</param>
        /// <param name="inputProvider">입력 제공자</param>
        public ADSState(PlayerController player, IInputProvider inputProvider)
        {
            Player = player;

            inputProvider.ADSInput.Where(_ => IsEnable == true)
                                  .Where(ads => ads == false)
                                  .Subscribe(_ =>
                                  {
                                      Player.SetAimState(Player.UnaimedState);
                                  }).AddTo(player);
        }

        /// <summary>
        /// ADSState 상태에 돌입하였을 때 실행 되는 초기화 함수
        /// </summary>
        public override void Enter()
        {
            base.Enter();

            Player.CameraController.SetWeight(fovWeight);

            Player.AnimatorController.Play("Shot", "Upper Layer");
            Player.AnimatorController.SetLayerWeight(1.0f, "Upper Layer");
        }

        /// <summary>
        /// ADSState 상태에서 빠져 나갔을 때 실행 되는 초기화 함수
        /// </summary>
        public override void Exit()
        {
            base.Exit();
        }
    }
}
