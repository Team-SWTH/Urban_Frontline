// ========================================
// File: PlayerHealth.cs
// Created: 2025-01-03 13:18:44
// Author: ※ 작성자 이름을 반드시 기입해주세요.
// ========================================

using UnityEngine;

namespace UrbanFrontline.Client.Core.Actor.Status
{
    /// <summary>
    /// 플레이어의 체력을 총괄하는 컴포넌트
    /// </summary>
    public class PlayerHealth : MonoBehaviour
    {
        [Header("Status")]

        #region Status

        /// <summary>
        /// 플레이어의 체력
        /// </summary>
        [Tooltip("플레이어의 체력")]
        [SerializeField]
        private float m_health;

        /// <summary>
        /// 플레이어의 최대 체력
        /// </summary>
        [Tooltip("플레이어의 최대 체력")]
        [SerializeField]
        private float m_maxHealth;

        #endregion

        private void Start()
        {
            m_health += m_maxHealth;    
        }

        private void DrainHealth(float drainValue)
        {
            m_health -= drainValue;

            if (m_health <= 0)
            {
                m_health = 0;
            }
        }

        private void RegenHealth(float regenValue)
        {
            m_health += regenValue;

            if (m_health > m_maxHealth)
            {
                m_health = m_maxHealth;
            }
        }
    }
}
