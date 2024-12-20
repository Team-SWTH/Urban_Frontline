// ========================================
// File: PlayerAnimator.cs
// Created: 2024-12-20 15:01:54
// Author: leeinhwan0421
// ========================================

using UnityEngine;
using UrbanFrontline.Client.Core.Actor.Animation;

public class PlayerAnimator : MonoBehaviour, IAnimatorController
{
    /// <summary>
    /// 플레이어의 애니메이터 (이 프로젝트에서는, Mesh Object의 Animator를 가져옵니다.)
    /// </summary>
    [SerializeField]
    private Animator m_animator;

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
    /// <param name="animationName">애니메이션 상태 이름</param>
    /// <param name="layerIndex">레이어 번호</param>
    public void Play(string animationName, int layerIndex)
    {
        m_animator.Play(animationName, layerIndex, 0f);
    }

    /// <summary>
    /// 특정 레이어의 현재 애니메이션 상태가 stateName으로 지정한 상태와 일치하는지 판별
    /// </summary>
    /// <param name="stateName">애니메이션 상태 이름</param>
    /// <param name="layerIndex">레이어 번호</param>
    /// <returns>일치하면 True, 일치하지 않으면 False</returns>
    public bool IsInState(string stateName, int layerIndex)
    {
        AnimatorStateInfo stateInfo = m_animator.GetCurrentAnimatorStateInfo(layerIndex);
        return stateInfo.IsName(stateName);
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
