// ========================================
// File: PlayerAnimator.cs
// Created: 2024-12-20 15:01:54
// Author: leeinhwan0421
// ========================================

using UnityEngine;
using UrbanFrontline.Client.Core.Actor.Animation;

namespace UrbanFrontline.Client.Core.Actor.Animation
{
    public class PlayerAnimator : MonoBehaviour, IAnimatorController
    {
        /// <summary>
        /// 플레이어의 애니메이터 (이 프로젝트에서는, Mesh Object의 Animator를 가져옵니다.)
        /// </summary>
        [Tooltip("플레이어의 애니메이터")]
        [SerializeField]
        private Animator m_animator;

        /// <summary>
        /// 전환 시, 부드러운 전환을 위한 변수
        /// </summary>
        [Tooltip("전환 시간")]
        [SerializeField]
        private float m_transitionDuration = 0.15f;

        /// <summary>
        /// 초기화 문
        /// </summary>
        private void Awake()
        {
            if (m_animator == null)
            {
                m_animator = GetComponentInChildren<Animator>();
            }
        }

        /// <summary>
        /// 특정 레이어의 특정 애니메이션 상태를 재생
        /// </summary>
        /// <param name="stateName">애니메이션 상태 이름</param>
        /// <param name="layerName">레이어 이름</param>
        public void Play(string stateName, string layerName)
        {
            int layerIndex = m_animator.GetLayerIndex(layerName);

            m_animator.CrossFade(stateName, m_transitionDuration, layerIndex);
        }

        /// <summary>
        /// 특정 레이어의 현재 애니메이션 상태가 stateName으로 지정한 상태와 일치하는지 판별
        /// </summary>
        /// <param name="stateName">애니메이션 상태 이름</param>
        /// <param name="layerName">레이어 이름</param>
        /// <returns>일치하면 True, 일치하지 않으면 False</returns>
        public bool IsInState(string stateName, string layerName)
        {
            int layerIndex = m_animator.GetLayerIndex(layerName);

            AnimatorStateInfo stateInfo = m_animator.GetCurrentAnimatorStateInfo(layerIndex);
            return stateInfo.IsName(stateName);
        }

        /// <summary>
        /// 현재 애니메이션 상태가 끝났는지 확인한다
        /// </summary>
        /// <param name="stateName">확인할 상태 이름</param>
        /// <param name="layerName">레이어 이름</param>
        /// <returns>해당 상태가 끝났으면 true, 아니면 false</returns>
        public bool IsEndState(string stateName, string layerName)
        {
            int layerIndex = m_animator.GetLayerIndex(layerName);

            AnimatorStateInfo stateInfo = m_animator.GetCurrentAnimatorStateInfo(layerIndex);
            return stateInfo.IsName(stateName) && stateInfo.normalizedTime >= 1.0f;
        }

        /// <summary>
        /// 레이어 가중치를 수정합니다.
        /// </summary>
        /// <param name="weight">가중치</param>
        /// <param name="layerName">레이어 이름</param>
        public void SetLayerWeight(float weight, string layerName)
        {
            int layerIndex = m_animator.GetLayerIndex(layerName);

            m_animator.SetLayerWeight(layerIndex, weight);
        }

        /// <summary>
        /// 플로트 조작
        /// </summary>
        /// <param name="paramName">파라미터 이름</param>
        /// <param name="value">목표 값</param>
        public void SetFloat(string paramName, float value)
        {
            m_animator.SetFloat(paramName, value);
        }

        /// <summary>
        /// 불리언 조작
        /// </summary>
        /// <param name="paramName">파라미터 이름</param>
        /// <param name="value">목표 값</param>
        public void SetBool(string paramName, bool value)
        {
            m_animator.SetBool(paramName, value);
        }

        /// <summary>
        /// 트리거 조작
        /// </summary>
        /// <param name="paramName">파라미터 이름</param>
        public void SetTrigger(string paramName)
        {
            m_animator.SetTrigger(paramName);
        }
    }
}
