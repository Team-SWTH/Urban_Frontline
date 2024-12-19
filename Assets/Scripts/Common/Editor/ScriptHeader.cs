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
    public readonly struct ScriptHeader
    {
        /// <summary>
        /// 파일명.
        /// </summary>
        public readonly string fileName;

        /// <summary>
        /// 파일 작성 시간.
        /// </summary>
        public readonly DateTime createdTime;

        public ScriptHeader(string filePath)
        {
            fileName = Path.GetFileName(filePath);
            createdTime = DateTime.Now;
        }

        public override string ToString()
        {
            return $@"// ========================================
// File: {fileName}
// Created: {createdTime:yyyy-MM-dd HH:mm:ss}
// Author: ※ 작성자 이름을 반드시 기입해주세요.
// ========================================

";
        }
    }

}
