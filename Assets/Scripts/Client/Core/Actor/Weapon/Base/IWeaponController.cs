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
        /// 사격 시 Fov 가중치
        /// </summary>
        float FireFovWeight { get; }

        /// <summary>
        /// 조준 시 Fov 가중치
        /// </summary>
        float ADSFovWeight { get; }

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

        /// <summary>
        /// 조준 가능한지에 대한 여부
        /// </summary>
        bool PossibleADS { get; }

        /// <summary>
        /// 장전 가능한지
        /// </summary>
        bool PossibleReload { get; }

        /// <summary>
        /// 사격할 수 있는지
        /// </summary>
        bool PossibleShot { get; }

        /// <summary>
        /// 현재 총알
        /// </summary>
        int CurrentAmmo { get; }

        /// <summary>
        /// 최대 총알
        /// </summary>
        int MaxAmmo { get; }

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
