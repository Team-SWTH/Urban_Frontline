// ========================================
// File: ScriptHeaderInjector.cs
// Created: 2024-12-19 18:00:00
// Author: leeinhwan0421
// ========================================

using System.IO;

using UnityEditor;

namespace UrbanFrontline.Common.Editor
{
    /// <summary>
    /// 스크립트 최상단에 정보(헤더)를 기록합니다.
    /// </summary>
    public class ScriptHeaderInjector : AssetModificationProcessor
    {
        /// <summary>
        /// cs.meta 파일의 확장자.
        /// </summary>
        private static readonly string m_csMetaFileSuffix = ".cs.meta";
        
        /// <summary>
        /// cs 파일의 확장자.
        /// </summary>
        private static readonly string m_csScriptFileSuffix = ".cs";
        
        public static void OnWillCreateAsset(string path)
        {
            if (path.EndsWith(m_csMetaFileSuffix) || !path.EndsWith(m_csScriptFileSuffix))
            {
                return;
            }

            EditorApplication.delayCall += () => InsertHeaderToScript( Path.GetFullPath(path));
        }
        
        /// <summary>
        /// 스크립트 최상단에 정보(헤더)를 기록합니다.
        /// </summary>
        /// <param name="filePath"></param>
        private static void InsertHeaderToScript(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return;
            }

            string originalContent = File.ReadAllText(filePath);
            if (string.IsNullOrEmpty(originalContent))
            {
                return;
            }
            
            string updatedContent = new ScriptHeader(filePath) + originalContent;
            File.WriteAllText(filePath, updatedContent);
        }
    }
}