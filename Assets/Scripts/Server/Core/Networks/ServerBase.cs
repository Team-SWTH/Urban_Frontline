// ========================================
// File: ServerBae.cs
// Created: 2024-12-29 02:52:15
// Author: LHBM04
// ========================================

using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace UrbanFrontline.Server.Core.Networks
{
    public abstract class ServerBase
    {
        protected abstract SocketType SocketType { get; }
        protected abstract ProtocolType ProtocolType { get; }
        
        public Socket Socket
        {
            get; 
            protected set;
        }

        public virtual string Address
        {
            get { return "127.0.0.1"; }
        }

        public virtual ushort Port
        {
            get { return 8080; }
        }

        public virtual int TickRate
        {
            get { return 60; }
        }

        public virtual int MaxConnections
        {
            get { return 10; }
        }

        public bool IsRunning
        {
            get;
            private set;
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

                Socket = new Socket(AddressFamily.InterNetwork, SocketType, ProtocolType);
                Socket.Bind(new IPEndPoint(IPAddress.Parse(Address), Port));

                if (ProtocolType == ProtocolType.Tcp)
                {
                    Socket.Listen(MaxConnections);
                }

                IsRunning = true;
                Debug.LogError($"Server started on {Address}:{Port}");
                OnServerStarted?.Invoke(this);

                m_routineTask = Task.Factory.StartNew(() => Routine(m_cts.Token), m_cts.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to start server: {ex.Message}");
                Stop();
            }
        }

        public virtual void Stop()
        {
            if (!IsRunning)
            {
                return;
            }

            IsRunning = false;
            m_cts.Cancel();

            try
            {
                m_routineTask?.Wait();
                m_routineTask?.Dispose();
                Socket?.Close();

                Debug.Log("Server stopped.");
                OnServerStopped?.Invoke(this);
            }
            catch (Exception ex)
            {
                Debug.LogError($"[Error] Failed to stop server: {ex.Message}");
            }
        }

        protected abstract void Routine(CancellationToken token);
    }
}

