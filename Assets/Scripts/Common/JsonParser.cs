// ========================================
// File: JsonParser.cs
// Created: 2024-12-19 19:23:29
// Author: ※ 작성자 이름을 반드시 기입해주세요.
// ========================================

using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace UrbanFrontline.Common
{
    /// <summary>
    /// .json 파일 I/O 전용 파싱 헬퍼 클래스.
    /// </summary>
    public static class JsonParser
    {
        /// <summary>
        /// JSON 문자열을 파싱하여 JsonData 객체로 반환합니다.
        /// </summary>
        public static JsonData FromJson(string json)
        {
            Dictionary<string, object> data = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            JsonData jsonData = new JsonData();

            foreach (KeyValuePair<string, object> entry in data)
            {
                jsonData.SetValue(entry.Key, entry.Value);
            }

            return jsonData;
        }

        /// <summary>
        /// JsonData 객체를 JSON 형식의 문자열로 변환합니다.
        /// </summary>
        public static string ToJson(JsonData jsonData)
        {
            Dictionary<string, object> data = jsonData.GetAllData();
            return JsonConvert.SerializeObject(data, Formatting.Indented);
        }

        /// <summary>
        /// JSON 파일을 읽어서 JsonData 객체로 반환합니다.
        /// </summary>
        public static JsonData Load(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return FromJson(json);
        }

        /// <summary>
        /// JsonData 객체를 JSON 파일로 저장합니다.
        /// </summary>
        public static void Save(string filePath, JsonData jsonData)
        {
            string jsonString = ToJson(jsonData);
            File.WriteAllText(filePath, jsonString);
        }
    }
}
