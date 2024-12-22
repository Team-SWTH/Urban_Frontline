using Cysharp.Threading.Tasks;
using System;
using UniRx;
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

            Player.AnimatorController.Play("Jump", 0);
            Player.AnimatorController.SetLayerWeight(1.0f, 1);

            Player.CharacterMovement.Jump();
        }

        /// <summary>
        /// Jump 상태에서 빠져나갔을 때 실행되는 초기화 함수
        /// </summary>
        public override void Exit()
        {
            base.Exit();

            Player.AnimatorController.Play("Land", 0);
        }

        /// <summary>
        /// Jump 상태일 때, 매 프레임마다 실행되는 함수
        /// </summary>
        public override void UpdateState()
        {
            base.UpdateState();

            if (Player.CharacterMovement.IsGrounded())
            {
                Player.SetMoveState(Player.IdleState);
            }
        }
    }
}
