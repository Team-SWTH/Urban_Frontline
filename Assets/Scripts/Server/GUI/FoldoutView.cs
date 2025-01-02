// ========================================
// File: FoldoutView.cs
// Created: 2025-01-02 23:44:16
// Author: LHBM04
// ========================================

using UnityEngine;

namespace UrbanFrontline.Server.GUI
{
    /// <summary>
    /// 폴드 형식의 GUI의 기본 동작을 정의합니다.
    /// </summary>
    public abstract class FoldoutView : MonoBehaviour
    {
        /// <summary>
        /// 모니터를 엽니다.
        /// </summary>
        public abstract void Open();

        /// <summary>
        /// 모니터링 결과를 보여줍니다.
        /// </summary>
        public abstract void Show();

        /// <summary>
        /// 모니터를 닫습니다.
        /// </summary>
        public abstract void Close();
    }
}