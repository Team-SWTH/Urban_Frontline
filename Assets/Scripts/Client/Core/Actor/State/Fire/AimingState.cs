// ========================================
// File: AimingState.cs
// Created: 2024-12-22 17:47:57
// Author: leeinhwan0421
// ========================================

using UniRx;
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
                                  .Where(_ => Player.WeaponController.PossibleADS == true)
                                  .Where(ads => ads == true)
                                  .Subscribe(_ =>
                                  {
                                      Player.SetAimState(Player.ADSState);
                                  }).AddTo(player);

            inputProvider.ReloadInput.Where(_ => IsEnable == true)
                                     .Where(_ => Player.WeaponController.PossibleReload == true)
                                     .Where(reload => reload == true)
                                     .Subscribe(_ =>
                                     {
                                         Player.SetAimState(Player.ReloadState);
                                     }).AddTo(player);
        }

        /// <summary>
        /// AimingState 상태에 돌입하였을 때 실행 되는 초기화 함수
        /// </summary>
        public override void Enter()
        {
            base.Enter();

            Player.CameraController.SetWeight(Player.WeaponController.FireFovWeight);
            Player.AnimatorController.SetLayerWeight(1.0f, "Upper Layer");
        }

        /// <summary>
        /// AimingState 상태에서 빠져 나갔을 때 실행 되는 초기화 함수
        /// </summary>
        public override void Exit()
        {
            base.Exit();
        }

        /// <summary>
        /// AimingState 상태일 때 매 프레임 호출되는 함수
        /// </summary>
        public override void Update()
        {
            base.Update();

            if (Player.WeaponController.ShouldReload)
            {
                if (Player.AnimatorController.IsEndState(Player.WeaponController.AttackStateName, "Upper Layer"))
                {
                    Player.SetAimState(Player.ReloadState);
                }
            }


            if (Player.WeaponController.PossibleShot)
            {
                if (InputManager.GetKey(KeyAction.Fire))
                {
                    Player.WeaponController.Shot();
                    Player.AnimatorController.Play(Player.WeaponController.AttackStateName, "Upper Layer");
                }
                else
                {
                    Player.SetAimState(Player.UnaimedState);
                }
            }
        }
    }
}
