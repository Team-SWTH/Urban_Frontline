// ========================================
// File: SingletonBehaviour.cs
// Created: 2024-12-19 18:32:38
// Author: LHBM04
// ========================================

using UnityEngine;

namespace UrbanFrontline.Common
{
    /// <summary>
    /// Singleton 패턴의 MonoBehaviour 클래스.
    /// </summary>
    /// <typeparam name="TInstance">MonoBehaviour를 상속하는 타입</typeparam>
    public abstract class SingletonBehaviour<TInstance> : MonoBehaviour where TInstance : MonoBehaviour
    {
        /// <summary>
        /// 인스턴스.
        /// </summary>
        private static TInstance m_instance = null;

        /// <summary>
        /// Thread-Safe를 위한 Lock 객체.
        /// </summary>
        private static object m_lock = new();

        /// <summary>
        /// 현재 어플리케이션이 종료되었는가에 대한 여부.
        /// </summary>
        private static bool m_isApplicationQuit = false;

        /// <summary>
        /// 싱글턴 인스턴스를 가져옵니다.
        /// </summary>
        public static TInstance Instance
        {
            get
            {
                if (m_isApplicationQuit)
                {
                    return null;
                }

                lock (m_lock)
                {
                    if (!m_instance)
                    {
                        m_instance = FindAnyObjectByType<TInstance>() ?? new GameObject(typeof(TInstance).ToString()).AddComponent<TInstance>();
                        DontDestroyOnLoad(m_instance);
                    }

                    return m_instance;
                }
            }
        }

        protected virtual void OnApplicationQuit()
        {
            m_isApplicationQuit = true;
        }

        public virtual void OnDestroy()
        {
            m_isApplicationQuit = true;
        }
    }
}
