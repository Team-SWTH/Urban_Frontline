// ========================================
// File: PlayerMovement.cs
// Created: 2024-12-20 16:56:55
// Author: leeinhwan0421
// ========================================

using UnityEngine;
using UrbanFrontline.Client.Core.Physics;

namespace UrbanFrontline.Client.Core.Actor.Movement
{
    [RequireComponent(typeof(CustomPhysicsCharacterController))]
    public class PlayerMovement : MonoBehaviour, ICharacterMovement
    {
        #region Fields
        /// <summary>
        /// 플레이어의 걷는 속도
        /// </summary>
        [Tooltip("플레이어의 걷는 속도")]
        [SerializeField]
        private float m_walkSpeed = 5.0f;

        /// <summary>
        /// 플레이어의 뛰는 속도
        /// </summary>
        [Tooltip("플레이어의 뛰는 속도")]
        [SerializeField]
        private float m_runSpeed = 10.0f;

        /// <summary>
        /// 플레이어의 공중 속도
        /// </summary>
        [Tooltip("플레이어의 공중 속도")]
        [SerializeField]
        private float m_airSpeed = 2.5f;

        /// <summary>
        /// 플레이어의 점프 높이
        /// </summary>
        [Tooltip("플레이어의 점프 높이")]
        [SerializeField]
        private float m_jumpHeight = 5.0f;

        /// <summary>
        /// 참조할 커스텀 캐릭터 컨트롤러
        /// </summary>
        private CustomPhysicsCharacterController m_controller;
        #endregion

        private void Awake()
        {
            m_controller = GetComponent<CustomPhysicsCharacterController>();
        }

        /// <summary>
        /// 걷는 로직
        /// </summary>
        public void Walk(Vector2 direction)
        {
            Vector3 moveDir = new Vector3(direction.x, 0, direction.y).normalized;
            Vector3 forwardMoveDir = transform.TransformDirection(moveDir.normalized);

            if (m_controller.IsGrounded)
            {
                m_controller.Move(m_walkSpeed * Time.deltaTime * forwardMoveDir);
            }
            else
            {
                m_controller.Move(m_airSpeed * Time.deltaTime * forwardMoveDir);
            }
        }

        /// <summary>
        /// 뛰는 로직
        /// </summary>
        public void Run(Vector2 direction)
        {
            Vector3 moveDir = new Vector3(direction.x, 0, direction.y).normalized;
            Vector3 forwardMoveDir = transform.TransformDirection(moveDir.normalized);

            if (m_controller.IsGrounded)
            {
                m_controller.Move(m_runSpeed * Time.deltaTime * forwardMoveDir);
            }
            else
            {
                m_controller.Move(m_airSpeed * Time.deltaTime * forwardMoveDir);
            }
        }

        /// <summary>
        /// 점프 로직
        /// </summary>
        public void Jump()
        {
            m_controller.AddVelocity(new Vector3(0.0f, m_jumpHeight, 0.0f));
        }


        /// <summary>
        /// 구르기 로직
        /// </summary>
        public void Roll()
        {

        }

        /// <summary>
        /// 현재 땅인지를 체크하는 기능
        /// </summary>
        /// <returns>현재 땅에 닿아있다면 true, 아니라면 false</returns
        public bool IsGrounded()
        {
            return m_controller.IsGrounded;
        }
    }
}

