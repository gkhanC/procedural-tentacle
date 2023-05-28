using System;
using HypeFire.Utilities.CustomStructures;
using UnityEngine;

namespace ProceduralTentacle.Creature.Tentacles
{
	[Serializable]
	public struct TentacleLegData
	{
		public Vector3 footPosition;
		public float resolution;
		public float distance;
		public float lerpMultiplier;
		public float lifeTime;
		public int partCount;
		public MinMaxFloat partOffset;
		public bool isInfinity;
	}
}