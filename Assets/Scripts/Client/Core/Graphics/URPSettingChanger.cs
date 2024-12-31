// ========================================
// File: URPSettingChanger.cs
// Created: 2024-12-31 13:06:12
// Author: leeinhwan0421
// ========================================

using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Reflection;

namespace UrbanFrontline.Client.Core.Graphics
{
    /// <summary>
    /// 씬이 시작 될 때, 씬에 알맞는 그래픽 세팅으로 변환시켜주는 컴포넌트
    /// </summary>
    public class URPSettingChanger : MonoBehaviour
    {
        /// <summary>
        /// 씬에서 사용할 UniversalRenderPipelineAsset
        /// </summary>
        [Tooltip("씬에서 사용할 UniversalRenderPipelineAsset")]
        public UniversalRenderPipelineAsset URPAsset;

        void Start()
        {
            ChangeURPSettings();
        }

        void ChangeURPSettings()
        {
            if (URPAsset != null)
            {
                QualitySettings.renderPipeline = URPAsset;
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogError("URP Asset is missing.");
#endif
            }
        }
    }
}
