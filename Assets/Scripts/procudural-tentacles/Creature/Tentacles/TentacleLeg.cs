using System.Collections.Generic;
using HypeFire.Library.Utilities.Extensions.Vector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ProceduralTentacle.Creature.Tentacles
{
	[RequireComponent(typeof(LineRenderer))]
	public class TentacleLeg : MonoBehaviour
	{
		private LineRenderer _lneRenderer;
		private CreatureController _creatureController;
		private TentacleLegData _data;

		private Vector3[] _parts;
		private Vector3[] _partOffsets;
		private Vector2 _footOffset;

		private bool _isCanDie = false;
		private bool _isDied = false;

		private float _legHeight = 0f;
		private float _rotationSpeed = 0f;
		private float _elongationSpeed = 0f;
		private float _elongationProgress = 0f;
		private float _rotationDirection = 0f;

		private float _growthTarget;
		
		public bool isInit { get; private set; }

		/// <summary>
		/// Initializes the tentacle leg.
		/// </summary>
		public void Initialize(CreatureController creature, TentacleLegData data)
		{
			_lneRenderer = GetComponent<LineRenderer>();
			_creatureController = creature;
			_data = data;

			_parts = new Vector3[data.partCount];
			_partOffsets = new Vector3[data.partCount - 2];
			InitializePartOffsets(ref _partOffsets);
			
			SetContactPoint();
			
			_legHeight = _data.legHeight.GetRandomValue();
			_rotationSpeed = _data.rotationSpeed.GetRandomValue();
			_rotationDirection = _data.rotationDirection;
			_elongationSpeed = _data.elongationSpeed.GetRandomValue();
			_elongationProgress = 0f;

			_growthTarget = 1f;
			_isCanDie = _data.isCanDie;
			_isDied = false;
			
			isInit = false;
		}

		/// <summary>
		/// Initializes the individual parts of the tentacle leg.
		/// </summary>
		public void InitializeParts()
		{
			//Start part at body Position
			_parts[0] = transform.position;

			_parts[^3] = (Vector3.up * .004f) + _data.footPosition;
			
			//Create a parabola
			_parts[2] = Vector3.Lerp(_parts[0] , _parts[6], .4f);
			_parts[2].y = _parts[0].y + _legHeight;
			
			_parts[1] = Vector3.Lerp(_parts[0], _parts[2], 0.5f);
			_parts[3] = Vector3.Lerp(_parts[2], _parts[6], 0.25f);
			_parts[4] = Vector3.Lerp(_parts[2], _parts[6], 0.5f);
			_parts[5] = Vector3.Lerp(_parts[2], _parts[6], 0.75f);

			RotatePartOffsets();
			
			_parts[1] += _partOffsets[0];
			_parts[2] += _partOffsets[1];
			_parts[3] += _partOffsets[2];
			_parts[4] += _partOffsets[3] / 2f;
			_parts[5] += _partOffsets[4] / 4f;

		}

		
		/// <summary>
		/// Initializes the offsets for individual parts of the tentacle leg.
		/// </summary>
		public void InitializePartOffsets(ref Vector3[] partOffsets)
		{
			for (int i = 0; i < partOffsets.Length; i++)
			{
				partOffsets[i] = (Random.onUnitSphere *
				                  Random.Range(_data.partOffset.min, _data.partOffset.max));
			}

			_footOffset = (Random.onUnitSphere * _data.footDistance);
		}

		/// <summary>
		/// Rotates the offsets of the tentacle leg parts.
		/// </summary>
		public void RotatePartOffsets()
		{
			_elongationSpeed += Time.deltaTime * _elongationProgress;
			if (_elongationProgress >= 360f)
			{
				_elongationProgress -= 360;
			}

			float newAngle = _rotationSpeed * Time.deltaTime *
				Mathf.Cos(_elongationProgress * Mathf.Deg2Rad) + 1;

			Vector3 axisRotation;
			for (int i = 1; i < 6; i++)
			{
				axisRotation = (_parts[i + 1] - _parts[i - 1]) / 2f;
				_partOffsets[i - 1] = Quaternion.AngleAxis(newAngle, _rotationDirection * axisRotation) * _partOffsets[i - 1];
			}
		}
		
		/// <summary>
		/// Returns points on a curve based on control points.
		/// </summary>
		public Vector3[] GetPointsOnCurve(Vector3[] controlPoints, int resolution, float t)
		{
			int segmentCount = resolution + 1;
			Vector3[] samplePoints = new Vector3[segmentCount];
			float segmentLength = 1f / resolution;

			for (int i = 0; i < segmentCount; i++)
			{
				float currentT = Mathf.Clamp01(i * segmentLength);
				samplePoints[i] = GetPointOnCurve(controlPoints, currentT);
			}

			return samplePoints;
		}
		
		/// <summary>
		/// Returns a point on a curve based on control points and a parameter t.
		/// </summary>
		public Vector3 GetPointOnCurve(Vector3[] controlPoints, float t)
		{
			int pointCount = controlPoints.Length;

			while (pointCount > 1)
			{
				for (int i = 0; i < pointCount - 1; i++)
				{
					controlPoints[i] = Vector3.Lerp(controlPoints[i], controlPoints[i + 1], t);
				}
				pointCount--;
			}
    
			return controlPoints[0];
		}

		/// <summary>
		/// Sets the contact point of the tentacle leg.
		/// </summary>
		public void SetContactPoint()
		{
			RaycastHit hit;
			var footOff = Vector3.zero;
			footOff.ChangeToX(_footOffset.x);
			footOff.ChangeToZ(_footOffset.y);
			Physics.Raycast(_data.footPosition + _data.rayPivotOffset + footOff, Vector3.down, out hit);
			_parts[^2] = hit.point;
		}
	}
}