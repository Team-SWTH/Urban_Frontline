// ========================================
// File: HostManager.cs
// Created: 2025-01-04 10:18:18
// Author: LHBM04
// ========================================

using Microsoft.Win32;
using System;
using System.Net;
using System.Net.Sockets;
using UrbanFrontline.Common;

namespace UrbanFrontline.Server.Core.Networks
{
    /// <summary>
    /// 
    /// </summary>
    public class HostManager : SingletonBehaviour<HostManager>
    {
        public ServerOption option;

        public Socket m_serverSocket;

        private void Awake()
        {
            m_serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            m_serverSocket.Bind(new IPEndPoint(IPAddress.Parse(option.ipAddress), option.port));
            m_serverSocket.Listen(option.backlogCount);

            // 임시로 10명만
            for (int i = 0; i < 10; i++)
            {
                SocketAsyncEventArgs args = new SocketAsyncEventArgs();
                args.Completed += new EventHandler<SocketAsyncEventArgs>(OnAcceptCompleted);
                RegisterAccept(args);
            }

            Utilities.Logger.LogNotice($"서버가 \"{option.ipAddress}:{option.port}\"에서 시작되었습니다.");
        }

        private void RegisterAccept(SocketAsyncEventArgs args)
        {
            args.AcceptSocket = null;

            if (!m_serverSocket.AcceptAsync(args))
            {
                OnAcceptCompleted(null, args);
            }
        }

        private void OnAcceptCompleted(object sender, SocketAsyncEventArgs args)
        {
            if (args.SocketError == SocketError.Success)
            {
                // SessionBase newSession = m_sessionFactory.Invoke();
                // newSession.Connect(args.AcceptSocket);
            }
            else
            {
                Console.WriteLine(args.SocketError.ToString());
            }

            RegisterAccept(args);
        }
    }
}