// ========================================
// File: PlayerStateBase.cs
// Created: 2024-12-20 12:42:08
// Author: leeinhwan0421
// ========================================

namespace UrbanFrontline.Client.Core.Actor.State.Base
{
    /// <summary>
    /// 기본 구현은 비어 놓았습니다. (추가 될 수 있습니다.)
    /// </summary>
    public abstract class PlayerStateBase : IPlayerState
    {
        public bool IsEnable;

        public virtual void Enter() 
        {
            IsEnable = true;
        }

        public virtual void Exit() 
        {
            IsEnable = false;
        }

        public virtual void UpdateState() { }
    }
}

