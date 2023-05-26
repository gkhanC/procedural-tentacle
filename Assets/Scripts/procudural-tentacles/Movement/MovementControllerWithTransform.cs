using System;
using HypeFire.Library.Utilities.Extensions.Vector;
using HypeFire.Library.Utilities.Logger;
using UnityEngine;

namespace ProceduralTentacle.Movement
{
	[Serializable]
	public class MovementControllerWithTransform : MovementController
	{
		private RaycastHit _rayHit;
		private Vector3 _rayPivotOffset = new Vector3(0f, 3f, 0f);
		private LayerMask moveAbleLayers;
		private float rayDistance;
		private bool _isLayerChecked;

		private float _calculatedHeight = 0f;
		public float velocityLerpTime { get; set; }
		public Vector3 velocity { get; private set; } = Vector3.zero;
		[field: SerializeField] public float heightFromGround { get; protected set; } = 1f;

		public MovementControllerWithTransform(LayerMask moveAbleLayers, float velocityLerpTime = 5f,
			float height = 1f)
		{
			this.velocityLerpTime = velocityLerpTime;
			this.moveAbleLayers = moveAbleLayers;
			heightFromGround = height;
			rayDistance = (height * 3f) + 3f;
		}

		private void HeightAdjustment(Transform objectTransform)
		{
			if (!_isLayerChecked)
			{
				int layer = objectTransform.gameObject.layer;
				int layerMaskValue = 1 << layer;
				if ((moveAbleLayers.value & layerMaskValue) != 0)
				{
					objectTransform.gameObject.LogSuccess(
						"obje Move Able bir layer değerine sahip olduğu için Controller ile gelen LayerMask'ın değeri değiştirildi.");
					var mask = moveAbleLayers;
					mask = ~objectTransform.gameObject.layer;
					moveAbleLayers = mask;
				}

				_isLayerChecked = true;
			}

			_calculatedHeight = objectTransform.position.y;

			if (Physics.Raycast(objectTransform.position + _rayPivotOffset, Vector3.down, out _rayHit,
				    rayDistance, moveAbleLayers))
			{
				_calculatedHeight = _rayHit.point.y + heightFromGround;
			}

			objectTransform.position = Vector3.Lerp(objectTransform.position,
				objectTransform.position.ChangeToY(_calculatedHeight),
				velocityLerpTime * Time.deltaTime);
		}

		public override void Move(Transform objectTransform, Vector3 direction, float speed,
			out Vector3 currentVelocity)
		{
			velocity = Vector3.Lerp(velocity, direction * speed, velocityLerpTime * Time.deltaTime);
			currentVelocity = velocity;
			objectTransform.position = objectTransform.position + velocity * Time.deltaTime;
			HeightAdjustment(objectTransform);
		}
	}
}