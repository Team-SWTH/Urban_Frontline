// ========================================
// File: Client.cs
// Created: 2024-12-20 03:19:13
// Author: LHBM04
// ========================================

using System;
using System.Net;
using System.Net.Sockets;

namespace UrbanFrontline.Server.Core.Networks
{
    /// <summary>
    /// 서버에 연결된 클라이언트의 네트워크 정보를 관리합니다.
    /// </summary>
    public sealed class Client : IEquatable<Client>
    {
        /// <summary>
        /// 해당 클라이언트의 고유 아이디.
        /// </summary>
        private Guid m_id;
        public Guid ID => m_id;

        /// <summary>
        /// 해당 클라이언트의 IP와 포트 정보를 포함하는 엔드 포인트.
        /// </summary>
        private IPEndPoint m_endPoint;
        public IPEndPoint EndPoint => m_endPoint;

        /// <summary>
        /// 클라이언트의 Socket.
        /// </summary>
        private Socket m_socket;
        public Socket Socket => m_socket;

        public Client(IPEndPoint ipEndPoint)
        {
            m_id = Guid.NewGuid();
            m_endPoint = ipEndPoint;
        }

        /// <summary>
        /// 데이터를 클라이언트로 전송합니다.
        /// </summary>
        public void Send(byte[] data)
        {
            if (m_socket?.Connected == true)
            {
                m_socket.Send(data);
            }
        }

        /// <summary>
        /// 클라이언트와의 연결을 종료합니다.
        /// </summary>
        public void Disconnect()
        {
            if (m_socket?.Connected == true)
            {
                m_socket.Shutdown(SocketShutdown.Both);
                m_socket.Close();
            }
        }

        /// <summary>
        /// 두 Client 객체가 동일한지 여부를 확인합니다.
        /// </summary>
        /// <param name="other">비교할 다른 Client 객체</param>
        /// <returns>동일하면 true, 그렇지 않으면 false</returns>
        public bool Equals(Client? other)
        {
            if (other is null)
                return false;

            return m_id == other.m_id;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Client client)
            {
                return Equals(client);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return m_id.GetHashCode();
        }

        public static bool operator ==(Client? left, Client? right)
        {
            if (left is null)
                return right is null;

            return left.Equals(right);
        }

        public static bool operator !=(Client? left, Client? right)
        {
            return !(left == right);
        }
    }
}
