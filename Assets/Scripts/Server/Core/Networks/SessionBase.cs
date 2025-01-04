// ========================================
// File: SessionBase.cs
// Created: 2024-12-27 06:55:34
// Author: LHBM04
// ========================================

using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UrbanFrontline.Server.Core.Packets;
using UrbanFrontline.Server.Core.Utilities;

namespace UrbanFrontline.Server.Core.Networks
{
    /// <summary>
    /// 세션의 기본 동작을 구현합니다.
    /// </summary>
    public abstract class SessionBase
    {
        #region Fields
        private Socket m_socket;
        private int m_isConnected;
        private object m_lock = new object();
        #endregion

        #region Buffers
        private ReceiveBuffer receiveBuffer = new ReceiveBuffer();
        private SendBuffer sendBuffer = new SendBuffer();
        #endregion

        #region Connection Management
        public void Connect(Socket connectedSocket)
        {
            m_socket = connectedSocket;
            OnConnected(connectedSocket.RemoteEndPoint);
            StartReceive();
        }

        public void Disconnect()
        {
            if (Interlocked.Exchange(ref m_isConnected, 1) == 1)
                return;

            OnDisconnected();

            m_socket.Shutdown(SocketShutdown.Both);
            m_socket.Close();
        }
        #endregion

        #region Data Transmission
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
        #endregion

        #region Data Reception
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
            // 1, 데이터 무결성 검사.
            int dataSize = BitConverter.ToUInt16(buffer.Array, buffer.Offset);
            if (buffer.Count < dataSize)
            {
                return;
            }

            // 2. 데이터 역직렬화.
            PacketBase.EType packetType = (PacketBase.EType)BitConverter.ToUInt16(buffer.Array, buffer.Offset + 2);
            PacketBase packet = null;
            switch (packetType)
            {
                case PacketBase.EType.C_Join:
                    packet = PacketHandler.Deserialize<C_Join>(buffer.Array);
                    break;
                case PacketBase.EType.S_Join:
                    packet = PacketHandler.Deserialize<S_Join>(buffer.Array);
                    break;
                case PacketBase.EType.C_Left:
                    packet = PacketHandler.Deserialize<C_Left>(buffer.Array);
                    break;
                // 다른 타입들 처리...
                default:
                    Logger.LogWarning("Unknown packet type: " + packetType);
                    return;
            }

            // 3. 패킷이 정상적으로 역직렬화되었으면 처리
            OnReceivePacket(new ArraySegment<byte>(buffer.Array, buffer.Offset + 4, dataSize - 4));
        }
        #endregion

        #region Abstract Methods
        protected abstract void OnConnected(EndPoint endPoint);
        protected abstract void OnDisconnected();
        protected abstract void OnSendPacket(int numBytes);
        protected abstract void OnReceivePacket(ArraySegment<byte> payload);
        #endregion
    }
}