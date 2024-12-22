// ========================================
// File: ServerHandler.cs
// Created: 2024-12-20 20:50:31
// Author: LHBM04
// ========================================

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UrbanFrontline.Server.Core.Utilities;

namespace UrbanFrontline.Server.Core.Networks
{
    /// <summary>
    /// UDP 방식의 서버를 관리합니다.
    /// </summary>
    [Serializable]
    public sealed class ServerHandler
    {
        private static readonly int Port = 11000;

        public async Task StartServerAsync()
        {
            using (UdpClient udpServer = new UdpClient(Port))
            {
                Debug.Log($"UDP 서버 시작, 포트: {Port}");
                while (true)
                {
                    try
                    {
                        // 비동기적으로 데이터 수신
                        var result = await udpServer.ReceiveAsync();
                        string receivedData = Encoding.UTF8.GetString(result.Buffer);
                        Debug.Log($"[{result.RemoteEndPoint}]로부터 수신한 데이터: {receivedData}");

                        // 비동기적으로 응답
                        string response = $"서버 응답: {receivedData}";
                        byte[] responseData = Encoding.UTF8.GetBytes(response);
                        await udpServer.SendAsync(responseData, responseData.Length, result.RemoteEndPoint);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"오류 발생: {ex.Message}");
                        udpServer.Close();
                    }
                }
            }
        }
    }
}
