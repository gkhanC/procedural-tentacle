using System;

namespace HypeFire.Utilities.CustomStructures
{
	[Serializable]
	public struct MinMaxInt : IMinMaxValue<int>
	{
		public int min;
		public int max;
		
		public  int Min
		{
			get => min;
			set => min = value;
		}

		public int Max
		{
			get => max;
			set => max = value;
		}
	}
}