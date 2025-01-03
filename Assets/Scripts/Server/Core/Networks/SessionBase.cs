// ========================================
// File: SessionBase.cs
// Created: 2024-12-27 06:55:34
// Author: LHBM04
// ========================================

using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UrbanFrontline.Server.Core.Utilities;

namespace UrbanFrontline.Server.Core.Networks
{
    /// <summary>
    /// 세션의 기본 동작을 구현합니다.
    /// </summary>
    public abstract class SessionBase
    {
        /// <summary>
        /// 소켓.
        /// </summary>
        private Socket m_socket;

        /// <summary>
        /// 연결 상태.
        /// </summary>
        private int m_isConnected;

        /// <summary>
        /// Lock을 위한 객체.
        /// </summary>
        private object m_lock = new object();

        #region I/O Buffers
        /// <summary>
        /// 수신용 버퍼.
        /// </summary>
        protected ReceiveBuffer receiveBuffer
        {
            get;
        } = new ReceiveBuffer();

        /// <summary>
        /// 송신용 버퍼.
        /// </summary>
        protected SendBuffer sendBuffer
        {
            get;
        } = new SendBuffer();
        #endregion

        #region Events
        /// <summary>
        /// 세션에 연결되었을 때 발생하는 이벤트.
        /// </summary>
        public event ConnectEvent onConnected;

        /// <summary>
        /// 패킷을 수신받았을 때 발생하는 이벤트.
        /// </summary>
        public event ConnectEvent onReceive;

        /// <summary>
        /// 패킷을 송신할 때 발생하는 이벤트.
        /// </summary>
        public event ConnectEvent onSend;

        /// <summary>
        /// 세션에서 연결이 해제되었을 때 발생하는 이벤트.
        /// </summary>
        public event ConnectEvent onDisconnected;
        #endregion

        /// <summary>
        /// 세션을 서버와 연결합니다.
        /// </summary>
        /// <param name="connectedSocket"></param>
        public void Connect(Socket connectedSocket)
        {
            m_socket = connectedSocket;
            OnConnected(connectedSocket.RemoteEndPoint);

            StartReceive();
        }

        /// <summary>
        /// 세션을 서버로부터 연결 해제합니다.
        /// </summary>
        public void Disconnect()
        {
            // 중첩 해제 방지.
            if (Interlocked.Exchange(ref m_isConnected, 1) == 1)
            {
                return;
            }

            OnDisconnected();
            m_socket.Shutdown(SocketShutdown.Both);
            m_socket.Close();
        }

        /// <summary>
        /// 데이터를 송신합니다.
        /// </summary>
        /// <param name="data">송신할 데이터.</param>
        public void Send(byte[] data)
        {
            lock (m_lock)
            {
                ArraySegment<byte> segment = sendBuffer.Open(data.Length);
                Array.Copy(data, 0, segment.Array, segment.Offset, data.Length);
                sendBuffer.Close(data.Length);

                RegisterSend();
            }
        }

        private void RegisterSend()
        {
            try
            {
                ArraySegment<byte> segment = sendBuffer.Open(sendBuffer.Clearance);
                m_socket.BeginSend(segment.Array, segment.Offset, segment.Count, SocketFlags.None, SendCallback, null);
            }
            catch (Exception exception)
            {
                Logger.LogException(exception);
                Disconnect();
            }
        }

        protected virtual void OnConnected(EndPoint endPoint)
        {
            onConnected?.Invoke(endPoint);
            Logger.LogNotice($"새로운 클라이언트와 연결되었습니다. {endPoint}");
        }

        protected virtual void OnDisconnected()
        {
            onDisconnected?.Invoke(m_socket.RemoteEndPoint);
            Logger.LogNotice($"새로운 클라이언트가 연결 해제되었습니다. {m_socket.RemoteEndPoint}");
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                int bytesSent = m_socket.EndSend(ar);
                OnSendPacket(bytesSent);
            }
            catch (Exception exception)
            {
                Logger.LogException(exception);
                Disconnect();
            }
        }

        private void StartReceive()
        {
            ArraySegment<byte> segment = receiveBuffer.WriteSegment;
            try
            {
                m_socket.BeginReceive(segment.Array, segment.Offset, segment.Count, SocketFlags.None, ReceiveCallback, null);
            }
            catch (Exception exception)
            {
                Logger.LogException(exception);
                Disconnect();
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                int bytesRead = m_socket.EndReceive(ar);
                if (bytesRead > 0)
                {
                    receiveBuffer.OnWrite(bytesRead);
                    ProcessBuffer();
                    StartReceive();
                }
                else
                {
                    Disconnect();
                }
            }
            catch (Exception exception)
            {
                Logger.LogException(exception);
                Disconnect();
            }
        }

        private void ProcessBuffer()
        {
            while (true)
            {
                ArraySegment<byte> readSegment = receiveBuffer.ReadSegment;
                if (readSegment.Count < 4)
                {
                    break;
                }

                int packetSize = BitConverter.ToInt32(readSegment.Array, readSegment.Offset);
                if (receiveBuffer.DataSize < packetSize)
                {
                    break;
                }

                ArraySegment<byte> segment = new ArraySegment<byte>(readSegment.Array, readSegment.Offset, packetSize);
                ProcessPacket(segment);
                receiveBuffer.OnRead(packetSize);
            }
        }

        private void ProcessPacket(ArraySegment<byte> buffer)
        {
            // 데이터 크기와 패킷 ID 읽기
            ushort dataSize = BitConverter.ToUInt16(buffer.Array, buffer.Offset);
            ushort packetId = BitConverter.ToUInt16(buffer.Array, buffer.Offset + 2);

            // 데이터 크기가 부족하면 처리하지 않음
            if (buffer.Count < dataSize)
            {
                return;
            }

            // 패킷 데이터를 OnRecvPacket에 전달
            OnReceivePacket(new ArraySegment<byte>(buffer.Array, buffer.Offset + 4, dataSize - 4));
        }

        protected abstract void OnSendPacket(int numBytes);

        /// <summary>
        /// 패킷을 
        /// </summary>
        /// <param name="payload"></param>
        protected abstract void OnReceivePacket(ArraySegment<byte> payload);
    }
}
