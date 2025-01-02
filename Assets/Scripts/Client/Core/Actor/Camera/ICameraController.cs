// ========================================
// File: ICameraController.cs
// Created: 2024-12-21 15:22:25
// Author: leeinhwan0421
// ========================================

using UnityEngine;

using Cysharp.Threading.Tasks;

namespace UrbanFrontline.Client.Core.Actor.Camera
{
    public interface ICameraController
    {
        /// <summary>
        /// 특정 FOV 값에 가중치를 곱해 보여줍니다.
        /// </summary>
        /// <param name="fieldOfViewWeight">가중치</param>
        void SetWeight(float fieldOfViewWeight);

        /// <summary>
        /// FOV를 바꾸는 함수
        /// </summary>
        /// <param name="fieldOfView">바꿀 FOV 값</param>
        void SetFieldOfView(float fieldOfView);

        /// <summary>
        /// 둘러보기 감도(곱연산 값) 조정
        /// </summary>
        /// <param name="multipler">변경할 값</param>
        void SetSensitivityMultipler(float multipler);

        /// <summary>
        /// FreeLook 상태 변환
        /// </summary>
        /// <param name="enable">freelook enable : true, disable : false</param>
        void SetFreeLookStatement(bool enable);
    }
}
