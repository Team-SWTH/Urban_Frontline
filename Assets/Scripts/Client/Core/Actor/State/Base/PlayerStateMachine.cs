// ========================================
// File: PlayerStateMachine.cs
// Created: 2024-12-20 14:31:18
// Author: leeinhwan0421
// ========================================

namespace UrbanFrontline.Client.Core.Actor.State.Base
{
    /// <summary>
    /// 플레이어의 상태 기계
    /// </summary>
    public class PlayerStateMachine
    {
        /// <summary>
        /// 현재 상태의 인터페이스
        /// </summary>
        public IPlayerState CurrentState { get; private set; }

        /// <summary>
        /// 상태 초기화 함수
        /// </summary>
        /// <param name="initialState">초기화할 상태</param>
        public void SetInitialState(IPlayerState initialState)
        {
            CurrentState = initialState;
            CurrentState.Enter();
        }

        /// <summary>
        /// 상태 변경 함수
        /// </summary>
        /// <param name="newState">변경할 상태</param>
        public void ChangeState(IPlayerState newState)
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}
