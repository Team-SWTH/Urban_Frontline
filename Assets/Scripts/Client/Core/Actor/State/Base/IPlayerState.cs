// ========================================
// File: IPlayerState.cs
// Created: 2024-12-20 12:38:58
// Author: leeinhwan0421
// ========================================

namespace UrbanFrontline.Client.Core.Actor.State.Base
{
    /// <summary>
    /// 각 상태가 구현해야 하는 공통 메서드 정의
    /// </summary>
    public interface IPlayerState
    {
        /// <summary>
        /// ~ 상태에 돌입하였을 때 실행 되는 초기화 함수
        /// </summary>
        void Enter();

        /// <summary>
        /// ~ 상태에서 빠져 나갔을 때 실행 되는 초기화 함수
        /// </summary>
        void Exit();

        /// <summary>
        /// ~ 상태일 때, 매 프레임마다 실행되는  함수
        /// </summary>
        void UpdateState();
    }
}
