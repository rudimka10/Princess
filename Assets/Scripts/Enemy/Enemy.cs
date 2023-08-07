using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour {

	[SerializeField]
	private float _movementSpeed;

	[SerializeField]
	private float _xPatrolMin;

	[SerializeField]
	private float _xPatrolMax;

	[SerializeField]
	private Animator _animator;

	[SerializeField]
	private float _removeDelay = 5;

	[SerializeField]
	private Vector2 obstacleDetectOffset = new(1, 0);

	[SerializeField]
	private float _obstacleDetectRadus = 0.2f;

	[SerializeField]
	private LayerMask _obstacleLayers;

	[SerializeField]
	private float _playerDetectDistance = 2;

	[SerializeField]
	private PiercingAttackSkill _piercingAttackSkill;

	private Transform _player;
	private Rigidbody2D _rigidbody;
	private Transform _localTransform;
	private Health _health;
	private float _directionSign;

	private float DirectionSign => _localTransform.localEulerAngles.y == 0 ? 1 : -1;

	private void Awake() {
		_rigidbody = GetComponent<Rigidbody2D>();

		_localTransform = transform;
		_directionSign = DirectionSign;

		_health = GetComponent<Health>();

		_player = FindObjectOfType<Player>().transform;
	}

	private void Start() {
		_animator.SetBool("running", true);
	}

	private void FixedUpdate() {
		if (_health.IsAlive) {
			if (TryAttackPlayer()) {
				_rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
			} else {
				UpdateMovementDirection();
				Move();
			}
		}
	}
	private bool TryAttackPlayer() {
		if (!_piercingAttackSkill.IsReady)
			return false;

		if (Vector3.Distance(_localTransform.position, _player.position) <= _playerDetectDistance)
			if (_localTransform.position.x > _player.position.x && _directionSign < 0) {
				int targetAttackType = Random.Range(0, 3);
				if (targetAttackType == 0) {
					OnAttack("attack1");
				} else if (targetAttackType == 0) {
					OnAttack("attack2");
				} else {
					OnAttack("attack3");
				}

				return true;
			}

		return false;
	}

	private void OnAttack(string animatorKey) {
		_animator.SetTrigger(animatorKey);
		_piercingAttackSkill.Cast(_health);
	}

	private void UpdateMovementDirection() {
		Vector2 pointToCheck = new(transform.position.x + _directionSign * obstacleDetectOffset.x, transform.position.y + obstacleDetectOffset.y);
		if (Physics2D.OverlapCircle(pointToCheck, _obstacleDetectRadus, _obstacleLayers)) {
			_directionSign *= -1;
			_localTransform.localEulerAngles = new Vector3(0, _directionSign > 0 ? 0 : 180, 0);
			return;
		}

		if (_directionSign > 0 && _localTransform.position.x > _xPatrolMax) {
			_localTransform.localEulerAngles = new Vector3(0, 180, 0);
			_directionSign = -1;
		} else if (_directionSign < 0 && _localTransform.position.x < _xPatrolMin) {
			_localTransform.localEulerAngles = Vector3.zero;
			_directionSign = 1;
		}
	}

	private void Move() {
		_rigidbody.velocity = new Vector2(_directionSign * _movementSpeed, _rigidbody.velocity.y);
	}

	public void OnEnemyDeath() {
		_rigidbody.velocity = Vector2.zero;
		_animator.SetTrigger("death");
		Invoke(nameof(Remove), _removeDelay);
	}

	private void Remove() {
		Destroy(gameObject);
	}
}
