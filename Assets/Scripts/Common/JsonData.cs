// ========================================
// File: JsonData.cs
// Created: 2024-12-19 19:22:17
// Author: LHBM04
// ========================================

using System;
using System.Collections.Generic;
using System.Text.Json;

namespace UrbanFrontline.Common
{
    /// <summary>
    /// .json 파일에 저장된 데이터.
    /// </summary>
    [Serializable]
    public class JsonData
    {
        /// <summary>
        /// 데이터가 담긴 딕셔너리.
        /// </summary>
        private Dictionary<string, JsonElement> m_data = new(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 키와 값을 설정하거나 가져옵니다.
        /// </summary>
        public JsonElement this[string key]
        {
            get { return GetValue<JsonElement>(key); }
            set { SetValue(key, value); }
        }

        /// <summary>
        /// 키와 값을 설정합니다.
        /// </summary>
        public void SetValue(string key, JsonElement value)
        {
            m_data[key] = value;
        }

        /// <summary>
        /// 키와 값을 설정합니다.
        /// </summary>
        public void SetValue<T>(string key, T value)
        {
            m_data[key] = JsonSerializer.SerializeToDocument(value).RootElement;
        }

        /// <summary>
        /// 키에 해당하는 값을 가져옵니다. 기본값을 제공할 수 있습니다.
        /// </summary>
        public T GetValue<T>(string key)
        {
            if (m_data.TryGetValue(key, out JsonElement value))
            {
                return value.Deserialize<T>();
            }

            return default;
        }

        /// <summary>
        /// 모든 키-값 쌍을 반환합니다.
        /// </summary>
        public Dictionary<string, JsonElement> GetAllData()
        {
            return m_data;
        }

        /// <summary>
        /// 특정 키를 삭제합니다.
        /// </summary>
        public void RemoveKey(string key)
        {
            if (m_data.ContainsKey(key))
            {
                m_data.Remove(key);
            }
        }

        /// <summary>
        /// JSON 데이터를 JSON 형식의 문자열로 변환합니다.
        /// </summary>
        public string ToJsonString()
        {
            return JsonSerializer.Serialize(m_data, new JsonSerializerOptions { WriteIndented = true });
        }
    }
}
