// ========================================
// File: SessionBase.cs
// Created: 2024-12-27 06:55:34
// Author: LHBM04
// ========================================

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace UrbanFrontline.Server.Core.Networks
{
    /// <summary>
    /// 세션.
    /// </summary>
    public abstract class SessionBase
    {
        private Socket m_socket;
        private int m_isConnected;
        private object m_lock = new object();

        /// <summary>
        /// 수신용 버퍼.
        /// </summary>
        protected ReceiveBuffer receiveBuffer
        {
            get;
            private set;
        } = new ReceiveBuffer();

        /// <summary>
        /// 송신용 버퍼.
        /// </summary>
        protected SendBuffer sendBuffer
        {
            get;
            private set;
        } = new SendBuffer();



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
            catch (Exception e)
            {
                Console.WriteLine(e);
                Disconnect();
            }
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                int bytesSent = m_socket.EndSend(ar);
                OnSend(bytesSent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Disconnect();
            }
        }

        private void StartReceive()
        {
            var segment = receiveBuffer.WriteSegment;
            try
            {
                m_socket.BeginReceive(segment.Array, segment.Offset, segment.Count, SocketFlags.None, ReceiveCallback, null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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
            catch (Exception e)
            {
                Console.WriteLine(e);
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
            OnRecvPacket(packetId, new ArraySegment<byte>(buffer.Array, buffer.Offset + 4, dataSize - 4));
        }

        protected abstract void OnConnected(EndPoint endPoint);
        protected abstract void OnSend(int numBytes);
        protected abstract void OnRecvPacket(ushort packetId, ArraySegment<byte> payload);
        protected abstract void OnDisconnected();
    }
}
