// ========================================
// File: InputProvider.cs
// Created: 2024-12-20 13:55:43
// Author: leeinhwan0421
// ========================================

using System;
using UniRx;
using UnityEngine;

namespace UrbanFrontline.Client.Core.Input
{
    public class InputProvider : MonoBehaviour, IInputProvider
    {
        #region Fields

        /// <summary>
        /// 이동 입력 이벤트 스트림
        /// </summary>
        private Subject<Vector2> m_moveInput = new Subject<Vector2>();
        public IObservable<Vector2> MoveInput => m_moveInput;

        /// <summary>
        /// 달리기 입력 이벤트 스트림
        /// </summary>
        private Subject<bool> m_runInput = new Subject<bool>();
        public IObservable<bool> RunInput => m_runInput;

        /// <summary>
        /// 점프 입력 이벤트 스트림
        /// </summary>
        private Subject<bool> m_jumpInput = new Subject<bool>();
        public IObservable<bool> JumpInput => m_jumpInput;

        /// <summary>
        /// 구르기 입력 이벤트 스트림
        /// </summary>
        private Subject<bool> m_rollInput = new Subject<bool>();
        public IObservable<bool> RollInput => m_rollInput;

        /// <summary>
        /// 자유시점 입력 이벤트 스트림
        /// </summary>
        private ReactiveProperty<bool> m_freeLookInput = new ReactiveProperty<bool>();
        public IReactiveProperty<bool> FreeLookInput => m_freeLookInput;

        #endregion

        private void Start()
        {
            Observable.EveryUpdate()
                      .Subscribe(_ => 
                      { 
                          UpdateMoveInput();
                          UpdateRunInput();
                          UpdateJumpInput(); 
                          UpdateRollInput();
                          UpdateFreeLookInput();
                      }).AddTo(this);
        }

        #region Update Methods
        /// <summary>
        /// 이동 입력을 갱신하는 코드
        /// </summary>
        private void UpdateMoveInput()
        {
            float horizontal = 0f;
            float vertical = 0f;

            if (InputManager.GetKey(KeyAction.Forward))
            {
                vertical += 1f;
            }
            if (InputManager.GetKey(KeyAction.Backward))
            {
                vertical -= 1f;
            }
            if (InputManager.GetKey(KeyAction.Right))
            {
                horizontal += 1f;
            }
            if (InputManager.GetKey(KeyAction.Left))
            {
                horizontal -= 1f;
            }

            Vector2 move = new Vector2(horizontal, vertical).normalized;
            m_moveInput.OnNext(move);
        }

        /// <summary>
        /// 달리기 입력을 갱신하는 코드
        /// </summary>
        private void UpdateRunInput()
        {
            m_runInput.OnNext(InputManager.GetKey(KeyAction.Run));
        }

        /// <summary>
        /// 점프 입력을 갱신하는 코드
        /// </summary>
        private void UpdateJumpInput()
        {
            m_jumpInput.OnNext(InputManager.GetKey(KeyAction.Jump));
        }

        /// <summary>
        /// 구르기 입력을 갱신하는 코드
        /// </summary>
        private void UpdateRollInput()
        {
            m_rollInput.OnNext(InputManager.GetKey(KeyAction.Roll));
        }

        /// <summary>
        /// 자유 시점 입력을 갱신하는 코드
        /// </summary>
        private void UpdateFreeLookInput()
        {
            m_freeLookInput.Value = InputManager.GetKey(KeyAction.FreeLook);
        }
        #endregion
    }
}
