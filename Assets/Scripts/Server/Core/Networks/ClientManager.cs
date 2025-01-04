// ========================================
// File: ClientManager.cs
// Created: 2025-01-04 10:15:33
// Author: LHBM04
// ========================================

using System.Net;
using System.Net.Sockets;
using UrbanFrontline.Common;

namespace UrbanFrontline.Server.Core.Networks
{
    public sealed class ClientManager : SingletonBehaviour<ClientManager>
    {
        private Socket m_clientSocket;

        private void Awake()
        {
            m_clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            m_clientSocket.Blocking = false;
        }

        private void Start()
        {
            try
            {
                m_clientSocket.Connect(new IPEndPoint(IPAddress.Any, 8080));
            }
            catch (SocketException exception)
            {
                if (exception.SocketErrorCode == SocketError.WouldBlock)
                {
                    // TODO: 비동기 연결 처리
                }
            }
        }
    }
}