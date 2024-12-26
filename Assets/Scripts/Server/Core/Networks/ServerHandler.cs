// ========================================
// File: ServerHandler.cs
// Created: 2024-12-27 05:35:29
// Author: LHBM04
// ========================================

using Cysharp.Threading.Tasks;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using UnityEngine;

namespace UrbanFrontline.Server.Core.Networks
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ServerHandler : MonoBehaviour
    {
        /// <summary>
        /// 입장할 수 있는 클라이언트의 수.
        /// </summary>
        [Tooltip("입장할 수 있는 클라이언트의 수.")]
        [SerializeField]
        private int m_registerCount;

        /// <summary>
        /// 
        /// </summary>
        [Tooltip("")]
        [SerializeField]
        private int m_backlogCount;

        private bool m_isRunning;

        /// <summary>
        /// 통신을 위한 TCP 프로토콜 방식의 소켓.
        /// </summary>
        private Socket m_socket;

        public bool Initialize(IPEndPoint endPoint)
        {
            try
            {
                // 1. 소켓 생성
                m_socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                m_socket.Bind(endPoint);
                m_socket.Listen(m_backlogCount);

                m_isRunning = true;

                return true;
            }
            catch (Exception e)
            {
#if UNITY_SERVER || UNITY_EDITOR
                Debug.LogError(e.Message);
#endif
                return false;
            }
        }

        public async void Routine()
        {
            try
            {
                while (true)
                {
                    // 서버가 멈추면 루프를 빠져나온다.
                    // 왠지는 모르겠는데 while(m_isRunning) <-- 이렇게 작성하면 반드시 SocketException이 발생한다.
                    if (!m_isRunning) 
                    { 
                        break;
                    }

          
                    Socket clientSocket = await m_socket.AcceptAsync();
                    // 1. 처음 보는 소캣인가? 그렇다면 새로운 세션을 생성한다.
                    // if (!FindFirstObjectByType<SessionHandler>().Has(clientSocket.AddressFamily))
                    // {
                    //     Session newSession = new Session(clientSocket);
                    //     continue;    
                    // }
                    // 2. 이미 알고 있는 소켓인가? 그렇다면 세션을 업데이트한다.
                    // 현재 받은 소켓만 주고, 데이터 처리는 세션에서 한다.
                    // FindAnyObjectByType<SessionHandler>().Get(clientSocket).Update();
                    clientSocket.Close();
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        public void Release()
        {
            m_socket.Close();
        }
    }
}
