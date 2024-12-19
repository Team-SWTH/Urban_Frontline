// ========================================
// File: SessionHandler.cs
// Created: 2024-12-20 03:18:49
// Author: LHBM04
// ========================================

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UrbanFrontline.Server.Core.Networks
{
    /// <summary>
    /// 서버 내 모든 세션을 관리합니다.
    /// </summary>
    public sealed class SessionHandler
    {
        private List<Session> m_sessions = new();
    }
}
