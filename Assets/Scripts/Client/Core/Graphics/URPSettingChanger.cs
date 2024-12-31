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

        /// <summary>
        /// 씬에서 사용할 ScriptableRendererData
        /// </summary>
        [Tooltip("씬에서 사용할 ScriptableRendererData")]
        public ScriptableRendererData RendererData;

        void Start()
        {
            ChangeURPSettings();
        }

        void ChangeURPSettings()
        {
            if (URPAsset != null && RendererData != null)
            {
                if (GraphicsSettings.currentRenderPipeline is UniversalRenderPipelineAsset currentURPAsset)
                {
                    GraphicsSettings.defaultRenderPipeline = URPAsset;

                    FieldInfo rendererDataListField = typeof(UniversalRenderPipelineAsset).GetField("m_RendererDataList", BindingFlags.NonPublic | BindingFlags.Instance);
                    if (rendererDataListField != null)
                    {
                        ScriptableRendererData[] rendererDataList = rendererDataListField.GetValue(URPAsset) as ScriptableRendererData[];
                        if (rendererDataList != null && rendererDataList.Length > 0)
                        {
                            rendererDataList[0] = RendererData;
                            rendererDataListField.SetValue(URPAsset, rendererDataList);
                        }
#if UNITY_EDITOR
                        Debug.Log("URP settings have been updated.");
#endif
                    }
                    else
                    {
#if UNITY_EDITOR
                        Debug.LogError("Unable to find m_RendererDataList field.");
#endif
                    }
                }
                else
                {
#if UNITY_EDITOR
                    Debug.LogError("Current render pipeline is not URP.");
#endif
                }
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogError("URP Asset or Renderer Data is missing.");
#endif
            }
        }
    }
}
