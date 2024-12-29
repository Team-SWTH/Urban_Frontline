// ========================================
// File: ServerEvent.cs
// Created: 2024-12-29 03:26:15
// Author: LHBM04
// ========================================

using System;

namespace UrbanFrontline.Server.Core.Networks
{
    [Serializable]
    public delegate void ServerEvent(ServerBase server);
}
