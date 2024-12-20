// ========================================
// File: NetworkManager.cs
// Created: 2024-12-20 02:40:32
// Author: LHBM04
// ========================================

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UniRx;
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
        private UdpClient m_serverHandler;

        /// <summary>
        /// 해당 서버의 IP와 포트 정보를 담고 있는 엔드 포인트.
        /// </summary>
        private IPEndPoint m_serverEndPoint;

        /// <summary>
        /// 클라이언트 핸들러.
        /// </summary>
        private ClientHandler m_clientHandler;

        /// <summary>
        /// 세션 핸들러.
        /// </summary>
        private SessionHandler m_sessionHandler;

        /// <summary>
        /// 서버 아이피.
        /// </summary>
        public IPAddress IPAddress => IPAddress.Any;

        public ushort Port => 12345;

        /// <summary>
        /// 현재 서버가 운영되고 있는가에 대한 여부.
        /// </summary>
        public bool IsRunning { get; private set; }

        private void Awake()
        {
            try
            {
                Debug.Log("서버를 가동합니다...");
                m_serverHandler ??= new UdpClient(Port);
                m_serverEndPoint ??= new IPEndPoint(IPAddress, Port);
                m_clientHandler ??= new ClientHandler();
                m_sessionHandler ??= new SessionHandler();
                IsRunning = true;

                ReceiveDataAsync();
                Debug.Log($"서버가 포트 \'{Port}\'에서 열렸습니다.");
            }
            catch (Exception ex)
            {
                Debug.LogError($"서버 시작 중 오류 발생: {ex.Message}");
                m_serverHandler.Close();
            }
        }

        private async void ReceiveDataAsync()
        {
            while (IsRunning)
            {
                try
                {
                    var result = await m_serverHandler.ReceiveAsync();
                    HandleReceivedData(result.Buffer, result.RemoteEndPoint);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"데이터 수신 중 오류 발생: {ex.Message}");
                }
            }
        }

        private void HandleReceivedData(byte[] data, IPEndPoint remoteEndPoint)
        {
            string receivedMessage = Encoding.UTF8.GetString(data);
            Debug.Log($"데이터 수신: {receivedMessage} (From: {remoteEndPoint})");

            // 클라이언트 처리
            if (!m_clientHandler.IsClientRegistered(remoteEndPoint))
            {
                m_clientHandler.RegisterClient(new Client(remoteEndPoint));
                Debug.Log($"새 클라이언트 등록: {remoteEndPoint}");
            }

            Client clientInstance = m_clientHandler.GetClient(remoteEndPoint);
            if (!m_sessionHandler.HasSession(clientInstance))
            {
                Session session = new Session(clientInstance);
                m_sessionHandler.RegisterSession(session);
                Debug.Log($"새 세션 생성: {session.ID}");
            }

            // 세션 활성화 시간 갱신
            m_sessionHandler.GetSession(clientInstance).Update();
            m_sessionHandler.CleanupSessions(TimeSpan.FromMinutes(3.0));
        }

        private void OnApplicationQuit()
        {
            try
            {
                Debug.Log("서버를 닫습니다...");
                IsRunning = false;
                m_serverHandler.Close();
                Debug.Log("서버가 정상적으로 종료되었습니다.");
            }
            catch (Exception ex)
            {
                Debug.LogError($"서버 종료 중 오류 발생: {ex.Message}");
            }
        }
    }
}