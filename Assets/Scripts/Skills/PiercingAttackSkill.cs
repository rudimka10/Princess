using System.Collections;
using UnityEngine;

public class PiercingAttackSkill : Skill {

	private void Start() {
		ResetTimer();
	}

	private void FixedUpdate() {
		TickTimer(Time.fixedDeltaTime);
	}

	public override void Cast(Health casterHealth) {
		SetupTimer();
		StartCoroutine(AttackCoroutine(casterHealth));
	}

	public override bool IsInAttackZone(Transform target) {
		return Vector3.Distance(aimPoint.position, target.position) <= range;
	}

	private IEnumerator AttackCoroutine(Health casterHealth) {
		yield return new WaitForSeconds(delay);

		var targets = FindAttackedTargets(casterHealth);

		if (targets.Count > 0) {
			var target = targets[0];
			target.Damage(damage);
		}
	}
}