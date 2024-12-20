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
        /// 플레이어 이동 입력을 (수평/수직)을 Vector2로 발행하는 Reactive Property
        /// </summary>
        private Subject<Vector2> m_moveInput = new Subject<Vector2>();
        public IObservable<Vector2> MoveInput => m_moveInput;

        /// <summary>
        /// 점프 입력 이벤트 스트림
        /// </summary>
        private Subject<Unit> m_jumpInput = new Subject<Unit>();
        public IObservable<Unit> JumpInput => m_jumpInput;

        /// <summary>
        /// 구르기 입력 이벤트 스트림
        /// </summary>
        private Subject<Unit> m_RollInput = new Subject<Unit>();
        public IObservable<Unit> RollInput => m_RollInput;

        #endregion

        private void Start()
        {
            // Update 문을 사용하는 것 보다 Observalbe을 사용하는 것이 성능이 더 좋다고 알려져 있음.
            // 만약 이게 아니라면, 수정할 의향이 있습니다.
            Observable.EveryUpdate()
                      .Subscribe(_ => 
                      { 
                          UpdateMoveInput(); 
                          UpdateJumpInput(); 
                          UpdateRollInput(); 
                      }).AddTo(this);
        }

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
        /// 점프 입력을 갱신하는 코드
        /// </summary>
        private void UpdateJumpInput()
        {
            if (InputManager.GetKey(KeyAction.Jump))
            {
                m_jumpInput.OnNext(Unit.Default);
            }
        }

        /// <summary>
        /// 구르기 입력을 갱신하는 코드
        /// </summary>
        private void UpdateRollInput()
        {
            if (InputManager.GetKey(KeyAction.Roll))
            {
                m_RollInput.OnNext(Unit.Default);
            }
        }
    }
}
