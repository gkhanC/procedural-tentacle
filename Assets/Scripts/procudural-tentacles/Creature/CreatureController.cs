using System;
using ProceduralTentacle.Input;
using ProceduralTentacle.Input.Abstract;
using ProceduralTentacle.Movement;
using UnityEngine;
using CharacterController = ProceduralTentacle.Movement.CharacterController;

namespace ProceduralTentacle.Creature
{
	public class CreatureController : MonoBehaviour
	{
		[field: SerializeField] public LayerMask moveAbleLayers { get; set; }
		[field: SerializeField] public float speed { get; private set; } = 5f;
		[field: SerializeField] public float heightFromGround { get; private set; } = 1f;

		private InputReader inputReader { get; set; } = new InputReader();
		private CharacterController characterController { get; set; } = new CharacterController();

		private void Awake()
		{
			inputReader.SetInputController(new KeyboardController());
			characterController.SetController(
				new MovementControllerWithTransform(moveAbleLayers, 5f, heightFromGround));
		}

		private void Update()
		{
			characterController.Move(transform, inputReader.ReadInput(), speed, out _);
		}
	}
}