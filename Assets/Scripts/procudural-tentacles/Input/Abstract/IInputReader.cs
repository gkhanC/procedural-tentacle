using UnityEngine;

namespace ProceduralTentacle.Input.Abstract
{
	public interface IInputReader
	{
		public void SetInputController(IInputController controller);
		public Vector3 ReadInput();
	}
}