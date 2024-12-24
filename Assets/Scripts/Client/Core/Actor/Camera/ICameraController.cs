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
        /// FreeLook (자유 시점) 활성화 함수
        /// </summary>
        /// <param name="enable">자유시점 활성화 여부</param>
        void EnableFreeLook(bool enable);

        /// <summary>
        /// FreeLook (자유 시점)이 해제될 때 부드럽게 돌아오도록 하는 비동기 함수
        /// </summary>
        /// <returns>Void</returns>
        UniTaskVoid RotatePlayerTowardsCameraAnimation();

        /// <summary>
        /// 매 프레임 호출되는 카메라 업데이트 함수
        /// </summary>
        void UpdateCamera();
    }
}
