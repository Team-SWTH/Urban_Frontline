// ========================================
// File: ITimer.cs
// Created: 2024-12-20 04:17:51
// Author: LHBM04
// ========================================

using System;
using UnityEngine;

namespace UrbanFrontline.Server.Level
{
    /// <summary>
    /// 게임 내 타이머가 구현해야 할 메서드의 시그니처가 선언됩니다.
    /// </summary>
    public interface ITimer
    {
        /// <summary>
        /// 타이머를 업데이트합니다.
        /// </summary>
        /// <param name="tick">현재 시간과 이전 시간의 차.</param>
        void Update(double tick);
    }
}