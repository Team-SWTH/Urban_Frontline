// ========================================
// File: Singleton.cs
// Created: 2024-12-19 18:32:38
// Author: LHBM04
// ========================================using UnityEngine;

using System;
using System.Collections.Generic;
using UnityEngine;

namespace UrbanFrameworks.Common
{
    public static class Singleton<TInstance> where TInstance : MonoBehaviour
    {
        private static Dictionary<Type, TInstance> m_instanceBank = new();
        private static readonly object m_lock = new();

        public static TInstance GetInstance()
        {
            lock (m_lock)
            {
                if (!m_instanceBank.ContainsKey(typeof(TInstance)) || 
                    !m_instanceBank[typeof(TInstance)])
                {
                    TInstance instance = GameObject.FindObjectOfType<TInstance>();
                    if (!instance)
                    {
                        // 새 GameObject 생성 및 TInstance 추가
                        GameObject singletonObject = new($"{typeof(TInstance).Name}_Singleton");
                        instance = singletonObject.AddComponent<TInstance>();
                    }

                    m_instanceBank[typeof(TInstance)] = instance;
                }

                return m_instanceBank[typeof(TInstance)];
            }
        }
    }
}
