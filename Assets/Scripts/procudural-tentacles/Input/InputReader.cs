using System;
using HypeFire.Library.Utilities.Extensions.Object;
using ProceduralTentacle.Input.Abstract;
using UnityEngine;

namespace ProceduralTentacle.Input
{
	[Serializable]
	public class InputReader : IInputReader
	{
		private IInputController _controller;

		public void SetInputController(IInputController controller)
		{
			_controller = controller;
		}

		public Vector3 ReadInput()
		{
			return _controller.IsNull() ? Vector3.zero : _controller.GetInputData();
		}
	}
}