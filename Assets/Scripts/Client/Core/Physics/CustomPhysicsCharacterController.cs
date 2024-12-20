// ========================================
// File: CustomPhysicsCharacterController.cs
// Created: 2024-12-20 10:30:02
// Author: leeinhwan0421
// ========================================

using UnityEngine;

using UniRx;

namespace UrbanFrontline.Client.Core.Physics
{
    [RequireComponent(typeof(CharacterController))]
    public class CustomPhysicsCharacterController : MonoBehaviour
    {
        #region Fields
        /// <summary>
        /// 캐릭터 컨트롤러
        /// </summary>
        private CharacterController m_characterController;

        public CharacterController CharacterController => m_characterController;

        /// <summary>
        /// 중력 가속도 가중치
        /// </summary>
        [Tooltip("중력 가속도 가중치")]
        public float Gravity = -9.8f;

        /// <summary>
        /// 공기 저항
        /// </summary>
        [Tooltip("공기 저항")]
        public float AirDrag = 0.001f;

        /// <summary>
        /// 현재 가속도
        /// </summary>
        [Tooltip("현재 가속도")]
        public Vector3 Velocity;

        #endregion

        private void Start()
        {
            m_characterController = GetComponent<CharacterController>();

            Observable.EveryFixedUpdate()
                      .Subscribe(_ => 
                      { 
                          HandlePhysics(); 
                      }).AddTo(this);
        }

        /// <summary>
        /// 모든 물리 처리를 하는 코드
        /// </summary>
        private void HandlePhysics()
        {
            Velocity += Gravity * Time.fixedDeltaTime * Vector3.up;
            Velocity += AddAirForce();

            Vector3 displacement = Velocity * Time.fixedDeltaTime;
            m_characterController.Move(displacement);

            if (m_characterController.isGrounded && Velocity.y < 0)
            {
                Velocity.y = 0;
            }
        }

        /// <summary>
        /// 공기 저항 값을 반환하는 함수
        /// </summary>
        private Vector3 AddAirForce()
        {
            float velocityMag = Velocity.magnitude;
            Vector3 velocityDir = Velocity.normalized;
            float airDragMag = AirDrag * Mathf.Pow(velocityMag, 2f);

            return airDragMag * -velocityDir;
        }
    }
}
