// ========================================
// File: ServerHandler.cs
// Created: 2024-12-29 06:17:03
// Author: ※ 작성자 이름을 반드시 기입해주세요.
// ========================================

using System.Net;
using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace UrbanFrontline.Server.Core.Networks
{
    public class ServerHandler : MonoBehaviour
    {
        [SerializeField]
        private bool m_isRunning;

        [SerializeField]
        private ServerBase m_server;

        public Socket Socket
        {
            get;
            protected set;
        }

        private Task m_routineTask;
        private CancellationTokenSource m_cts;

        public event ServerEvent OnServerStarted;
        public event ServerEvent OnServerStopped;

        public virtual void Start()
        {
            try
            {
                m_cts = new CancellationTokenSource();

                Socket = new Socket(AddressFamily.InterNetwork, m_server.socketType, m_server.protocolType);
                Socket.Bind(new IPEndPoint(IPAddress.Parse(m_server.ipAddress), m_server.port));

                if (m_server.protocolType == ProtocolType.Tcp)
                {
                    Socket.Listen(m_server.backlogCount);
                }

                m_isRunning = true;
                Utilities.Logger.LogNotice($"서버가 \"{m_server.ipAddress}:{m_server.port}\"에서 시작되었습니다.");
                OnServerStarted?.Invoke(m_server);

                m_routineTask = Task.Factory.StartNew(() => Routine(m_cts.Token), m_cts.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            }
            catch (Exception ex)
            {
                Utilities.Logger.LogAssertion($"서버를 열 수 없었습니다!", ex);
                Stop();
            }
        }

        public virtual void Stop()
        {
            if (!m_isRunning)
            {
                return;
            }

            m_isRunning = false;
            m_cts.Cancel();

            try
            {
                m_routineTask?.Wait();
                m_routineTask?.Dispose();
                Socket?.Close();

                Utilities.Logger.LogNotice("서버를 닫았습니다.");
                OnServerStopped?.Invoke(m_server);

                Task.Delay(1000).Wait();
                Application.Quit();
            }
            catch (Exception ex)
            {
                Utilities.Logger.LogAssertion($"서버를 닫을 수 없습니다!", ex);
            }
        }

        protected async void Routine(CancellationToken token)
        {
            while (m_isRunning && !token.IsCancellationRequested)
            {
                string temp = await Task.Run(() => Console.ReadLine());
                if (temp == "exit")
                {
                    break;
                }

                Socket clientSocket = await Socket.AcceptAsync();
                if (clientSocket == null)
                {

                }

                Utilities.Logger.Log(temp);
            }

            Stop();
        }

        private void OnDestroy()
        {
            Stop();
        }

        private void OnApplicationQuit()
        {
            Stop();
        }
    }
}