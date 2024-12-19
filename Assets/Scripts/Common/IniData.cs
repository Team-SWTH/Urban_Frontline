// ========================================
// File: IniData.cs
// Created: 2024-12-19 19:20:28
// Author: ※ 작성자 이름을 반드시 기입해주세요.
// ========================================

using System;
using System.Collections.Generic;

namespace UrbanFrontline.Common
{
    /// <summary>
    /// .ini 파일에 저장된 데이터.
    /// </summary>
    [Serializable]
    public class IniData
    {
        private Dictionary<string, Dictionary<string, string>> m_sections = new(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 섹션과 키를 기준으로 값을 설정하거나 가져옵니다.
        /// </summary>
        public string this[string section, string key]
        {
            get => GetValue(section, key);
            set => SetValue(section, key, value);
        }

        /// <summary>
        /// 섹션과 키를 기준으로 값을 설정합니다.
        /// </summary>
        public void SetValue(string section, string key, string value)
        {
            if (!m_sections.ContainsKey(section))
            {
                m_sections[section] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            }

            m_sections[section][key] = value;
        }

        /// <summary>
        /// 섹션과 키를 기준으로 값을 가져옵니다.
        /// </summary>
        public string GetValue(string section, string key, string defaultValue = null)
        {
            if (m_sections.ContainsKey(section) && m_sections[section].ContainsKey(key))
            {
                return m_sections[section][key];
            }

            return defaultValue;
        }

        /// <summary>
        /// 특정 섹션의 모든 키-값 쌍을 반환합니다.
        /// </summary>
        public Dictionary<string, string> GetSection(string section)
        {
            return m_sections.GetValueOrDefault(section);
        }

        /// <summary>
        /// 섹션 목록을 가져옵니다.
        /// </summary>
        public IEnumerable<string> GetSections()
        {
            return m_sections.Keys;
        }

        /// <summary>
        /// 특정 섹션을 삭제합니다.
        /// </summary>
        public void RemoveSection(string section)
        {
            if (m_sections.ContainsKey(section))
            {
                m_sections.Remove(section);
            }
        }

        /// <summary>
        /// 특정 섹션의 키를 삭제합니다.
        /// </summary>
        public void RemoveKey(string section, string key)
        {
            if (m_sections.ContainsKey(section) && m_sections[section].ContainsKey(key))
            {
                m_sections[section].Remove(key);

                // 키가 모두 삭제되었다면 섹션도 제거
                if (m_sections[section].Count == 0)
                {
                    m_sections.Remove(section);
                }
            }
        }
    }
}
