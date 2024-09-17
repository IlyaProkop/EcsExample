using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMonoProvider : MonoProvider<JoystickProvider>
{
#if UNITY_EDITOR
	private void OnValidate()
	{
		//if (Value.Value == null)
		//{
		//	Value = new JoystickLink
		//	{
		//		Value = GetComponent<FloatingJoystick>()
		//	};
		//}
	}
#endif
}
