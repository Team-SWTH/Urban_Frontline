// ========================================
// File: IInputProvider.cs
// Created: 2024-12-20 13:24:54
// Author: leeinhwan0421
// ========================================

using UnityEngine;

using R3;
using System;

namespace UrbanFrontline.Client.Core.Input
{
    /// <summary>
    /// 입력 기능을 여기에 하나씩 추가하시면 됩니다.
    /// </summary>
    public interface IInputProvider
    {
        /// <summary>
        /// 상하좌우 입력 이벤트 스트림
        /// </summary>
        Observable<Vector2> MoveInput { get; }

        /// <summary>
        /// 달리기 입력 이벤트 스트림
        /// 키가 눌릴 경우 OnNext(Unit.Default 발생)
        /// </summary>
        Observable<bool> RunInput { get; }

        /// <summary>
        /// 점프 입력 이벤트 스트림
        /// 키가 눌릴 경우 OnNext(Unit.Default 발생)
        /// </summary>
        Observable<bool> JumpInput { get; }

        /// <summary>
        /// 구르기 입력 이벤트 스트림
        /// 키가 눌릴 경우 OnNext(Unit.Default 발생)
        /// </summary>
        Observable<bool> RollInput { get; }

        /// <summary>
        /// 자유시점 입력 이벤트 스트림
        /// </summary>
        ReactiveProperty<bool> FreeLookInput { get; }

        /// <summary>
        /// 발사 입력 이벤트 스트림
        /// </summary>
        Observable<bool> FireInput { get; } 

        /// <summary>
        /// 조준 입력 이벤트 스트림
        /// </summary>
        Observable<bool> ADSInput { get; }

        /// <summary>
        /// 장전 입력 이벤트 스트림
        /// </summary>
        Observable<bool> ReloadInput { get; }
    }
}
