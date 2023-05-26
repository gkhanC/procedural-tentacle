using System;
using UnityEngine;
using ProceduralTentacle.Movement.Abstract;

namespace ProceduralTentacle.Movement
{
	[Serializable]
	public class CharacterController : ICharacterController
	{
		[field: SerializeField] public MovementController movementController { get; private set; }

		public void SetController(MovementController controller)
		{
			movementController = controller;
		}

		public void Move(Transform objectTransform, Vector3 direction, float speed,
			out Vector3 currentVelocity) =>
			movementController.Move(objectTransform, direction, speed, out currentVelocity);
	}
}