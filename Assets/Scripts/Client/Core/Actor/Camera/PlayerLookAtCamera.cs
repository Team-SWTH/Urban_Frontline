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

using R3;

namespace UrbanFrontline.Client.Core.Actor.Camera
{
    public class PlayerLookAtCamera : MonoBehaviour, ICameraController
    {
        [Header("Components")]

        #region Components
        /// <summary>
        /// 카메라의 트랜스폼
        /// </summary>
        [Tooltip("카메라의 트랜스폼")]
        [SerializeField]
        private Transform m_cameraTransform;

        /// <summary>
        /// 시네머신 카메라 컴포넌트
        /// </summary>
        [Tooltip("시네머신 카메라 컴포넌트")]
        [SerializeField]
        private CinemachineCamera m_cinemachineCamera;

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
        #endregion

        [Space(10.0f)]
        [Header("FOV")]

        #region FOV
        /// <summary>
        /// FOV 값
        /// </summary>
        [Tooltip("FOV 값")]
        [SerializeField]
        private float m_fieldOfView = 75.0f;

        /// <summary>
        /// FOV 가중치
        /// </summary>
        [Tooltip("FOV 가중치")]
        [SerializeField]
        private float m_fieldOfViewWeight = 1.0f;

        /// <summary>
        /// FOV Lerp Speed
        /// </summary>
        [Tooltip("FOV Lerp Speed")]
        [SerializeField]
        private float m_fieldOfViewLerpSpeed = 0.3f;
        #endregion

        [Space(10.0f)]
        [Header("FreeLook")]

        #region FreeLook
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


        private void Awake()
        {
            SetFieldOfView(m_fieldOfView);
            SetWeight(m_fieldOfViewWeight);
        }

        /// <summary>
        /// FOV를 바꾸는 함수
        /// </summary>
        /// <param name="fieldOfViewWeight">가중치</param>
        public void SetWeight(float fieldOfViewWeight)
        {
            m_fieldOfViewWeight = fieldOfViewWeight;
        }

        /// <summary>
        /// 특정 FOV 값에 가중치를 곱해 보여줍니다.
        /// </summary>
        /// <param name="fieldOfView">원본 FOV 값</param>
        public void SetFieldOfView(float fieldOfView)
        {
            m_fieldOfView = fieldOfView;
        }

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

            float destFOV = m_fieldOfView * m_fieldOfViewWeight;
            float lerpFOV = Mathf.Lerp(m_cinemachineCamera.Lens.FieldOfView, destFOV, m_fieldOfViewLerpSpeed);
            m_cinemachineCamera.Lens.FieldOfView = lerpFOV;
        }
    }
}
