using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Skill : MonoBehaviour {

	public float cooldown = 1;

	public float delay = 0.4f;

	public int damage = 1;

	public float normalForce = 1;

	public string animatorKey = "attack";

	public Transform aimPoint;

	public float range = 1;

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

	public List<Health> FindAttackedTargets(Health casterHealth) {
		var targetsInRange = new List<Health>();

		var all = GameObject.FindObjectsOfType<Health>();

		foreach (var target in GameObject.FindObjectsOfType<Health>()) {
			if (target == casterHealth || !target.IsAlive)
				continue;

			if (IsInAttackZone(target.transform))
				targetsInRange.Add(target);
		}

		return targetsInRange.OrderBy(t => Vector3.Distance(t.transform.position, transform.position)).ToList();
	}

	public abstract void Cast(Health casterHealth);

	public abstract bool IsInAttackZone(Transform target);

}