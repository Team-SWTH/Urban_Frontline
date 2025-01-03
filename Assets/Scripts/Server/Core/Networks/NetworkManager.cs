// ========================================
// File: NetworkManager.cs
// Created: 2024-12-31 21:32:22
// Author: LHBM04
// ========================================

using UnityEngine;
using UrbanFrontline.Common;

namespace UrbanFrontline.Server.Core.Networks
{
    public class NetworkManager : SingletonBehaviour<NetworkManager>
    {
        [SerializeField]
        private Listener m_listener;

        [SerializeField]
        private Connector m_connector;

        [SerializeField]
        private JobScheduler m_jobScheduler;
    }
}