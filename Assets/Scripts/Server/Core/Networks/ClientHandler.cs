// ========================================
// File: ClientManager.cs
// Created: 2024-12-20 03:02:31
// Author: LHBM04
// ========================================

using System;
using System.Collections.Generic;
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
        private List<Client> m_clients = new();

        /// <summary>
        /// 클라이언트를 추가합니다.
        /// </summary>
        /// <param name="client">추가할 새 클라이언트.</param>
        public void Add(Client client)
        {
            if (m_clients.Contains(client))
            {
                return;
            }

            m_clients.Add(client);
        }

        /// <summary>
        /// 특정 클라이언트를 삭제합니다.
        /// </summary>
        /// <param name="clientID">삭제할 클라이언트의 ID.</param>
        public void Remove(Guid clientID)
        {
            m_clients.RemoveAll(x => x.ID == clientID);
        }

        
    }
}