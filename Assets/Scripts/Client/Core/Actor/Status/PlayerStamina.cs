// ========================================
// File: PlayerStamina.cs
// Created: 2025-01-03 13:17:04
// Author: leeinhwan0421
// ========================================

using UnityEngine;
using UnityEngine.UI;

namespace UrbanFrontline.Client.Core.Actor.Status
{
    /// <summary>
    /// 플레이어의 스테미나를 총괄하는 컴포넌트
    /// </summary>
    public class PlayerStamina : MonoBehaviour
    {
        [Header("Status")]

        #region Status
        /// <summary>
        /// 스테미나
        /// </summary>
        [Tooltip("스테미나")]
        [SerializeField]
        private float m_stamina;

        /// <summary>
        /// 최대 스테미나
        /// </summary>
        [Tooltip("최대 스테미나")]
        [SerializeField]
        private float m_maxStamina;
        #endregion

        [Space(10.0f)]
        [Header("Offsets")]

        #region Offsets
        /// <summary>
        /// 달릴 때 초당 소모되는 스테미나
        /// </summary>
        [Tooltip("달릴 때 초당 소모되는 스테미나")]
        [SerializeField]
        private float m_runStaminaDrainPerSecond;

        /// <summary>
        /// 점프할 때 순간적으로 소모되는 스테미나
        /// </summary>
        [Tooltip("점프할 때 순간적으로 소모되는 스테미나")]
        [SerializeField]
        private float m_jumpStaminaDrainPerFrame;

        /// <summary>
        /// 구를 때 순간적으로 소모되는 스테미나
        /// </summary>
        [Tooltip("구를 때 순간적으로 소모되는 스테미나")]
        [SerializeField]
        private float m_rollStaminaDrainPerFrame;

        /// <summary>
        /// 걷거나 공중에 있을 때 초당 회복되는 스테미나
        /// </summary>
        [Tooltip("걷거나 공중에 있을 떄 초당 회복되는 스테미나")]
        [SerializeField]
        private float m_walkStaminaRegenPerSecond;

        /// <summary>
        /// 가만히 있을 때 초당 회복되는 스테미나
        /// </summary>
        [Tooltip("가만히 있을 때 초당 회복되는 스테미나")]
        [SerializeField]
        private float m_idleStaminaRegenPerSecond;
        #endregion

        #region Properties
        public float RunStaminaDrainPerSecond => m_runStaminaDrainPerSecond;
        public float JumpStaminaDrainPerFrame => m_jumpStaminaDrainPerFrame;
        public float RollStaminaDrainPerFrame => m_rollStaminaDrainPerFrame;
        public float WalkStaminaRegenPerSecond => m_walkStaminaRegenPerSecond;
        public float IdleStaminaRegenPerSecond => m_idleStaminaRegenPerSecond;


        /// <summary>
        /// 점프가 가능한지 true, false 반환
        /// </summary>
        public bool Jumpable
        {
            get
            {
                return m_stamina - m_jumpStaminaDrainPerFrame >= 0.0f;
            }
        }

        /// <summary>
        /// 구를 수 있는지 true, false 반환
        /// </summary>
        public bool Rollable
        {
            get
            {
                return m_stamina - m_rollStaminaDrainPerFrame >= 0.0f;
            }
        }

        /// <summary>
        /// 달릴 수 있는지 true, false 반환
        /// </summary>
        public bool Runable
        {
            get
            {
                return m_stamina - m_runStaminaDrainPerSecond * Time.deltaTime >= 0.0f;
            }
        }
        #endregion

        [Space(10.0f)]
        [Header("HUD")]

        #region HUD
        /// <summary>
        /// 스테미나 Filled 이미지
        /// </summary>
        [Tooltip("스테미나 Filled 이미지")]
        [SerializeField]
        private Image m_staminaHUDImage;
        #endregion

        private void Start()
        {
            m_stamina = m_maxStamina;
            m_staminaHUDImage.fillAmount = 1.0f;
        }

        /// <summary>
        /// 스테미나 소모 메서드
        /// </summary>
        /// <param name="drainValue">소모될 값</param>
        public void DrainStamina(float drainValue)
        {
            m_stamina -= drainValue;

            if (m_stamina < 0.0f)
            {
                m_stamina = 0.0f;
            }

            m_staminaHUDImage.fillAmount = m_stamina / m_maxStamina;
        }

        /// <summary>
        /// 스테미나 리젠 메서드
        /// </summary>
        /// <param name="regenValue">리젠 될 값</param>
        public void RegenStamina(float regenValue)
        {
            m_stamina += regenValue;

            if (m_stamina > m_maxStamina)
            {
                m_stamina = m_maxStamina;
            }

            m_staminaHUDImage.fillAmount = m_stamina / m_maxStamina;
        }
    }
}
