// ========================================
// File: ScriptSnipetHelper.cs
// Created: 2024-12-19 18:00:00
// Author: leeinhwan0421
// ========================================

using System;
using System.IO;
using UnityEditor;

namespace UrbanFrontline.Common.Editor
{
    /// <summary>
    /// 스크립트 최상단에 정보(헤더)를 기록합니다.
    /// </summary>
    public class ScriptSnippetHelper : AssetPostprocessor
    {
        /// <summary>
        /// cs 파일의 확장자.
        /// </summary>
        private static readonly string m_csScriptFileSuffix = ".cs";

        /// <summary>
        /// js 파일의 확장자.
        /// </summary>
        /// <param name="path"></param>
        private static readonly string m_jsScriptFileSuffix = ".js";

        /// <summary>
        /// boo 파일의 확장자.
        /// </summary>
        /// <param name="path"></param>
        private static readonly string m_booScriptFileSuffix = ".boo";

        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            foreach (string path in importedAssets)
            {
                if (IsScriptFile(path))
                {
                    string scriptContent = File.ReadAllText(path);
                    scriptContent = scriptContent.Replace(ScriptSnippets.DateTimeSnippetName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    scriptContent = scriptContent.Replace(ScriptSnippets.AuthorSnippetName, Environment.UserName);
                    File.WriteAllText(path, scriptContent);
                }
            }
        }

        private static bool IsScriptFile(string path)
        {
            if (!path.EndsWith(m_csScriptFileSuffix) &&
                !path.EndsWith(m_jsScriptFileSuffix) &&
                !path.EndsWith(m_booScriptFileSuffix))
            {
                return false;
            }

            return true;
        }
    }
}