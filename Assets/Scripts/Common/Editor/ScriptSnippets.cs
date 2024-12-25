// ========================================
// File: ScriptHeader.cs
// Created: 2024-12-19 18:34:22
// Author: LHBM04
// ========================================

using System;
using System.IO;

namespace UrbanFrontline.Common.Editor
{
    /// <summary>
    /// 스크립트 이름, 작성 날짜 등을 저장합니다.
    /// </summary>
    public readonly struct ScriptSnippets
    {
        public static readonly string DateTimeSnippetName = "#DATETIME#";

        public static readonly string AuthorSnippetName = "#AUTHOR#";
    }
}
