// ========================================
// File: ServerBae.cs
// Created: 2024-12-29 02:52:15
// Author: LHBM04
// ========================================

using System;
using System.Net.Sockets;
using UnityEngine;

namespace UrbanFrontline.Server.Core.Networks
{
    [Serializable]
    public struct ServerOption
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
    }
}

