// ========================================
// File: IniParser.cs
// Created: 2024-12-19 19:19:46
// Author: LHBM04
// ========================================

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace UrbanFrontline.Common
{
    /// <summary>
    /// .ini 파일 I/O 전용 파싱 헬퍼 클래스.
    /// </summary>
    public static class IniParser
    {
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(
            string section, string key, string defaultValue,
            StringBuilder returnValue, int size, string filePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern long WritePrivateProfileString(
            string section, string key, string value, string filePath);

        /// <summary>
        /// .ini 파일을 읽어서 IniData 객체로 반환합니다.
        /// </summary>
        public static IniData Load(string filePath)
        {
            IniData iniData = new IniData();

            // 섹션 목록 가져오기
            StringBuilder sectionBuffer = new StringBuilder(1024);
            GetPrivateProfileString(null, null, null, sectionBuffer, sectionBuffer.Capacity, filePath);

            var sections = sectionBuffer.ToString().Split(new[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var section in sections)
            {
                StringBuilder keyBuffer = new StringBuilder(1024);
                GetPrivateProfileString(section, null, null, keyBuffer, keyBuffer.Capacity, filePath);

                string[] keys = keyBuffer.ToString().Split(new[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var key in keys)
                {
                    var valueBuffer = new StringBuilder(255);
                    GetPrivateProfileString(section, key, null, valueBuffer, valueBuffer.Capacity, filePath);

                    iniData.SetValue(section, key, valueBuffer.ToString());
                }
            }

            return iniData;
        }

        /// <summary>
        /// IniData 객체를 .ini 파일로 저장합니다.
        /// </summary>
        public static void Save(string filePath, IniData iniData)
        {
            foreach (string section in iniData.GetSections())
            {
                Dictionary<string, string> keys = iniData.GetSection(section);
                if (keys != null)
                {
                    foreach (var keyValue in keys)
                    {
                        WritePrivateProfileString(section, keyValue.Key, keyValue.Value, filePath);
                    }
                }
            }
        }
    }
}

