// ========================================
// File: PoolBehaviour.cs
// Created: 2025-01-02 19:21:58
// Author: LHBM04
// ========================================

using System;
using System.Collections.Concurrent;
using UnityEngine;

namespace UrbanFrontline.Common
{
    /// <summary>
    /// Pool 패턴의 MonoBehaviour 클래스.
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    public class PoolBehaviour<TObject> : MonoBehaviour, IPool<TObject> where TObject : MonoBehaviour
    {
        /// <summary>
        /// 생성한 인스턴스를 담을 Queue.
        /// </summary>
        private ConcurrentQueue<TObject> m_poolQueue;

        /// <summary>
        /// 생성할 인스턴스의 프리팹.
        /// </summary>
        [SerializeField]
        [Tooltip("생성할 인스턴스의 프리팹.")]
        private GameObject m_prefab;

        /// <summary>
        /// 생성할 인스턴스의 기본 최대 개수.
        /// </summary>
        private static readonly int DEFAULT_MAX_SIZE = 20;

        /// <summary>
        /// 생성할 인스턴스의 최대 개수.
        /// </summary>
        [SerializeField]
        [Tooltip("생성할 인스턴스의 최대 개수.")]
        private int m_maxSize;

        public int CountActive
        {
            get { return m_maxSize - m_poolQueue.Count; }
        }

        public int CountInactive
        {
            get { return m_poolQueue.Count; }
        }

        private void Reset()
        {
            m_maxSize = DEFAULT_MAX_SIZE;
        }

        private void Start()
        {
            if (!m_prefab)
            {
#region UNITY_EDITOR
                Debug.LogException(new NullReferenceException("Prefab이 설정되지 않았습니다."));
#endregion
                return;
            }

            m_maxSize = m_maxSize > 0 ? m_maxSize : DEFAULT_MAX_SIZE;
            for (int count = 0; count < m_maxSize; ++count)
            {
                m_poolQueue.Enqueue(Create());
            }
        }

        public TObject Create()
        {
            TObject instance = Instantiate(m_prefab).GetComponent<TObject>();
            instance.gameObject.name = $"{m_prefab.name}_{m_poolQueue.Count}";
            instance.gameObject.transform.parent = transform;
            instance.gameObject.transform.position = transform.position;
            instance.gameObject.SetActive(false);

            return instance;
        }

        public TObject Get()
        {
            if (!m_poolQueue.TryDequeue(out TObject newObj))
            {
                m_maxSize++;
                return Create();
            }

            return newObj;
        }

        public void Release(TObject usedObj)
        {
            m_poolQueue.Enqueue(usedObj);
            usedObj.gameObject.transform.parent = transform;
            usedObj.gameObject.transform.position = transform.position;
            usedObj.gameObject.SetActive(false);
        }
    }
}