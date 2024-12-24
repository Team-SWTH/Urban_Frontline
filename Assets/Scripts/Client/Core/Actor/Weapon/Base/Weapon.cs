// ========================================
// File: Weapon.cs
// Created: 2024-12-24 13:39:22
// Author: leeinhwan0421
// ========================================

using UnityEngine;

namespace UrbanFrontline.Client.Core.Actor.Weapon.Base
{
    public class Weapon : MonoBehaviour, IWeaponController
    {
        #region Fields
        [Header("ADS")]

        /// <summary>
        /// 정조준 가능 여부
        /// </summary>
        [Tooltip("정조준 가능 여부")]
        [SerializeField]
        private bool m_possibleADS = true;
        public bool PossibleADS => m_possibleADS;

        [Space(10.0f)]
        [Header("Reload")]

        /// <summary>
        /// 재장전 가능 여부
        /// </summary>
        [Tooltip("재장전 가능 여부")]
        [SerializeField]
        private bool m_possibleReload = true;

        public bool PossibleReload
        {
            get
            {
                if (m_possibleReload)
                {
                    return true;
                }
                else
                {
                    return !(m_currentAmmo == m_maxAmmo);
                }
            }
        }

        /// <summary>
        /// 재장전 중인지를 반환
        /// </summary>
        private bool m_isReloadInProgress = false;
        public bool IsReloadInProgress => m_isReloadInProgress;

        [Space(10.0f)]
        [Header("Ammo")]

        /// <summary>
        /// 현재 총알 갯수
        /// </summary>
        [Tooltip("현재 총알 갯수")]
        [SerializeField]
        private int m_currentAmmo = 0;

        /// <summary>
        /// 장전 시 채워 줄 총알 갯수
        /// </summary>
        [Tooltip("장전 시 채워 줄 총알 갯수")]
        [SerializeField]
        private int m_maxAmmo = 0;

        #endregion

        /// <summary>
        /// 발사 로직
        /// </summary>
        public void Shot()
        {

        }

        /// <summary>
        /// 재장전 로직
        /// </summary>
        public void Reload()
        {

        }

        /// <summary>
        /// 장착 로직
        /// 저는 장전 캔슬이 불가능 하도록 만들었습니다.
        /// </summary>
        public void Equip()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// 해제 로직
        /// 저는 장전 캔슬이 불가능하도록 만들었습니다.
        /// </summary>
        public void Unequip()
        {
            gameObject.SetActive(false);
        }
    }
}
