// ========================================
// File: TestMonitor.cs
// Created: 2025-01-02 22:55:59
// Author: LHBM04
// ========================================

using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace UrbanFrontline.Server.GUI
{
    /// <summary>
    /// 테스트 모니터.
    /// </summary>
    public class TestMonitor : FoldoutView
    {
        public RectTransform label;
        public RectTransform value;
        private bool m_isExpanded = false;

        private void Start()
        {
            label.gameObject.SetActive(true);
            value.gameObject.SetActive(false);

            Observable.EveryUpdate()
                .Where(_ => m_isExpanded)
                .Where(_ => !RectTransformUtility.RectangleContainsScreenPoint(value, Input.mousePosition, null))
                .Where(_ => Input.GetKeyDown(KeyCode.Mouse0))
                .Subscribe(_ => Close())
                .AddTo(this);

            Observable.EveryUpdate()
                .Where(_ => m_isExpanded)
                .Subscribe(_ => Show())
                .AddTo(this);
        }

        public override void Open()
        {
            m_isExpanded = true;
            label.gameObject.SetActive(false);
            value.gameObject.SetActive(true);
        }

        public override void Close()
        {
            m_isExpanded = false;
            label.gameObject.SetActive(true);
            value.gameObject.SetActive(false);
        }

        public override void Show()
        {
            Debug.Log("Hello, World!");
        }
    }
}