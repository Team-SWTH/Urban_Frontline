// ========================================
// File: PlayerController.cs
// Created: 2024-12-20 12:47:23
// Author: leeinhwan0421
// ========================================

using UnityEngine;

using UniRx;

using UrbanFrontline.Client.Core.Input;
using UrbanFrontline.Client.Core.Actor.Animation;
using UrbanFrontline.Client.Core.Actor.State;
using UrbanFrontline.Client.Core.Actor.Movement;

namespace UrbanFrontline.Client.Core.Actor
{
    /// <summary>
    /// 플레이어를 전체적으로 관리하는 클래스
    /// </summary>
    [RequireComponent(typeof(InputProvider))]
    [RequireComponent(typeof(PlayerAnimator))]
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerController : MonoBehaviour
    {
        /// <summary>
        /// 애니메이터를 관리하는 인터페이스
        /// </summary>
        public IAnimatorController AnimationController;

        /// <summary>
        /// 움직임을 관리하는 인터페이스
        /// </summary>
        public ICharacterMovement CharacterMovement;

        /// <summary>
        /// 입력 이벤트를 발행하는 인터페이스
        /// </summary>
        public IInputProvider InputProvider;

        /// <summary>
        /// 가만히 있을 경우 State
        /// </summary>
        public IdleState IdleState { get; private set; }

        /// <summary>
        /// 움직일 경우 State
        /// </summary>
        public WalkState WalkState { get; private set; }

        /// <summary>
        /// 플레이어가 가지고 있는 StateMachaine
        /// </summary>
        private PlayerStateMachine m_stateMachine;

        private void Awake()
        {
            AnimationController = GetComponent<IAnimatorController>();
            CharacterMovement = GetComponent<ICharacterMovement>();
            InputProvider = GetComponent<IInputProvider>();

            m_stateMachine = new PlayerStateMachine();

            IdleState = new IdleState(this, InputProvider);
            WalkState = new WalkState(this, InputProvider);

            m_stateMachine.SetInitialState(IdleState);

            Observable.EveryUpdate()
                      .Subscribe(_ =>
                      {
                          m_stateMachine.Update();
                      }).AddTo(this);
        }

        /// <summary>
        /// 상태를 전환하는 코드
        /// </summary>
        /// <param name="state">상태를 전환합니다.</param>
        public void SetState(IPlayerState state)
        {
            if (state != null)
            {
                m_stateMachine.ChangeState(state);
            }
        }
    }
}
