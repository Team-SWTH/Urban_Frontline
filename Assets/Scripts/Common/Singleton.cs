// ========================================
// File: Singleton.cs
// Created: 2024-12-27 05:15:58
// Author: LHBM04
// ========================================

using System;

namespace UrbanFrontline.Common
{
    /// <summary>  
    /// Singleton 패턴을 구현합니다.  
    /// </summary>  
    /// <typeparam name="TInstance">싱글톤 클래스 타입</typeparam>  
    public abstract class Singleton<TInstance> where TInstance : class
    {
        /// <summary>
        /// 인스턴스.
        /// </summary>
        private static TInstance m_instance = null;

        /// <summary>
        /// Thread-Safe를 위한 Lock 객체.
        /// </summary>
        private static object m_lock = new object();

        /// <summary>
        /// 싱글턴 인스턴스를 가져옵니다.
        /// </summary>
        public static TInstance Instance
        {
            get
            {
                lock (m_lock)
                {
                    if (m_instance == null)
                    {
                        m_instance ??= Activator.CreateInstance(typeof(TInstance), true) as TInstance;
                    }

                    return m_instance;
                }
            }
        }
    }
}
