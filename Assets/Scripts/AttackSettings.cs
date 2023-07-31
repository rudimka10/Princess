using UnityEngine;

[System.Serializable]
public class AttackSettings {

	public float cooldown = 1;

	public float delay = 0.4f;

	public float range = 2;

	public int damage = 1;

	public Transform origin;

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
}