// ========================================
// File: SQLManager.cs
// Created: 2024-12-29 00:26:14
// Author: LHBM04
// ========================================

using System;
using System.Collections.Generic;
using System.Text;
using MySqlConnector;

namespace UrbanFrontline.Server.Core.Databases
{
    /// <summary>
    /// SQL 명령어.
    /// </summary>
    public static class SQLUtility
    {
        /// <summary>
        /// SQL 명령어를 실행합니다.
        /// </summary>
        /// <param name="connection">연결할 데이터베이스 연결 객체입니다.</param>
        /// <param name="command">실행할 SQL 명령어입니다.</param>
        /// <returns>영향을 받은 행의 수를 반환합니다.</returns>
        public static int ExecuteNonQuery(MySqlConnection connection, string command)
        {
            using var cmd = new MySqlCommand(command, connection);
            return cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// SQL 명령어를 실행합니다.
        /// </summary>
        /// <param name="connection">연결할 데이터베이스 연결 객체입니다.</param>
        /// <param name="command">실행할 SQL 명령어입니다.</param>
        /// <returns>영향을 받은 행의 수를 반환합니다.</returns>
        public static int ExecuteNonQuery(MySqlConnection connection, StringBuilder command)
        {
            using var cmd = new MySqlCommand(command.ToString(), connection);
            return cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// SQL 명령어를 실행합니다.
        /// </summary>
        /// <param name="connection">연결할 데이터베이스 연결 객체입니다.</param>
        /// <param name="command">실행할 SQL 명령어입니다.</param>
        /// <returns>영향을 받은 행의 수를 반환합니다.</returns>
        public static int ExecuteNonQuery(MySqlConnection connection, StringBuilder command, Dictionary<string, object> parameters)
        {
            using var cmd = new MySqlCommand(command.ToString(), connection);
            foreach (var parameter in parameters)
            {
                cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
            }
            return cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// SQL 명령어를 실행합니다.
        /// </summary>
        /// <param name="connection">연결할 데이터베이스 연결 객체입니다.</param>
        /// <param name="command">실행할 SQL 명령어입니다.</param>
        /// <returns>영향을 받은 행의 수를 반환합니다.</returns>
        public static int ExecuteNonQuery(MySqlConnection connection, string command, Dictionary<string, object> parameters)
        {
            using var cmd = new MySqlCommand(command, connection);
            foreach (var parameter in parameters)
            {
                cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
            }
            return cmd.ExecuteNonQuery();
        }
    }
}
