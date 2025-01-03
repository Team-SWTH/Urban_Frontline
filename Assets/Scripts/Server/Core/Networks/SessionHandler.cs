// ========================================
// File: SessionHandler.cs
// Created: 2024-12-27 06:55:17
// Author: LHBM04
// ========================================

using System;
using System.Collections.Concurrent;
using UnityEngine;

namespace UrbanFrontline.Server.Core.Networks
{
    /// <summary>
    /// 세션 핸들러.
    /// </summary>
    public class SessionHandler : MonoBehaviour
    {
        /// <summary>
        /// 세션 목록.
        /// </summary>
        private ConcurrentDictionary<Guid, SessionBase> m_sessions;
    }
}
