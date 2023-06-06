using System;
using System.Collections.Generic;
using HypeFire.Utilities.CustomStructures;
using ProceduralTentacle.Input;
using ProceduralTentacle.Input.Abstract;
using ProceduralTentacle.Movement;
using UnityEngine;
using CharacterController = ProceduralTentacle.Movement.CharacterController;
using Random = UnityEngine.Random;

namespace ProceduralTentacle.Creature
{
	public class CreatureController : MonoBehaviour
	{
		[field: SerializeField] public LayerMask moveAbleLayers { get; set; }
		[field: SerializeField] public float speed { get; private set; } = 5f;
		[field: SerializeField] public float heightFromGround { get; private set; } = 1f;

		private InputReader inputReader { get; set; } = new InputReader();
		private CharacterController characterController { get; set; } = new CharacterController();

		public CreatureData creatureData { get; private set; } = new CreatureData();

		[field: SerializeField] public int totalLegCount { get; private set; } = 0;
		[field: SerializeField] public int deployedLegCount { get; private set; } = 0;
		[field: SerializeField] public Vector3 legPlacementOrigin { get; private set; } = Vector3.zero;
		[field: SerializeField] public int minAnchoredParts { get; private set; } = 2;

		[field: SerializeField]
		public MinMaxFloat legDistance { get; private set; } = new MinMaxFloat(4.5f, 6.8f);

		private bool _isCanCreateLeg = false;
		public List<GameObject> legPool { get; private set; } = new List<GameObject>();
		[field: SerializeField] public Vector3 legLocation;

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

		public void Reset()
		{
			legPool.ForEach((x) => { DestroyImmediate(x.gameObject); });

			totalLegCount = 0;
			deployedLegCount = 0;
			var rotation = 360f / creatureData.maxLegCount;
			var randLocation = Random.insideUnitCircle;
			legLocation = new Vector3(randLocation.x, 0f, randLocation.y);
			minAnchoredParts = creatureData.minAnchoredLegs * minAnchoredParts;
			legDistance.Max = creatureData.legPlacementRadius * 2.2f;
		}
	}
}