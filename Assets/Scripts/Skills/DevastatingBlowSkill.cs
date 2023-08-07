using System.Collections;
using UnityEngine;

public class DevastatingBlowSkill : Skill {

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
		var casterPosition = casterHealth.transform.position;

		foreach (var target in targets) {
			target.Damage(damage);
			if (target.TryGetComponent(out Rigidbody2D rb)) {
				Vector2 dir = (target.transform.position - casterPosition).normalized;
				rb.AddForce(dir * normalForce);
			}
		}
	}
}