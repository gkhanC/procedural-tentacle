using UnityEngine;

namespace ProceduralTentacle.Input.Abstract
{
	public class KeyboardController : IInputController
	{
		public Vector3 GetInputData()
		{
			return new Vector3(UnityEngine.Input.GetAxis("Horizontal"), 0f,
				UnityEngine.Input.GetAxis("Vertical"));
		}
	}
}