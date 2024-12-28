using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UrbanFrontline.Server.Core.Networks;

namespace UrbanFrontline.Client
{
    public class TestClient : MonoBehaviour
    {
        private const string ServerAddress = "127.0.0.1";
        private const int ServerPort = 8080;

        private Socket clientSocket;

        private ReceiveBuffer m_receiveBuffer;
        private SendBuffer m_sendBuffer;

        private void Start()
        {
            try
            {
                // Create a TCP client socket
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                Debug.Log($"Connecting to server at {ServerAddress}:{ServerPort}...");

                // Connect to the server
                clientSocket.Connect(new IPEndPoint(IPAddress.Parse(ServerAddress), ServerPort));
                Debug.Log("Connected to server.");
            }
            catch (SocketException ex)
            {
                Debug.LogError($"Socket error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error: {ex.Message}");
            }
        }

        private void Update()
        {
            if (clientSocket == null || !clientSocket.Connected)
                return;

            foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key))
                {
                    string message = key.ToString();
                    SendMessageToServer(message);
                }
            }
        }

        private void SendMessageToServer(string message)
        {
            try
            {
                // Send message to the server
                byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                clientSocket.Send(messageBytes);
                Debug.Log($"Sent to server: {message}");

                // Receive response from the server
                byte[] buffer = new byte[1024];
                int receivedBytes = clientSocket.Receive(buffer);
                string response = Encoding.UTF8.GetString(buffer, 0, receivedBytes);

                Debug.Log($"Server response: {response}");
            }
            catch (SocketException ex)
            {
                Debug.LogError($"Socket error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error: {ex.Message}");
            }
        }

        void OnApplicationQuit()
        {
            if (clientSocket != null && clientSocket.Connected)
            {
                clientSocket.Close();
                Debug.Log("Disconnected from server.");
            }
        }
    }
}