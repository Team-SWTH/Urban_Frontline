// ========================================
// File: PlayerLookAtCamera.cs
// Created: 2024-12-20 22:15:09
// Author: leeinhwan0421
// ========================================

using System.Threading;
using Cysharp.Threading.Tasks;

using UnityEngine;

using Unity.Cinemachine;
using System;

namespace UrbanFrontline.Client.Core.Actor.Camera
{
    public class PlayerLookAtCamera : MonoBehaviour, ICameraController
    {
        #region Fields

        [Header("Components")]

        /// <summary>
        /// 카메라의 트랜스폼
        /// </summary>
        [Tooltip("카메라의 트랜스폼")]
        [SerializeField]
        private Transform m_cameraTransform;

        /// <summary>
        /// 시네머신의 공전 컴포넌트
        /// </summary>
        [Tooltip("시네머신의 공전 컴포넌트")]
        [SerializeField]
        private CinemachineOrbitalFollow m_cinemachineOrbitalFollow;

        /// <summary>
        /// 시네머신의 회전 컴포넌트
        /// </summary>
        [Tooltip("시네머신의 회전 컴포넌트")]
        [SerializeField]
        private CinemachineInputAxisController m_cinemachineAxisController;

        [Space(10.0f)]
        [Header("Offsets")]

        /// <summary>
        /// FreeLook이 해제 될 경우 돌아갈 프리셋
        /// 기본 값으로 (0, 4)
        /// </summary>
        [Tooltip("FreeLook이 해제 될 경우 돌아갈 프리셋")]
        [SerializeField]
        private Vector2 m_desinationOrbitalOffset;

        /// <summary>
        /// FreeLook (자유 시점) 활성화 변수
        /// </summary>
        private bool m_isFreeLookEnabled = false;

        /// <summary>
        /// FreeLook 애니메이션 취소 함수
        /// </summary>
        private CancellationTokenSource m_cancelAnimation;

        /// <summary>
        /// FreeLook 애니메이션의 지속 시간
        /// </summary>
        [Tooltip("FreeLook 애니메이션의 지속 시간")]
        [SerializeField]
        private float m_cancelAnimationDuration = 0.1f;

        #endregion

        /// <summary>
        ///  FreeLook (자유 시점) 활성화 함수
        /// </summary>
        /// <param name="enable">자유시점 활성화 여부</param>
        public void EnableFreeLook(bool enable)
        {
            m_isFreeLookEnabled = enable;

            if (!enable)
            {
                m_isFreeLookEnabled = true;

                m_cancelAnimation?.Cancel();
                m_cancelAnimation?.Dispose();
                m_cancelAnimation = new CancellationTokenSource();

                RotatePlayerTowardsCameraAnimation().Forget();
            }
        }

        /// <summary>
        /// FreeLook 해제 활성화 함수
        /// </summary>
        /// <returns>void</returns>
        public async UniTaskVoid RotatePlayerTowardsCameraAnimation()
        {
            float initialX = m_cinemachineOrbitalFollow.HorizontalAxis.Value;
            float initialY = m_cinemachineOrbitalFollow.VerticalAxis.Value;

            float t = 0f;

            while (t < 1f)
            {
                t += Time.deltaTime / m_cancelAnimationDuration;

                m_cinemachineOrbitalFollow.HorizontalAxis.Value = Mathf.Lerp(initialX, m_desinationOrbitalOffset.x, t);
                m_cinemachineOrbitalFollow.VerticalAxis.Value = Mathf.Lerp(initialY, m_desinationOrbitalOffset.y, t);

                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken: m_cancelAnimation.Token);
            }

            m_isFreeLookEnabled = false;
        }

        /// <summary>
        /// 매 프레임 호출되는 카메라 업데이트 함수
        /// </summary>
        public void UpdateCamera()
        {
            if (!m_isFreeLookEnabled)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, m_cameraTransform.eulerAngles.y, transform.eulerAngles.z);
            }
        }
    }
}
