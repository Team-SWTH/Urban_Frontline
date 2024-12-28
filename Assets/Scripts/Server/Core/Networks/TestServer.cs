using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace UrbanFrontline.Server.Core.Networks
{
    /// <summary>
    /// 대충 테스트 해보자.
    /// [테스트 목록]
    /// - 클라이언트 접속.
    /// - 세션 생성.
    /// </summary>
    public class TestServer : ServerBase
    {
        protected override SocketType SocketType 
        {
            get { return SocketType.Stream; }
        }

        protected override ProtocolType ProtocolType
        {
            get { return ProtocolType.Tcp; }
        }

        protected override void Routine(CancellationToken token)
        {
            if (IsRunning && !token.IsCancellationRequested)
            {
                while (IsRunning && !token.IsCancellationRequested)
                {
                    try
                    {
                        Socket clientSocket = Socket.Accept();
                        Debug.Log($"Client connected: {clientSocket.RemoteEndPoint}");

                        Task.Run(() => HandleClient(clientSocket, token));
                    }
                    catch (Exception ex)
                    {
                        Debug.Log($"[Error] {ex.Message}");
                    }
                }
            }
        }

        private void HandleClient(Socket clientSocket, CancellationToken token)
        {
            var buffer = new byte[1024];

            try
            {
                while (IsRunning && !token.IsCancellationRequested)
                {
                    int received = clientSocket.Receive(buffer);
                    if (received > 0)
                    {
                        string message = Encoding.UTF8.GetString(buffer, 0, received);
                        Debug.Log($"Received: {message}");
                        clientSocket.Send(System.Text.Encoding.UTF8.GetBytes($"Echo: {message}"));
                    }
                }
            }
            catch (SocketException ex)
            {
                Debug.Log($"[Error] {ex.Message}");
            }
            finally
            {
                clientSocket.Close();
            }
        }
    }
}
