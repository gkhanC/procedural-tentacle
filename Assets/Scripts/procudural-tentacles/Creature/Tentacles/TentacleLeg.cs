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

		private Vector3[] parts;
		private Vector3[] partOffsets;
		public bool isInit { get; private set; }

		public void Initialize(CreatureController creature, TentacleLegData data)
		{
			_lneRenderer = GetComponent<LineRenderer>();
			_creatureController = creature;
			_data = data;

			parts = new Vector3[data.partCount];
			partOffsets = new Vector3[data.partCount - 2];
			PartOffsetInitial(ref partOffsets);

			isInit = false;
		}

		public void PartOffsetInitial(ref Vector3[] partOffsets)
		{
			for (int i = 0; i < partOffsets.Length; i++)
			{
				partOffsets[i] = (Random.onUnitSphere *
				                  Random.Range(_data.partOffset.min, _data.partOffset.max));
			}
		}
	}
}