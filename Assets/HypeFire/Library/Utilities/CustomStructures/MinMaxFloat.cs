using System;

namespace HypeFire.Utilities.CustomStructures
{
	[Serializable]
	public struct MinMaxFloat :IMinMaxValue<float>
	{
		public float min;
		public float max;
		
		public  float Min
		{
			get => min;
			set => min = value;
		}

		public float Max
		{
			get => max;
			set => max = value;
		}

		public MinMaxFloat(float min, float max)
		{
			this.min = min;
			this.max = min;
		}
		
		public MinMaxFloat(int min, int max)
		{
			this.min = (float)min;
			this.max = (float)min;
		}
	}
}