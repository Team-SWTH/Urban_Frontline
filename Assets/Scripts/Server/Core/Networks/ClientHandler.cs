// ========================================
// File: ClientHandler.cs
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
        /// 클라이언트들을 관리하는 컬렉션.
        /// </summary>
        private readonly ConcurrentDictionary<Guid, Client> _clients = new();

        /// <summary>
        /// 클라이언트를 관리하는 메서드.
        /// </summary>
        /// <param name="endPoint">클라이언트의 IP 엔드포인트.</param>
        /// <param name="data">수신된 데이터.</param>
        public void HandleClient(IPEndPoint endPoint, byte[] data)
        {
            // 1. 클라이언트를 검색하거나 등록
            var client = GetClientByEndPoint(endPoint);
            if (client == null)
            {
                client = new Client(endPoint);
                RegisterClient(client);
                Debug.Log($"New client connected: {endPoint}");
            }

            // 2. 데이터 처리
            if (data != null && data.Length > 0)
            {
                // 클라이언트 핑 업데이트
                client.Update(DateTime.Now);

                // 데이터 처리
                ProcessClientData(client, data);
            }

            // 3. 연결 상태 확인 및 정리
            if (!client.IsConnected)
            {
                RemoveClient(client.ClientID);
            }
        }

        /// <summary>
        /// 클라이언트를 등록합니다.
        /// </summary>
        public bool RegisterClient(Client client)
        {
            if (client == null || client.EndPoint == null)
            {
                Debug.LogWarning("The client to be registered is invalid.");
                return false;
            }

            bool added = _clients.TryAdd(client.ClientID, client);
            if (added)
            {
                Debug.Log($"Client registered successfully: {client.ClientID} ({client.EndPoint}).");
            }
            else
            {
                Debug.LogWarning($"Client registration failed: {client.ClientID} ({client.EndPoint}).");
            }

            return added;
        }

        /// <summary>
        /// 클라이언트를 제거합니다.
        /// </summary>
        public bool RemoveClient(Guid clientID)
        {
            if (_clients.TryRemove(clientID, out var removedClient))
            {
                removedClient.Disconnect();
                Debug.Log($"Client removed successfully: {clientID}.");
                return true;
            }

            Debug.LogWarning($"Client removal failed: {clientID}.");
            return false;
        }

        /// <summary>
        /// 클라이언트 데이터를 처리합니다.
        /// </summary>
        private void ProcessClientData(Client client, byte[] data)
        {
            try
            {
                client.ReceiveBuffer.Read(data);
                Debug.Log($"Data received from client {client.ClientID}: {data.Length} bytes");

                // 데이터에 대한 추가 처리 로직 구현
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error processing data for client {client.ClientID}: {ex.Message}");
            }
        }

        /// <summary>
        /// IP 엔드포인트를 기반으로 클라이언트를 검색합니다.
        /// </summary>
        public Client GetClientByEndPoint(IPEndPoint endPoint)
        {
            return _clients.Values.FirstOrDefault(c => c.EndPoint.Equals(endPoint));
        }

        /// <summary>
        /// 연결이 끊어진 클라이언트를 정리합니다.
        /// </summary>
        public void CleanupDisconnectedClients()
        {
            var disconnectedClients = _clients.Values.Where(c => !c.IsConnected).ToList();
            foreach (var client in disconnectedClients)
            {
                RemoveClient(client.ClientID);
            }

            Debug.Log($"Number of cleaned up clients: {disconnectedClients.Count}.");
        }
    }
}

