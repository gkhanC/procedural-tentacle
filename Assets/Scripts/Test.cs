using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
	public Vector3 first = new Vector3(0, 0, 0), second = new Vector3(0, 0, 1), third = new Vector3(0, 0, 5);

	private void Update()
	{
		first = Vector3.Lerp(second, third, .04f);
	}
}