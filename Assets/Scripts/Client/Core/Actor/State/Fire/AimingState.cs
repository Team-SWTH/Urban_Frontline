// ========================================
// File: AimingState.cs
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
    /// [조준]: 견착 사격 상태
    /// </summary>
    public class AimingState : PlayerStateBase
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
        public AimingState(PlayerController player, IInputProvider inputProvider)
        {
            Player = player;

            inputProvider.ADSInput.Where(_ => IsEnable == true)
                                  .Where(ads => ads == true)
                                  .Subscribe(_ =>
                                  {
                                      Player.SetAimState(Player.ADSState);
                                  }).AddTo(player);

            inputProvider.FireInput.Where(_ => IsEnable == true)
                                   .Where(fire => fire == false)
                                   .Subscribe(_ =>
                                   {
                                       Player.SetAimState(Player.IdleState);
                                   }).AddTo(player);
        }

        /// <summary>
        /// AimingState 상태에 돌입하였을 때 실행 되는 초기화 함수
        /// </summary>
        public override void Enter()
        {
            base.Enter();

            Player.AnimatorController.Play("Shot", "Upper Layer");
            Player.AnimatorController.SetLayerWeight(1.0f, "Upper Layer");
        }

        /// <summary>
        /// AimingState 상태에서 빠져 나갔을 때 실행 되는 초기화 함수
        /// </summary>
        public override void Exit()
        {
            base.Exit();
        }
    }
}
