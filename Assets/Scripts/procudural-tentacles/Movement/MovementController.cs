using System;
using UnityEngine;

namespace ProceduralTentacle.Movement
{
	[Serializable]
	public abstract class MovementController
	{
		
		
		
		public abstract void Move(Transform objectTransform, Vector3 direction, float speed, out Vector3 currentVelocity);
	}
}