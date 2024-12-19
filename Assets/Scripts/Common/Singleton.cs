// ========================================
// File: Singleton.cs
// Created: 2024-12-19 18:32:38
// Author: LHBM04
// ========================================using UnityEngine;

using System;
using System.Collections.Generic;
using UnityEngine;

namespace UrbanFrontline.Common
{
    /// <summary>
    /// 싱글턴 패턴을 구현한 정적 클래스입니다.
    /// 특정 MonoBehaviour 타입에 대한 전역적으로 접근 가능한 인스턴스를 제공합니다.
    /// </summary>
    /// <typeparam name="TInstance">MonoBehaviour를 상속하는 타입</typeparam>
    public static class Singleton<TInstance> where TInstance : MonoBehaviour
    {
        /// <summary>
        /// 타입별로 인스턴스를 저장하는 딕셔너리.
        /// </summary>
        private static Dictionary<Type, TInstance> m_instanceBank = new();
        
        /// <summary>
        /// Thread-Safe를 위한 Lock Object.
        /// </summary>
        private static readonly object m_lock = new();

        /// <summary>
        /// 싱글턴 인스턴스를 가져옵니다.
        /// </summary>
        /// <returns>해당 타입의 싱글턴 인스턴스.</returns>
        public static TInstance GetInstance()
        {
            lock (m_lock)
            {
                if (!m_instanceBank.ContainsKey(typeof(TInstance)) || 
                    !m_instanceBank[typeof(TInstance)])
                {
                    TInstance instance = GameObject.FindFirstObjectByType<TInstance>();
                    if (!instance)
                    {
                        GameObject singletonObject = new($"{typeof(TInstance).Name}");
                        instance = singletonObject.AddComponent<TInstance>();
                    }
                    
                    GameObject.DontDestroyOnLoad(m_instanceBank[typeof(TInstance)]);
                    m_instanceBank[typeof(TInstance)] = instance;
                }
                
                return m_instanceBank[typeof(TInstance)];
            }
        }
    }
}
