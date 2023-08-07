using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions {

	/// <summary>
	/// Call specified action in first instance of selected type in active scene.
	/// </summary>
	/// <returns>
	/// Information about whether the action was made.
	/// </returns>
	public static bool CallInInstance<T>(this MonoBehaviour obj, Action<T> action) where T : MonoBehaviour {
		var target = GameObject.FindObjectOfType<T>();
		if (target != null)
			action(target);
		return target != null;
	}

	/// <summary>
	/// Call specified action in all instances of selected type in active scene.
	/// </summary>
	/// <returns>
	/// Count of objects involved.
	/// </returns>
	public static int CallInAllInstances<T>(this MonoBehaviour obj, Action<T> action) where T : MonoBehaviour {
		int countObjectsInvolved = 0;
		var targets = GameObject.FindObjectsOfType<T>();
		foreach (var target in targets) {
			countObjectsInvolved++;
			action(target);
		}
		return countObjectsInvolved;
	}
}