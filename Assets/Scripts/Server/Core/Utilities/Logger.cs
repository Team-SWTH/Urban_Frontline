// ========================================
// File: Logger.cs
// Created: 2024-12-29 06:43:42
// Author: LHBM04
// ========================================

using System;
#if !UNITY_EDITOR
using System.Runtime.InteropServices;
#else
using UnityEngine;
#endif

namespace UrbanFrontline.Server.Core.Utilities
{
    /// <summary>
    /// 콘솔에 로그를 출력하는 정적 클래스.
    /// </summary>
    public static class Logger
    {
#if !UNITY_EDITOR
        /// <summary>
        /// 로그의 중요도 순위를 정의하는 열거형.
        /// </summary>
        private enum ELevel : byte
        {
            Log,
            Notice,
            Warning,
            Error,
            Assertion
        }

        #region External Functions
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetConsoleWindow();

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetConsoleOutputCP(uint wCodePageID);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetConsoleCP(uint wCodePageID);\
        #endregion

        static Logger()
        {
            if (GetConsoleWindow() == IntPtr.Zero)
            {
                AllocConsole();
            }

            /* UTF-8 Encoding */
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            SetConsoleOutputCP(65001);
            SetConsoleCP(65001);
            Console.Clear(); 
        }
#endif

        /// <summary>
        /// 콘솔에 로그를 출력합니다.
        /// </summary>
        /// <param name="message">출력할 로그.</param>
        public static void Log(string message)
        {
#if UNITY_EDITOR
            Debug.Log(message);
#else
            Log(ELevel.Log, message);
#endif
        }

        /// <summary>
        /// 콘솔에 알림 로그를 출력합니다.
        /// </summary>
        /// <param name="message">출력할 알림 로그.</param>
        public static void LogNotice(string message)
        {
#if UNITY_EDITOR
            Debug.Log(message);
#else
            Log(ELevel.Notice, message);
#endif
        }

        /// <summary>
        /// 콘솔에 경고 로그를 출력합니다.
        /// </summary>
        /// <param name="message">출력할 경고 로그.</param>
        public static void LogWarning(string message)
        {
#if UNITY_EDITOR
            Debug.LogWarning(message);
#else
            Log(ELevel.Warning, message);
#endif
        }

        /// <summary>
        /// 콘솔에 에러 로그를 출력합니다.
        /// </summary>
        /// <param name="message">출력할 에러 로그.</param>
        public static void LogError(string message)
        {
#if UNITY_EDITOR
            Debug.LogError(message);
#else
            Log(ELevel.Error, message);
#endif
        }

        /// <summary>
        /// 예외와 함께 콘솔에 에러 로그를 출력합니다.
        /// </summary>
        /// <param name="message">출력할 에러 로그.</param>
        /// <param name="exception">throw할 예외.</param>
        public static void LogException(string message, Exception exception)
        {
#if UNITY_EDITOR
            Debug.LogException(exception);
#else
            Log(ELevel.Assertion, $"{message} =>> {exception.Message}");
            throw exception;
#endif
        }

#if !UNITY_EDITOR
        private static string GetTimestamp(string format)
        {
            return DateTime.Now.ToString(format);
        }

        private static string GetConsoleColor(ELevel logLevel) =>
            logLevel switch
            {
                ELevel.Log => "\x1b[90m",       // Grey for Log
                ELevel.Notice => "\x1b[93m",    // Yellow for Notice
                ELevel.Warning => "\x1b[33m",   // Yellow for Warning
                ELevel.Error => "\x1b[31m",     // Red for Error
                ELevel.Assertion => "\x1b[31m", // Red for Assertion
                _ => throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null)
            };


        private static void Log(ELevel logLevel, string message)
        {
            Console.WriteLine($"<{GetTimestamp("yyyy/MM/dd HH:mm:ss")}>{GetConsoleColor(logLevel)}[{logLevel}]\x1b[0m {message}");
        }
#endif
    }
}