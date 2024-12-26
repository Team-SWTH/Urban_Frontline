// ========================================
// File: NetworkManager.cs
// Created: 2024-12-27 05:31:31
// Author: LHBM04
// ========================================

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;
using UrbanFrontline.Common;

namespace UrbanFrontline.Server.Core.Networks
{
    /// <summary>
    /// 네트워크 매니저.
    /// </summary>
    public sealed class NetworkManager : SingletonBehaviour<NetworkManager>
    {
        #region Server Settings
        /// <summary>
        /// 서버 핸들러.
        /// </summary>
        [Header("Server Settings")]
        [Tooltip("서버 핸들러.")]
        [SerializeField]
        private ServerHandler m_serverHandler;

        /// <summary>
        /// 서버 호스트의 DNS 주소.
        /// </summary>
        [Tooltip("서버 호스트의 DNS 주소.")]
        [Space, SerializeField]
        private string m_hostDNS;

        /// <summary>
        /// 서버가 열릴 포트.
        /// </summary>
        [Tooltip("서버가 열릴 포트.")]
        [SerializeField]
        private int m_port;
        #endregion

        #region Session Settings
        /// <summary>
        /// 
        /// </summary>
        [Header("Session Settings")]
        [Tooltip("")]
        [SerializeField]
        private SessionHandler m_sessionHandler;

        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [Space, SerializeField]
        private int m_tickRate;

        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [SerializeField]
        private float m_timeout;
        #endregion

        private NetworkManager()
        {
            // 외부에서 인스턴스를 생성하지 못하도록 하기 위함.
        }

        private void Awake()
        {
            
        }

        private void Start()
        {
            if (m_serverHandler == null)
            {
                return;
            }

            if (m_serverHandler.Initialize(new IPEndPoint(IPAddress.Parse(m_hostDNS), m_port)))
            {
                Debug.Log($"서버가 {m_hostDNS}:{m_port}에서 열렸습니다.");
            }
            else
            {
                Debug.LogError("서버 초기화에 실패했습니다.");
            }
        }

        protected override void OnApplicationQuit()
        {
            base.OnApplicationQuit();
            if (m_serverHandler != null) 
            {
                m_serverHandler.Release();
            }
        }
    }
}
