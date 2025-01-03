// ========================================
// File: Listener.cs
// Created: 2025-01-04 01:54:10
// Author: LHBM04
// ========================================

using System.Net.Sockets;
using System.Net;
using System;
using UnityEngine;

namespace UrbanFrontline.Server.Core.Networks
{
    public class Listener : MonoBehaviour
    {
        Socket m_listenSocket;
        Func<SessionBase> m_sessionFactory;

        private void Start()
        {
            Init(new IPEndPoint(IPAddress.Any, 10200), () => { return new ClientSession(); });
        }

        public void Init(IPEndPoint endPoint, Func<SessionBase> sessionFactory, int register = 10, int backlog = 100)
        {
            // 1. 클라이언트의 접속을 받아들이기 위한 객체 생성 및 초기화
            m_listenSocket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // 2. 서버 정보를 소켓에 bind
            m_listenSocket.Bind(endPoint);

            // 3. 소켓을 수신 상태로 둡니다.
            // 보류 중인 연결 큐의 최대 길이를 매개변수로 입력합니다.
            m_listenSocket.Listen(backlog);

            // 4. register의 수 만큼 소켓 Accept를 비동기로 처리한다.
            for (int i = 0; i < register; i++)
            {
                SocketAsyncEventArgs args = new SocketAsyncEventArgs();

                // 이벤트 핸들러 작업이 완료되면 이 곳에 추가된 함수가 실행된다.
                // 추가된 함수 => OnAcceptCompleted
                args.Completed += new EventHandler<SocketAsyncEventArgs>(OnAcceptCompleted);
                RegisterAccept(args);
            }
        }

        // AcceptAsync를 사용해 비동기로 처리
        // SocketAsyncEventArgs를 사용해 AcceptAsync 비동기 성공과 실패에 대한 처리를 합니다.
        private void RegisterAccept(SocketAsyncEventArgs args)
        {
            // Event를 재사용하기 위해 기존에 있던 것을 null 처리
            args.AcceptSocket = null;

            // 작업이 보류 되었는지 완료되었는지를 bool 값(pending)으로 return 해줍니다.
            // 바로 처리를 했는지 아니면 못했는지를 확인합니다.
            bool pending = m_listenSocket.AcceptAsync(args);

            // 바로 처리가 되었을 경우(false)
            if (pending == false)
                OnAcceptCompleted(null, args);
        }

        // SocketAsyncEventArgs를 사용해 성공 실패 여부 체크 후 세션 시작 및 연결
        private void OnAcceptCompleted(object sender, SocketAsyncEventArgs args)
        {
            if (args.SocketError == SocketError.Success)
            {
                SessionBase newSession = m_sessionFactory.Invoke();
                newSession.Connect(args.AcceptSocket);
            }
            else
                Console.WriteLine(args.SocketError.ToString());

            RegisterAccept(args);
        }
    }
}