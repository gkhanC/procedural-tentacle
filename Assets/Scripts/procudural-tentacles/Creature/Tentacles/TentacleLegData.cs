using System;
using HypeFire.Utilities.CustomStructures;
using UnityEngine;

namespace ProceduralTentacle.Creature.Tentacles
{
	[Serializable]
	public struct TentacleLegData
	{
		public bool isCanDie;
		public Vector3 footPosition;
		public float resolution;
		public float distance;
		public float lerpMultiplier;
		
		public float lifeTime;
		public int partCount;
		public MinMaxFloat partOffset;
		public float footDistance;
		public Vector3 rayPivotOffset;// = new Vector3(0f,3f,0f);
		public RangedFloat legHeight;
		public float rotationDirection;
		public RangedFloat rotationSpeed;
		public RangedFloat elongationSpeed;
		public bool isInfinity;
	}
}