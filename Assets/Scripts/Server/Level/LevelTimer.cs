// ========================================
// File: LevelTimer.cs
// Created: 2024-12-20 04:16:19
// Author: LHBM04
// ========================================

using System;
using UnityEngine;

namespace UrbanFrontline.Server.Level
{
    /// <summary>
    /// 스테이지 내 타이머.
    /// </summary>
    public class LevelTimer : ITimer
    {
        /// <summary>
        /// 현재까지 계산된 시간.
        /// </summary>
        private TimeSpan m_time;
        public TimeSpan Time => m_time;

        /// <summary>
        /// 타이머를 업데이트합니다.
        /// </summary>
        /// <param name="tick">현재 시간과 이전 시간의 차.</param>
        void ITimer.Update(double tick)
        {
            m_time = m_time.Add(TimeSpan.FromSeconds(tick));
        }
    }
}
