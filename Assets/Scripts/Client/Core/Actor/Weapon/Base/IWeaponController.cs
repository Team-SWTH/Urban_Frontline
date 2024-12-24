// ========================================
// File: IWeaponController.cs
// Created: 2024-12-24 12:51:13
// Author: leeinhwan0421
// ========================================

namespace UrbanFrontline.Client.Core.Actor.Weapon.Base
{
    public interface IWeaponController
    {
        /// <summary>
        /// 조준 가능한지에 대한 여부
        /// </summary>
        bool PossibleADS { get; }
        
        /// <summary>
        /// 장전 가능한지
        /// </summary>
        bool PossibleReload { get; }

        /// <summary>
        /// 장전 중인지
        /// </summary>
        bool IsReloadInProgress { get; }

        /// <summary>
        /// 발사
        /// </summary>
        void Shot();

        /// <summary>
        /// 재장전
        /// </summary>
        void Reload();

        /// <summary>
        /// 활성화
        /// </summary>
        void Equip();

        /// <summary>
        /// 비활성화
        /// </summary>
        void Unequip();
    }
}
