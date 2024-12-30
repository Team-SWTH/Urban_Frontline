// ========================================
// File: UnaimedState.cs
// Created: 2024-12-22 17:47:57
// Author: leeinhwan0421
// ========================================

using UniRx;
using UrbanFrontline.Client.Core.Actor.State.Base;
using UrbanFrontline.Client.Core.Input;

namespace UrbanFrontline.Client.Core.Actor.State.Fire
{
    /// <summary>
    /// [조준]: 아무 행동도 하지 않는 상태
    /// </summary>
    public class UnaimedState : PlayerStateBase
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
        public UnaimedState(PlayerController player, IInputProvider inputProvider)
        {
            Player = player;

            inputProvider.FireInput.Where(_ => IsEnable == true)
                                   .Where(_ => Player.AnimatorController.IsInState("Roll", "Base Layer") == false)
                                   .Where(_ => Player.WeaponController.PossibleShot == true)
                                   .Where(fire => fire == true)
                                   .Subscribe(_ =>
                                   {
                                       Player.SetAimState(Player.AimingState);
                                   }).AddTo(player);

            inputProvider.ADSInput.Where(_ => IsEnable == true)
                                  .Where(_ => Player.AnimatorController.IsInState("Roll", "Base Layer") == false)
                                  .Where(_ => Player.WeaponController.PossibleADS == true)
                                  .Where(ads => ads == true)
                                  .Subscribe(_ =>
                                  {
                                      Player.SetAimState(Player.ADSState);
                                  }).AddTo(player);

            inputProvider.ReloadInput.Where(_ => IsEnable == true)
                                     .Where(_ => Player.AnimatorController.IsInState("Roll", "Base Layer") == false)
                                     .Where(_ => Player.WeaponController.PossibleReload == true)
                                     .Where(reload => reload == true)
                                     .Subscribe(_ =>
                                     {
                                         Player.SetAimState(Player.ReloadState);
                                     }).AddTo(player);
        }

        /// <summary>
        /// UnaimedState 상태에 돌입하였을 때 실행 되는 초기화 함수
        /// </summary>
        public override void Enter()
        {
            base.Enter();

            Player.CameraController.SetWeight(1.0f);

            Player.AnimatorController.Play("Idle", "Upper Layer");
            Player.AnimatorController.SetLayerWeight(0.5f, "Upper Layer");
        }

        /// <summary>
        /// UnaimedState 상태에서 빠져 나갔을 때 실행 되는 초기화 함수
        /// </summary>
        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            if (Player.WeaponController.ShouldReload)
            {
                Player.SetAimState(Player.ReloadState);
            }
        }
    }
}
