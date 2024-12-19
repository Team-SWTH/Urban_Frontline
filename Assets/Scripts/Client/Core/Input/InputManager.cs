// ========================================
// File: InputManager.cs
// Created: 2024-12-19 19:56:32
// Author: leeinhwan0421
// ========================================

using System;
using System.IO;
using System.Collections.Generic;

using UnityEngine;

using UrbanFrontline.Common;

namespace UrbanFrontline.Client.Core.Input
{
    public static class InputManager
    {
        /// <summary>
        /// 바인딩 할 수 있는 키의 기본 값 배열
        /// </summary>
        private static readonly KeyCode[] DefaultKeyBind = {
            KeyCode.W,
            KeyCode.A,
            KeyCode.S,
            KeyCode.D,
            KeyCode.Space,
            KeyCode.LeftShift
        };

        /// <summary>
        /// 키 바인딩이 저장되어있는 파일 경로
        /// 기대값 경로 : C:\Users\{사용자 명}\AppData\LocalLow\Team SWTH\Urban_Frontline\keybindings.json
        /// </summary>
        private static readonly string KeyBindFilePath = Path.Combine(Application.persistentDataPath, "keybindings.json");

        /// <summary>
        /// 실제 바인딩 딕셔너리
        /// </summary>
        private static Dictionary<KeyAction, KeyCode> m_keyBindDictionary = new Dictionary<KeyAction, KeyCode>();

        static InputManager()
        {
            LoadKeyBindAll();
        }

        #region IO

        /// <summary>
        /// 키 바인딩을 불러옵니다.
        /// </summary>
        private static void LoadKeyBindAll()
        {
            if (File.Exists(KeyBindFilePath))
            {
                JsonData jsonData = JsonParser.Load(KeyBindFilePath);

                foreach (KeyAction action in System.Enum.GetValues(typeof(KeyAction)))
                {
                    string actionKey = action.ToString();
                    if (jsonData.GetValue<object>(actionKey) is string keyCodeString &&
                        System.Enum.TryParse(keyCodeString, out KeyCode keyCode))
                    {
                        m_keyBindDictionary[action] = keyCode;
                    }
                }
            }
            else
            {
                ResetKeyBindAll();
            }
        }

        /// <summary>
        /// 키 바인딩을 저장합니다.
        /// </summary>
        private static void SaveKeyBindAll()
        {
            JsonData jsonData = new JsonData();

            foreach (var binding in m_keyBindDictionary)
            {
                jsonData.SetValue(binding.Key.ToString(), binding.Value.ToString());
            }

            JsonParser.Save(KeyBindFilePath, jsonData);
        }

        /// <summary>
        /// 키 바인딩을 초기화 합니다.
        /// </summary>
        private static void ResetKeyBindAll()
        {
            m_keyBindDictionary.Clear();

            for (int i = 0; i < KeyBindFilePath.Length; i++)
            {
                m_keyBindDictionary[(KeyAction)i] = DefaultKeyBind[i];
            }

            SaveKeyBindAll();
        }

        /// <summary>
        /// 특정 키 바인딩을 변경합니다.
        /// </summary>
        /// <param name="keyAction">변경할 키 액션</param>
        /// <param name="keyCode">새로운 키 코드</param>
        public static void ChangeKeyBind(KeyAction keyAction, KeyCode keyCode)
        {
            if (m_keyBindDictionary.ContainsKey(keyAction))
            {
                m_keyBindDictionary[keyAction] = keyCode;
                SaveKeyBindAll();
            }
        }

        #endregion

        #region Key Detection
        /// <summary>
        /// 특정 키가 현재 눌려있는지를 확인합니다.
        /// </summary>
        /// <param name="keyAction">확인할 특정 키</param>
        /// <returns>키가 눌려 있으면 true, 그렇지 않으면 false</returns>
        public static bool GetKey(KeyAction keyAction)
        {
            return UnityEngine.Input.GetKey(m_keyBindDictionary[keyAction]);
        }

        /// <summary>
        /// 특정 키가 이번 프레임에 눌렸는지 확인합니다.
        /// </summary>
        /// <param name="keyAction">확인할 특정 키</param>
        /// <returns>키가 이번 프레임에 눌렸으면 true, 그렇지 않으면 false</returns>
        public static bool GetKeyDown(KeyAction keyAction)
        {
            return UnityEngine.Input.GetKeyDown(m_keyBindDictionary[keyAction]);
        }

        /// <summary>
        /// 특정 키가 이번 프레임에 떼어졌는지 확인합니다.
        /// </summary>
        /// <param name="keyAction">확인할 특정 키</param>
        /// <returns>키가 이번 프레임에 떼어졌으면 true, 그렇지 않으면 false</returns>
        public static bool GetKeyUp(KeyAction keyAction)
        {
            return UnityEngine.Input.GetKeyUp(m_keyBindDictionary[keyAction]);
        }
        #endregion
    }
}
