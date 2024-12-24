// ========================================
// File: NetworkManager.cs
// Created: 2024-12-20 02:40:32
// Author: LHBM04
// ========================================

using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using R3;
using UnityEngine;
using UnityEngine.Events;
using UrbanFrontline.Common;

namespace UrbanFrontline.Server.Core.Networks
{
    /// <summary>
    /// 서버를 초기화하고, 이를 관리합니다.
    /// </summary>
    public sealed class NetworkManager : Singleton<NetworkManager>
    {
        /// <summary>
        /// 
        /// </summary>
        [SerializeField]
        private ServerHandler m_serverHandler;

        /// <summary>
        /// 클라이언트 핸들러.
        /// </summary>
        [SerializeField]
        private ClientHandler m_clientHandler;

        /// <summary>
        /// 세션 핸들러.
        /// </summary>
        [SerializeField]
        private SessionHandler m_sessionHandler;

        private async void Start()
        {
            await m_serverHandler.StartServerAsync();
        }

        protected override void OnApplicationQuit()
        {
            base.OnApplicationQuit();
        }
    }
}