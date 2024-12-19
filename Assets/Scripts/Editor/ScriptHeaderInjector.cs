// ========================================
// File: ScriptHeaderInjector.cs
// Created: 2024-12-19 18:00:00
// Author: leeinhwan0421
// Project: Urban_Frontline
// ========================================

using System;
using System.IO;

using UnityEditor;

public class ScriptHeaderInjector : AssetModificationProcessor
{
    public static void OnWillCreateAsset(string path)
    {
        if (!path.EndsWith(".cs.meta") && path.EndsWith(".cs"))
        {
            string fullPath = Path.GetFullPath(path);

            EditorApplication.delayCall += () => AddHeaderToScript(fullPath);
        }
    }

    private static void AddHeaderToScript(string filePath)
    {
        if (!File.Exists(filePath))
            return;

        string originalContent = File.ReadAllText(filePath);
        if (string.IsNullOrEmpty(originalContent))
            return;

        string header = $@"// ========================================
// File: {Path.GetFileName(filePath)}
// Created: {DateTime.Now:yyyy-MM-dd HH:mm:ss}
// Author: 
// Project: Urban_Frontline
// ========================================

";

        string updatedContent = header + originalContent;

        File.WriteAllText(filePath, updatedContent);
    }
}
