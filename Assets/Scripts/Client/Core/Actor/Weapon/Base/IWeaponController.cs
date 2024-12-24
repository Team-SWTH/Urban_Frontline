// ========================================
// File: IWeaponController.cs
// Created: 2024-12-24 12:51:13
// Author: leeinhwan0421
// ========================================

namespace UrbanFrontline.Client.Core.Actor.Weapon.Base
{
    public interface IWeaponController
    {
        #region Fov
        /// <summary>
        /// 사격 시 Fov 가중치
        /// </summary>
        float FireFovWeight { get; }

        /// <summary>
        /// 조준 시 Fov 가중치
        /// </summary>
        float ADSFovWeight { get; }
        #endregion

        #region Motions
        /// <summary>
        /// 재장전 모션 이름
        /// </summary>
        string ReloadStateName { get; }

        /// <summary>
        /// 공격 모션 이름
        /// </summary>
        string AttackStateName { get; }

        /// <summary>
        /// 정조준 모션 이름
        /// </summary>
        string ADSStateName { get; }
        #endregion

        #region ADS
        /// <summary>
        /// 조준 가능한지에 대한 여부
        /// </summary>
        bool PossibleADS { get; }
        #endregion

        #region Reload
        /// <summary>
        /// 장전 가능한지
        /// </summary>
        bool PossibleReload { get; }

        /// <summary>
        /// 현재 재장전이 무조건 필요하다면 (현재 총알이 0발인 경우) true를 반환하는 property
        /// </summary>
        bool ShouldReload { get; }
        #endregion

        #region Fire
        /// <summary>
        /// 사격할 수 있는지
        /// </summary>
        bool PossibleShot { get; }
        #endregion


        /// <summary>
        /// 발사
        /// </summary>
        void Shot();

        /// <summary>
        /// 재장전
        /// </summary>
        void Reload();
    }
}
