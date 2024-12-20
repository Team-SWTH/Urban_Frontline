// ========================================
// File: PlayerLookAtCamera.cs
// Created: 2024-12-20 22:15:09
// Author: leeinhwan0421
// ========================================

using UnityEngine;

namespace UrbanFrontline.Client.Core.Actor.Camera
{
    /// <summary>
    /// 현재 단순히 코드를 짠 수준입니다. 추후 수정 예정
    /// </summary>
    public class PlayerLookAtCamera : MonoBehaviour
    {
        /// <summary>
        /// 카메라의 트랜스폼
        /// </summary>
        [Tooltip("카메라의 트랜스폼")]
        [SerializeField]
        private Transform m_cameraTransform;

        void Update()
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, m_cameraTransform.eulerAngles.y, transform.eulerAngles.z);
        }
    }
}
