// ========================================
// File: CustomPhysicsCharacterController.cs
// Created: 2024-12-20 10:30:02
// Author: leeinhwan0421
// ========================================

using UnityEngine;

using R3;

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

        [Header("Physics Values")]
        /// <summary>
        /// 중력 가속도 가중치
        /// </summary>
        [Tooltip("중력 가속도 가중치")]
        [SerializeField]
        private float m_gravity = -9.8f;

        /// <summary>
        /// 공기 저항
        /// </summary>
        [Tooltip("공기 저항")]
        [SerializeField]
        private float m_airDrag = 0.001f;

        /// <summary>
        /// 현재 가속도
        /// </summary>
        [Tooltip("현재 가속도")]
        [SerializeField]
        private Vector3 m_velocity;

        [Space(10.0f)]
        [Header("Ground Values")]
        /// <summary>
        /// 현재 땅에 닿아 있는지
        /// </summary>
        [Tooltip("현재 땅에 닿아 있는지")]
        public bool IsGrounded;

        /// <summary>
        /// 땅 체크 길이
        /// </summary>
        [Tooltip("땅 체크 길이")]
        [SerializeField]
        private float m_rayDistance;

        [Tooltip("땅 레이어")]
        [SerializeField]
        private LayerMask m_layerMask;
        #endregion

        private void Start()
        {
            m_characterController = GetComponent<CharacterController>();

            Observable.EveryUpdate()
                      .Subscribe(_ => 
                      { 
                          HandlePhysics();
                          CheckOnGrounded();
                      }).AddTo(this);
        }

        /// <summary>
        /// 모든 물리 처리를 하는 코드
        /// </summary>
        private void HandlePhysics()
        {
            m_velocity += m_gravity * Time.deltaTime * Vector3.up;
            m_velocity += AddAirForce();

            Vector3 displacement = m_velocity * Time.deltaTime;
            m_characterController.Move(displacement);

            if (m_characterController.isGrounded && m_velocity.y < 0)
            {
                m_velocity.y = -0.01f;
            }
        }

        /// <summary>
        /// 공기 저항 값을 반환하는 함수
        /// </summary>
        private Vector3 AddAirForce()
        {
            float velocityMag = m_velocity.magnitude;
            Vector3 velocityDir = m_velocity.normalized;
            float airDragMag = m_airDrag * Mathf.Pow(velocityMag, 2f);

            return airDragMag * -velocityDir;
        }

        private void CheckOnGrounded()
        {
            RaycastHit hit;
            IsGrounded = UnityEngine.Physics.Raycast(transform.position, Vector3.down, out hit, m_rayDistance, m_layerMask);

#if UNITY_EDITOR
            Debug.DrawRay(transform.position, Vector3.down * m_rayDistance, Color.green);
#endif
        }

        public void AddVelocity(Vector3 velocity)
        {
            m_velocity += velocity;
        }

        public void Move(Vector3 moveVector)
        {
            m_characterController.Move(moveVector);
        }
    }
}
