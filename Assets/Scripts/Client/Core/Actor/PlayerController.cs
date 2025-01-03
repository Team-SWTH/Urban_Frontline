// ========================================
// File: PlayerController.cs
// Created: 2024-12-20 12:47:23
// Author: leeinhwan0421
// ========================================

using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

using UrbanFrontline.Client.Core.Input;

using UrbanFrontline.Client.Core.Actor.Animation;
using UrbanFrontline.Client.Core.Actor.Camera;
using UrbanFrontline.Client.Core.Actor.Movement;
using UrbanFrontline.Client.Core.Actor.State.Base;
using UrbanFrontline.Client.Core.Actor.State.Fire;
using UrbanFrontline.Client.Core.Actor.State.Move;
using UrbanFrontline.Client.Core.Actor.Status;
using UrbanFrontline.Client.Core.Actor.Weapon.Base;

namespace UrbanFrontline.Client.Core.Actor
{
    /// <summary>
    /// 플레이어를 전체적으로 관리하는 클래스
    /// </summary>
    [RequireComponent(typeof(InputProvider))]
    [RequireComponent(typeof(PlayerAnimator))]
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerCameraController))]
    [RequireComponent(typeof(PlayerStamina))]
    [RequireComponent(typeof(PlayerHealth))]
    public class PlayerController : MonoBehaviour
    {
        #region Interfaces
        /// <summary>
        /// 입력 이벤트를 발행하는 인터페이스
        /// </summary>
        public IInputProvider InputProvider;

        /// <summary>
        /// 애니메이터를 관리하는 인터페이스
        /// </summary>
        public IAnimatorController AnimatorController;

        /// <summary>
        /// 움직임을 관리하는 인터페이스
        /// </summary>
        public ICharacterMovement CharacterMovement;

        /// <summary>
        /// 카메라와의 연동을 관리하는 인터페이스
        /// </summary>
        public ICameraController CameraController;

        /// <summary>
        /// 무기와의 연동을 관리하는 인터페이스
        /// </summary>
        public IWeaponController WeaponController;
        #endregion

        #region Move State Classes
        /// <summary>
        /// 가만히 있을 경우 State
        /// </summary>
        public IdleState IdleState { get; private set; }

        /// <summary>
        /// 움직일 경우 State
        /// </summary>
        public WalkState WalkState { get; private set; }

        /// <summary>
        /// 달릴 경우 State
        /// </summary>
        public RunState RunState { get; private set; }

        /// <summary>
        /// 구를 경우 State
        /// </summary>
        public RollState RollState { get; private set; }

        /// <summary>
        /// 점프할 경우 State
        /// </summary>
        public JumpState JumpState { get; private set; }
        #endregion

        #region Fire State Classes
        /// <summary>
        /// 비조준 State
        /// </summary>
        public UnaimedState UnaimedState { get; private set; }
        
        /// <summary>
        /// 지향 사격 State
        /// </summary>
        public AimingState AimingState { get; private set; }

        /// <summary>
        /// 조준 State
        /// </summary>
        public ADSState ADSState { get; private set; }

        /// <summary>
        /// 장전 State
        /// </summary>
        public ReloadState ReloadState { get; private set; }
        #endregion

        #region Status Classes
        public PlayerStamina PlayerStamina { get; private set; }
        #endregion

        /// <summary>
        /// 플레이어가 가지고 있는 Move State machine
        /// </summary>
        private PlayerStateMachine m_moveStateMachine;

        /// <summary>
        /// 플레이어가 가지고 있는 조준 State machine
        /// </summary>
        private PlayerStateMachine m_aimStateMachine;

        private void Awake()
        {
            InputProvider = GetComponent<IInputProvider>();
            AnimatorController = GetComponent<IAnimatorController>();
            CharacterMovement = GetComponent<ICharacterMovement>();
            CameraController = GetComponent<ICameraController>();
            WeaponController = GetComponentInChildren<IWeaponController>();

            PlayerStamina = GetComponent<PlayerStamina>();

            m_moveStateMachine = new PlayerStateMachine();
            m_aimStateMachine = new PlayerStateMachine();

            IdleState = new IdleState(this, InputProvider);
            WalkState = new WalkState(this, InputProvider);
            RunState = new RunState(this, InputProvider);
            RollState = new RollState(this);
            JumpState = new JumpState(this, InputProvider);

            UnaimedState = new UnaimedState(this, InputProvider);
            AimingState = new AimingState(this, InputProvider);
            ADSState = new ADSState(this, InputProvider);
            ReloadState = new ReloadState(this);

            m_moveStateMachine.SetInitialState(IdleState);
            m_aimStateMachine.SetInitialState(UnaimedState);

            InputProvider.FreeLookInput.Subscribe(enabled => 
                                        { 
                                            CameraController.SetFreeLookStatement(enabled); 
                                        }).AddTo(this);

            // 임시 코드 
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            m_moveStateMachine.Update();
            m_aimStateMachine.Update();
        }

        /// <summary>
        /// 움직임 상태를 전환하는 코드
        /// </summary>
        /// <param name="state">전환할 상태</param>
        public void SetMoveState(IPlayerState state)
        {
            m_moveStateMachine.ChangeState(state);
        }

        /// <summary>
        /// 조준 상태를 전환하는 코드
        /// </summary>
        /// <param name="state">전환할 상태</param>
        public void SetAimState(IPlayerState state)
        {
            m_aimStateMachine.ChangeState(state);
        }
    }
}
