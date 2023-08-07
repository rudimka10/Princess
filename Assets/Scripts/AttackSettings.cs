using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackSettings {

	public float cooldown = 1;

	public float delay = 0.4f;

	public int damage = 1;

	public float normalForce = 1;

	public BoxCollider2D attackZone;

	public string animatorKey = "attack";

	private float _timer;

	public bool IsReady => _timer <= 0;

	public void TickTimer(float deltaTime) {
		if (_timer > 0)
			_timer -= deltaTime;
	}

	public void SetupTimer() {
		_timer = cooldown;
	}

	public void ResetTimer() {
		_timer = 0;
	}

	public List<Health> FindAttackedTargets() {
		var targetsInRange = new List<Health>();

		foreach (var target in GameObject.FindObjectsOfType<Health>())
			if (attackZone.bounds.Contains(target.transform.position))
				targetsInRange.Add(target);

		return targetsInRange;
	}
}