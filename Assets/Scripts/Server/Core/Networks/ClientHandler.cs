// ========================================
// File: ClientManager.cs
// Created: 2024-12-20 03:02:31
// Author: LHBM04
// ========================================

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;

namespace UrbanFrontline.Server.Core.Networks
{
    /// <summary>
    /// 서버 내에 접속한 클라이언트들을 관리합니다.
    /// </summary>
    public sealed class ClientHandler
    {
        /// <summary>
        /// 서버 내 클라이언트를 모으는 List.
        /// </summary>
        private readonly ConcurrentDictionary<Guid, Client> m_clients = new();

        /// <summary>
        /// 클라이언트 추가 및 제거에 사용하는 Lock.
        /// </summary>
        private readonly object m_lock = new object();

        /// <summary>
        /// 새로운 클라이언트를 등록합니다.
        /// </summary>
        /// <param name="client">등록할 클라이언트</param>
        public void RegisterClient(Client client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            lock (m_lock)
            {
                m_clients[client.ID] = client;
                Console.WriteLine($"Client {client.ID} registered.");
            }
        }

        public bool IsClientRegistered(IPEndPoint endPoint)
        {
            lock (m_lock)
            {
                return m_clients.Any(client => client.Value.EndPoint == endPoint);
            }
        }

        public Client GetClient(IPEndPoint endPoint)
        {
            lock (m_lock)
            {
                return m_clients.FirstOrDefault(x => x.Value.EndPoint == endPoint).Value;
            }
        }

        /// <summary>
        /// 클라이언트를 제거합니다.
        /// </summary>
        /// <param name="clientId">제거할 클라이언트의 ID</param>
        public void RemoveClient(Guid clientId)
        {
            lock (m_lock)
            {
                if (m_clients.TryRemove(clientId, out Client client))
                {
                    client.Disconnect();
                    Console.WriteLine($"Client {clientId} removed.");
                }
            }
        }

        /// <summary>
        /// 특정 클라이언트에 메시지를 보냅니다.
        /// </summary>
        /// <param name="clientId">클라이언트 ID</param>
        /// <param name="message">보낼 메시지</param>
        public void SendMessage(Guid clientId, byte[] message)
        {
            if (m_clients.TryGetValue(clientId, out var client))
            {
                client.Send(message);
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogError($"Client {clientId} not found.");
#endif
            }
        }

        /// <summary>
        /// 모든 클라이언트에 메시지를 브로드캐스트합니다.
        /// </summary>
        /// <param name="message">브로드캐스트할 메시지</param>
        public void BroadcastMessage(byte[] message)
        {
            foreach (var client in m_clients.Values)
            {
                client.Send(message);
            }
        }

        /// <summary>
        /// 현재 활성 상태의 클라이언트 목록을 반환합니다.
        /// </summary>
        /// <returns>활성 클라이언트 ID 목록</returns>
        public IEnumerable<Guid> GetActiveClients()
        {
            return m_clients.Keys;
        }

        /// <summary>
        /// 모든 클라이언트를 정리하고 종료합니다.
        /// </summary>
        public void ShutdownAllClients()
        {
            foreach (var client in m_clients.Values)
            {
                client.Disconnect();
            }

            m_clients.Clear();
#if UNITY_EDITOR
            Debug.Log("All clients have been disconnected.");
#endif
        }
    }
}