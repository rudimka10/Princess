using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

	[SerializeField]
	private Health _playerHealth;

    [SerializeField]
    private AttackSettings _piercingStrikeSettings;

	[SerializeField]
	private AttackSettings _devastatingBlowSettings;

	private void Update()
    {
        if (Input.GetMouseButtonDown(0))
			TryPiercingAttack();

		else if (Input.GetMouseButtonDown(1))
			TryDevastatingBlowAttack();
	}

	private void TryPiercingAttack() {

	}

	private void TryDevastatingBlowAttack() {

	}
	/*
	void TryAttack(string animatorKey) {
		if (_timer <= 0) {
			animator.SetTrigger(animatorKey);
			_timer = _attackCooldown;
			Invoke(nameof(Attack), _attackDelay);
		}
	}

    private void Attack() {
		var attacked = FindAttackedTargets();
		for (int i = 0; i < attacked.Count; i++)
			attacked[i].Damage(_damage);
	}
	*/
    private void FixedUpdate()
    {
	//	_piercingStrikeSettings.TickTimer(Time.fixedDeltaTime);
		//_devastatingBlowSettings.TickTimer(Time.fixedDeltaTime);
    }

    private List<Health> FindAttackedTargets()
    {
        var allTargets = FindObjectsOfType<Health>();
        var attacked = new List<Health>();

        /*foreach (var e in allTargets)
            if (e.IsAlive && e != _playerHealth)
                if (Vector3.Distance(_aimPoint.position, e.transform.position) <= _searchRadius)
    				attacked.Add(e);*/

        return attacked;
    }
}