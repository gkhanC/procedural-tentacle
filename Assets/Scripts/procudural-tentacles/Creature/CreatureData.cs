using System;
using HypeFire.Utilities.CustomStructures;
using UnityEngine;

namespace ProceduralTentacle.Creature
{
	[Serializable]
	public class CreatureData
	{
		
		[field: SerializeField] public float legPlacementRadius { get; private set; } = 3f;
		[field: SerializeField] public float newLegDelay { get; set; } = 0.35f;

		[field: SerializeField] public int legResolution { get; private set; } = 40;
		[field: SerializeField] public int numberOfLeg { get; private set; } = 6;
		[field: SerializeField] public int minAnchoredLegs { get; private set; } = 2;
		[field: SerializeField] public int partsPerLeg { get; private set; } = 5;

		[field: SerializeField] public MinMaxFloat legLifeTime { get; private set; } = new MinMaxFloat(5f, 15f);
		[field: SerializeField]
		public MinMaxFloat legElongationSpeed { get; set; } = new MinMaxFloat(4.5f, 6.8f);
		
		public int maxLegCount => numberOfLeg * partsPerLeg;
		
		public GameObject legPrefab;
	}
}