// ========================================
// File: Player.cs
// Created: 2024-12-19 20:56:02
// Author: leeinhwan0421
// ========================================

using System;

using UnityEngine;

using UniRx;

using UrbanFrontline.Client.Core.Utilities;

namespace UrbanFrontline.Client.Core.Actor
{
    public class Player : MonoBehaviour
    {
        /// <summary>
        /// 그냥 단지 테스트 코드이므로 수정하지 마세요!! (병합 충돌의 위험)
        /// </summary>
        private void Start()
        {
            Observable.Interval(TimeSpan.FromSeconds(1))
                      .Subscribe(_ =>
                      {
                          Debug.Log($"Forward : {InputManager.GetKey(KeyAction.Forward)}\r\n" +
                                    $"Backward : {InputManager.GetKey(KeyAction.Backward)}\r\n" +
                                    $"Left : {InputManager.GetKey(KeyAction.Left)}\r\n" +
                                    $"Right : {InputManager.GetKey(KeyAction.Right)}\r\n");
                      })
                      .AddTo(this);
                
        }
    }
}
