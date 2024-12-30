// ========================================
// File: Client.cs
// Created: 2024-12-29 06:02:39
// Author: ※ 작성자 이름을 반드시 기입해주세요.
// ========================================

using System;
using UnityEngine;

namespace UrbanFrontline.Server.Core.Clients
{
    [Serializable]
    public struct Client
    {
        public string ipAddress;

        public string port;

        public Client(string ipAddress, string port)
        {
            this.ipAddress = ipAddress;
            this.port = port;
        }

        public Client(string ipAddress, ushort port)
        {
            this.ipAddress = ipAddress;
            this.port = port.ToString();
        }
    }
}