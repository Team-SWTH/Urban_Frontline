// ========================================
// File: IAnimationController.cs
// Created: 2024-12-20 14:58:41
// Author: leeinhwan0421
// ========================================

namespace UrbanFrontline.Client.Core.Actor.Animation
{
    /// <summary>
    /// 애니메이션을 제어하는 인터페이스
    /// </summary>
    public interface IAnimatorController
    {
        /// <summary>
        /// 해당 이름의 애니메이션(상태)을 재생한다.
        /// 일반적으로 Animator State Machine 상의 State 이름과 일치하도록 한다.
        /// </summary>
        /// <param name="animationName">재생할 애니메이션(상태) 이름</param>
        /// <param name="layerIndex">레이어 번호</param>
        void Play(string animationName, int layerIndex);

        /// <summary>
        /// 현재 애니메이션 상태가 특정 이름인지 확인한다.
        /// </summary>
        /// <param name="stateName">확인할 상태 이름</param>
        /// <param name="layerIndex">레이어 번호</param>
        /// <returns>해당 상태이면 true, 아니면 false</returns>
        bool IsInState(string stateName, int layerIndex);

        /// <summary>
        /// float 파라미터 세팅
        /// </summary>
        void SetFloat(string paramName, float value);

        /// <summary>
        /// bool 파라미터 세팅
        /// </summary>
        void SetBool(string paramName, bool value);

        /// <summary>
        /// trigger 파라미터 설정
        /// </summary>
        void SetTrigger(string paramName);
    }

}
