// ========================================
// File: TransformPacket.cs
// Created: 2024-12-20 16:34:01
// Author: LHBM04
// ========================================

using System.IO;
using UnityEngine;

namespace UrbanFrontline.Server.Core.Actors
{
    public class TransformPacket : Packet
    {
        public override EType Type => EType.Transform;

        public override byte[] Serialize()
        {
            throw new System.NotImplementedException();
        }

        public override void Deserialize(byte[] data)
        {
            throw new System.NotImplementedException();
        }
    }
}