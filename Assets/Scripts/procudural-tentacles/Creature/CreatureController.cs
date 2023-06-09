using System;
using System.Collections;
using System.Collections.Generic;
using HypeFire.Library.Utilities.Extensions.Vector;
using HypeFire.Library.Utilities.Logger;
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
		private bool _isCanCreateLeg = false;
		private Transform _transform;
		public CreatureData creatureData { get; private set; } = new CreatureData();
		
		public List<GameObject> legPool { get; private set; } = new List<GameObject>();
		
		[field: SerializeField]
		public MinMaxFloat legDistance { get; private set; } = new MinMaxFloat(4.5f, 6.8f);
	
		private InputReader inputReader { get; set; } = new InputReader();
		
		private CharacterController characterController { get; set; } = new CharacterController();
		
		[field: SerializeField] public float heightFromGround { get; private set; } = 1f;
		[field: SerializeField] public float newLegCooldown { get; private set; } = .25f;
		[field: SerializeField] public float speed { get; private set; } = 5f;
		
		[field: SerializeField] public int deployedLegCount { get; private set; } = 0;
		[field: SerializeField] public int minAnchoredParts { get; private set; } = 2;
		[field: SerializeField] public int totalLegCount { get; private set; } = 0;
		
		[field: SerializeField] public Vector3 legLocation { get; private set; } = Vector3.zero;
		[field: SerializeField] public Vector3 legPlacementOrigin { get; private set; } = Vector3.zero;

		[field: SerializeField] public LayerMask moveAbleLayers { get; set; }

		private void Awake()
		{
			inputReader.SetInputController(new KeyboardController());
			characterController.SetController(
				new MovementControllerWithTransform(moveAbleLayers, 5f, heightFromGround));
			_transform = transform;
		}

		private void Start()
		{
			Reset();
			var t = Vector3.Angle(Vector3.forward,
				new Vector3(.25f,0f,1f));
			t.Log();
		}

		private void Update()
		{
			if(_isCanCreateLeg)
				return;
			
			characterController.Move(_transform, inputReader.ReadInput(), speed, out _);

			legPlacementOrigin = _transform.position +
			                     characterController.movementController.velocity.normalized *
			                     creatureData.legPlacementRadius;
			
			if(creatureData.maxLegCount <= totalLegCount)
				return;

			var offset = Random.insideUnitCircle * creatureData.legPlacementRadius;
			var newLegPosition = legPlacementOrigin + new Vector3(offset.x, 0f, offset.y);

			if (characterController.movementController.velocity.magnitude > 1f)
			{
				var newLegAngle = Vector3.Angle(characterController.movementController.velocity,
					newLegPosition - _transform.position);

				if (Mathf.Abs(newLegAngle) > 90)
				{
					newLegPosition = _transform.position - (newLegPosition - _transform.position);
				}
			}

			var distance = Vector3.Distance(
				new Vector3().ChangeToX(_transform.position.x).ChangeToZ(transform.position.z),
				new Vector3().ChangeToX(newLegPosition.x).ChangeToZ(newLegPosition.z));

			if (distance < legDistance.min)
			{
				
				newLegPosition = ((newLegPosition - _transform.position).normalized * legDistance.min) +
				                 _transform.position;
			}

		}

		public void TakeALeg()
		{
			
		}

		private void OnValidate()
		{
			Reset();
		}

		public void Reset()
		{
			legPool.ForEach((x) => { DestroyImmediate(x.gameObject); });

			totalLegCount = 0;
			deployedLegCount = 0;
			var randLocation = Random.insideUnitCircle;
			legLocation = new Vector3(randLocation.x, 0f, randLocation.y);
			minAnchoredParts = creatureData.minAnchoredLegs * minAnchoredParts;
			legDistance.Max = creatureData.legPlacementRadius * 2.2f;
		}

		public IEnumerator WaitToCreateLeg()
		{
			_isCanCreateLeg = false;
			yield return new WaitForSeconds(newLegCooldown);
			_isCanCreateLeg = true;
		}
	}
}