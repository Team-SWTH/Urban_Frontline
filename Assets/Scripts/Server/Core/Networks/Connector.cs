// ========================================
// File: Connector.cs
// Created: 2025-01-04 01:59:14
// Author: LHBM04
// ========================================

using System.Net.Sockets;
using System.Net;
using UnityEngine;

namespace UrbanFrontline.Server.Core.Networks
{
    public class Connector : MonoBehaviour
    {
        private Socket _clientSocket;

        public void Connect(string ipAddress, int port)
        {
            _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _clientSocket.Blocking = false;

            try
            {
                _clientSocket.Connect(new IPEndPoint(IPAddress.Parse(ipAddress), port));
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