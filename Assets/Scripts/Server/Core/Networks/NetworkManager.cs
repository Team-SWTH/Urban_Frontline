// ========================================
// File: NetworkManager.cs
// Created: 2024-12-20 02:40:32
// Author: LHBM04
// ========================================

using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Events;

namespace UrbanFrontline.Server.Core.Networks
{
    /// <summary>
    /// 서버를 초기화하고, 이를 관리합니다.
    /// </summary>
    public sealed class NetworkManager : MonoBehaviour
    {
        /// <summary>
        /// UDP 방식의 서버 핸들러.
        /// </summary>
        private UdpClient m_udpServer;

        /// <summary>
        /// 해당 서버의 IP와 포트 정보를 담고 있는 엔드 포인트.
        /// </summary>
        private IPEndPoint m_clientEndPoint;

        /// <summary>
        /// 서버 아이피.
        /// </summary>
        [Tooltip("서버 아이피")]
        private IPAddress m_ipAddress;
        public IPAddress IPAddress => m_ipAddress;

        /// <summary>
        /// 포트.
        /// </summary>
        [Tooltip("포트.")]
        private ushort m_port = 12345;
        public ushort Port => m_port;

        /// <summary>
        /// 현재 서버가 운영되고 있는가에 대한 여부.
        /// </summary>
        [Tooltip("현재 서버가 운영되고 있는가에 대한 여부.")]
        [SerializeField]
        private bool m_isRunning;
        public bool IsRunning => m_isRunning;

        private void Reset()
        {
            m_udpServer = new UdpClient(m_port);
            m_clientEndPoint = new IPEndPoint(IPAddress.Any, m_port);
        }

        private void Awake()
        {
            try
            {
                Debug.Log("서버를 가동합니다...");
                m_udpServer ??= new UdpClient(m_port);
                m_clientEndPoint ??= new IPEndPoint(IPAddress.Any, m_port);
                Debug.Log($"서버가 포트 \'{m_port}\'에서 열렸습니다.");
            } 
            catch 
            {
                Debug.LogError("!!");
                m_udpServer.Close();
            }
        }
            
        private void OnApplicationQuit()
        {
            try
            {
                Debug.Log("서버를 닫습니다...");
                m_udpServer.Close();
                Debug.Log("서버가 정상적으로 종료되었습니다.");
            } 
            catch 
            {
                Debug.LogError("!!");
            }
        }
    }
}