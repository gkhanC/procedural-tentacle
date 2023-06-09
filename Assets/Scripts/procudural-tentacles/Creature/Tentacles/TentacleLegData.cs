using System;
using HypeFire.Utilities.CustomStructures;
using UnityEngine;

namespace ProceduralTentacle.Creature.Tentacles
{
	[Serializable]
	public struct TentacleLegData
	{
		public bool isCanDie;
		public bool isInfinity;
		
		public float distance;
		public float lifeTime;
		public float lerpMultiplier;
		public float footDistance;
		public float rotationDirection;
		
		public int resolution;
		public int partCount;
		
		public MinMaxFloat partOffset;
		
		public RangedFloat rotationSpeed;
		public RangedFloat legHeight;
		public RangedFloat elongationSpeed;
		
		public Vector3 footPosition;
		public Vector3 rayPivotOffset;// = new Vector3(0f,3f,0f);
		
	}
}