// ========================================
// File: TestChatField.cs
// Created: 2025-01-04 07:22:33
// Author: ※ 작성자 이름을 반드시 기입해주세요.
// ========================================

using UnityEngine;
using UnityEngine.UI;

namespace UrbanFrontline.Server.GUI
{
    public class TestChatField : MonoBehaviour
    {
        [SerializeField]
        private InputField m_inputField;

        private void Reset()
        {
            m_inputField = GetComponentInChildren<InputField>();
        }

        private void Awake()
        {
            m_inputField = m_inputField ?? GetComponentInChildren<InputField>();
        }

        private void Start()
        {
            
        }
    }
}