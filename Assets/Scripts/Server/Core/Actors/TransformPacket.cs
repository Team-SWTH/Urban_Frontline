// ========================================
// File: TransformPacket.cs
// Created: 2024-12-20 16:34:01
// Author: ※ 작성자 이름을 반드시 기입해주세요.
// ========================================

using System.IO;
using UnityEngine;

namespace UrbanFrontline.Server.Core.Actors
{
    public class TransformPacket : Packet
    {
        public override EType Type => EType.Transform;

        protected override void WriteData(BinaryWriter writer)
        {
            throw new System.NotImplementedException();
        }
    }
}