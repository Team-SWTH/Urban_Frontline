// ========================================
// File: KeyAction.cs
// Created: 2024-12-19 21:16:44
// Author: leeinhwan0421
// ========================================

namespace UrbanFrontline.Client.Core.Input
{

    /// <summary>
    /// 바인딩 할 수 있는 키 액션 집합
    /// </summary>
    public enum KeyAction
    {
        // 움직임
        Forward,
        Left,
        Backward,
        Right,
        // 달리기
        Run,
        // 점프
        Jump,
        // 구르기
        Roll,
        // 자유 시점
        FreeLook,
        // 발사
        Fire,
        // 정조준
        ADS,
        Reload
    }
}
