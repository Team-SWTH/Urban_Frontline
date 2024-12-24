// ========================================
// File: Weapon.cs
// Created: 2024-12-24 13:39:22
// Author: leeinhwan0421
// ========================================

using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace UrbanFrontline.Client.Core.Actor.Weapon.Base
{
    public class Weapon : MonoBehaviour, IWeaponController
    {
        #region Fields
        [Header("Motion")]

        /// <summary>
        /// 공격 시 재생되어야 할 애니메이터 상태 이름
        /// </summary>
        [Tooltip("공격 시 재생되어야 할 애니메이터 상태 이름")]
        [SerializeField]
        private string m_attackStateName;
        public string AttackStateName => m_attackStateName;

        /// <summary>
        /// 재장전 시 재생되어야 할 애니메이터 상태 이름
        /// </summary>
        [Tooltip("재장전 시 재생되어야 할 애니메이터 상태 이름")]
        [SerializeField]
        private string m_reloadStateName;
        public string ReloadStateName => m_reloadStateName;

        /// <summary>
        /// 정조준 시 재생되어야 할 애니메이터 상태 이름
        /// </summary>
        [Tooltip("정조준 시 재생되어야 할 애니메이터 상태 이름")]
        [SerializeField]
        private string m_adsStateName;
        public string ADSStateName => m_adsStateName;

        [Space(10.0f)]
        [Header("Field of View")]
        [Range(0.01f, 1.0f)]
        [SerializeField]
        private float m_fireFovWeight = 1.0f;
        public float FireFovWeight => m_fireFovWeight;

        [Range(0.01f, 1.0f)]
        [SerializeField]
        private float m_adsFovWeight = 1.0f;
        public float ADSFovWeight => m_adsFovWeight;

        [Space(10.0f)]
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
                if (m_currentAmmo == m_maxAmmo || !m_possibleReload || m_isReloadInProgress)
                {
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        /// 재장전 중인지를 반환
        /// </summary>
        private bool m_isReloadInProgress = false;

        [Space(10.0f)]
        [Header("Ammo")]

        /// <summary>
        /// 현재 총알 갯수
        /// </summary>
        [Tooltip("현재 총알 갯수")]
        [SerializeField]
        private int m_currentAmmo = 0;
        public int CurrentAmmo => m_currentAmmo;

        /// <summary>
        /// 장전 시 채워 줄 총알 갯수
        /// </summary>
        [Tooltip("장전 시 채워 줄 총알 갯수")]
        [SerializeField]
        private int m_maxAmmo = 0;

        public int MaxAmmo => m_maxAmmo;

        [Space(10.0f)]
        [Header("Shot")]

        /// <summary>
        /// 총을 쏠 수 있는지 여부
        /// </summary>
        [Tooltip("총을 쏠 수 있는지")]
        [SerializeField]
        private bool m_possibleShot = true;

        public bool PossibleShot
        {
            get
            {
                if (m_currentAmmo <= 0 || !m_possibleShot)
                {
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        /// 발당 간격
        /// </summary>
        [Tooltip("발당 간격")]
        [SerializeField]
        private float m_intervalPerShot = 1f;

        #endregion

        /// <summary>
        /// 발사 로직
        /// </summary>
        public void Shot()
        {
            m_possibleShot = false;
            m_currentAmmo--;

            StartShotCooldown().Forget();

            // TODO: 투사체 발사 및 사운드
        }

        /// <summary>
        /// 발사 쿨타임 메서드
        /// </summary>
        private async UniTaskVoid StartShotCooldown()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(m_intervalPerShot));
            m_possibleShot = true;
        }

        /// <summary>
        /// 재장전 로직
        /// </summary>
        public void Reload()
        {

        }
    }
}
