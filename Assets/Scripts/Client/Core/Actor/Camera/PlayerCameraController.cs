// ========================================
// File: PlayerLookAtCamera.cs
// Created: 2024-12-20 22:15:09
// Author: leeinhwan0421
// ========================================

using System;
using System.Threading;
using System.Collections.Generic;

using UnityEngine;

using Unity.Cinemachine;
using Cysharp.Threading.Tasks;

namespace UrbanFrontline.Client.Core.Actor.Camera
{
    public class PlayerCameraController : MonoBehaviour, ICameraController
    {
        [Header("Components")]

        #region Components
        /// <summary>
        /// 카메라의 Transform
        /// </summary>
        [Tooltip("카메라의 Transform")]
        [SerializeField]
        private Transform m_cameraTransform;

        /// <summary>
        /// 카메라 팔로우의 Transform
        /// </summary>
        [Tooltip("카메라 팔로우의 Transform")]
        [SerializeField]
        private Transform m_cameraFollowTransform;

        /// <summary>
        /// 시네머신 카메라 컴포넌트
        /// </summary>
        [Tooltip("시네머신 카메라 컴포넌트")]
        [SerializeField]
        private CinemachineCamera m_cinemachineCamera;

        [Space(5.0f)]

        /// <summary>
        /// 오른손의 Transform
        /// </summary>
        [Tooltip("오른손의 Transform")]
        [SerializeField]
        private Transform m_rightHandTransform;

        #endregion

        [Space(10.0f)]
        [Header("FOV Settings")]

        #region FOV Settings
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
        [Header("Input Settings")]

        #region Input Settings

        /// <summary>
        /// 마우스 X축 입력
        /// </summary>
        [Tooltip("마우스 X축 입력")]
        [SerializeField]
        private string m_xAxisInput = "Mouse X";

        /// <summary>
        /// 마우스 Y축 입력
        /// </summary>
        [Tooltip("마우스 Y축 입력")]
        [SerializeField]
        private string m_yAxisInput = "Mouse Y";

        /// <summary>
        /// 회전 감도
        /// </summary>
        [Tooltip("sensitivity")]
        [SerializeField]
        private float m_sensitivity = 200;
        private float m_sensitivityMultipler = 1.0f;

        /// <summary>
        /// 좌우 회전 값
        /// </summary>
        private float m_mouseX;

        /// <summary>
        /// 상하 회전 값
        /// </summary>
        private float m_mouseY;

        /// <summary>
        /// 최대 상 회전 값
        /// </summary>
        [Tooltip("최대 상 회전 값")]
        [SerializeField]
        private float m_maxMouseY = 70.0f;

        /// <summary>
        /// 최대 하 회전 값
        /// </summary>
        [Tooltip("최대 하 회전 값")]
        [SerializeField]
        private float m_minMouseY = -70.0f;
        #endregion

        [Space(10.0f)]
        [Header("FreeLook Settings")]

        #region FreeLook Settings
        /// <summary>
        /// 둘러보기 활성화 값
        /// <summary>
        [Tooltip("둘러보기 활성화 값")]
        [SerializeField]
        private bool m_freelookEnable;

        /// <summary>
        /// 둘러보기 비활성화 시 돌아갈 속도
        /// </summary>
        [Tooltip("둘러보기 비활성화 시 돌아갈 속도")]
        [SerializeField]
        private float m_freelookLerpSpeed = 5.0f;

        /// <summary>
        /// 둘러보기 비활성화 시 돌아갈 위치
        /// </summary>
        [Tooltip("둘러보기 비활성화 시 돌아갈 위치")]
        [SerializeField]
        private float m_beforeMouseX;
        private float m_beforeMouseY;
        #endregion

        private void Awake()
        {
            SetFieldOfView(m_fieldOfView);
            SetWeight(m_fieldOfViewWeight);
        }

        private void Update()
        {
            m_mouseX += UnityEngine.Input.GetAxis(m_xAxisInput) * m_sensitivity * m_sensitivityMultipler * Time.deltaTime;
            m_mouseY += UnityEngine.Input.GetAxis(m_yAxisInput) * m_sensitivity * m_sensitivityMultipler * Time.deltaTime;
            m_mouseY = Mathf.Clamp(m_mouseY, m_minMouseY, m_maxMouseY);

            Quaternion result = Quaternion.Euler(new Vector3(-m_mouseY, m_mouseX, 0));

            if (m_freelookEnable == false)
            {
                m_cameraFollowTransform.rotation = Quaternion.Euler(result.eulerAngles.x, m_cameraFollowTransform.rotation.eulerAngles.y, result.eulerAngles.z);
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, result.eulerAngles.y, transform.rotation.eulerAngles.z);
                m_rightHandTransform.rotation = Quaternion.LookRotation(m_cameraTransform.forward) * Quaternion.Euler(0, -90, 0);
            }
            else
            {
                m_cameraFollowTransform.rotation = Quaternion.Euler(result.eulerAngles.x, result.eulerAngles.y, result.eulerAngles.z);
            }
        }

        private void LateUpdate()
        {
            float destFOV = m_fieldOfView * m_fieldOfViewWeight;
            float lerpFOV = Mathf.Lerp(m_cinemachineCamera.Lens.FieldOfView, destFOV, m_fieldOfViewLerpSpeed);
            m_cinemachineCamera.Lens.FieldOfView = lerpFOV;
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
        /// 둘러보기 감도(곱연산 값) 조정
        /// </summary>
        /// <param name="multipler">변경할 값</param>
        public void SetSensitivityMultipler(float multipler)
        {
            m_sensitivityMultipler = multipler;
        }

        /// <summary>
        /// FreeLook 상태 변환
        /// </summary>
        /// <param name="enable">freelook enable : true, disable : false</param>
        public void SetFreeLookStatement(bool enable)
        {
            if (m_freelookEnable == false && enable == true)
            {
                m_beforeMouseX = m_mouseX;
                m_beforeMouseY = m_mouseY;

                m_freelookEnable = true;

                return;
            }
            
            if (m_freelookEnable == true && enable == false)
            {
                OffFreeLookAnimation().Forget();

                return;
            }
        }

        /// <summary>
        /// FreeLook 비활성화 시 애니메이션
        /// </summary>
        /// <returns>void</returns>
        private async UniTaskVoid OffFreeLookAnimation()
        {
            float beforeSensevity = m_sensitivity;
            m_sensitivity = 0;

            float t = 0.0f;

            while (t < 1.0f)
            {
                t += Time.deltaTime * m_freelookLerpSpeed;

                m_mouseX = Mathf.Lerp(m_mouseX, m_beforeMouseX, t);
                m_mouseY = Mathf.Lerp(m_mouseY, m_beforeMouseY, t);

                await UniTask.WaitForEndOfFrame();
            }

            m_mouseX = m_beforeMouseX;
            m_mouseY = m_beforeMouseY;

            m_sensitivity = beforeSensevity;

            m_freelookEnable = false;
        }
    }
}
