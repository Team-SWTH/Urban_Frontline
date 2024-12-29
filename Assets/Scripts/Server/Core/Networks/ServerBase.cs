// ========================================
// File: ServerBae.cs
// Created: 2024-12-29 02:52:15
// Author: LHBM04
// ========================================

using System.Net.Sockets;
using UnityEngine;

namespace UrbanFrontline.Server.Core.Networks
{
    public class ServerBase : MonoBehaviour
    {
        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        public SocketType socketType;

        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        public ProtocolType protocolType;

        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        public string ipAddress;

        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        public ushort port;

        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        public int tickRate;

        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        public int backlogCount;

        protected virtual void Reset()
        {
            socketType = SocketType.Stream;
            protocolType = ProtocolType.Tcp;

            ipAddress = "127.0.0.1";
            port = 8080;

            tickRate = 60;
            backlogCount = 100;
        }
    }
}

