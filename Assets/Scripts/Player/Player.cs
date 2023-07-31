using Controllers.Sound;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[DisallowMultipleComponent]
public class Player : MonoBehaviour {

	#region SERIALIZED FIELDS

	[SerializeField]
	private float _movementSpeed;

	[SerializeField]
	private float _jumpingPower;

	[SerializeField]
	private CircleCollider2D _groundCheck;

	[SerializeField]
	private LayerMask _groundLayers = ~0;

	[SerializeField]
	private Health _health;

	[SerializeField]
	private AudioClip _deathSound;

	[SerializeField]
	private Animator _animator;

	[SerializeField]
	private AttackSettings _piercingAttackSettings;

	[SerializeField]
	private AttackSettings _devastatingBlowSettings;

	#endregion

	#region PRIVATE FIELDS

	private GameView _gameView;

	private Transform _localTransform;
	private Transform _groundCheckTransform;
	private Rigidbody2D _rigidbody;

	private float _horizontalInput;
	private bool _directionIsRight;
	private bool _runningToggleStart;

	#endregion

	#region ACCESSORS

	public bool IsGrounded => Physics2D.OverlapCircle(_groundCheckTransform.position, _groundCheck.radius, _groundLayers);

	public Health Health => _health;

	#endregion

	#region UNITY MESSAGES

	private void Awake() {
		SetupPhysics();
		ResetAttackTimers();

		_gameView = FindObjectOfType<GameView>();
	}

	private void Update() {
		if (_health.IsAlive && !_gameView.Paused) {
			HandleMovementInput();
			RotateToMovementDirection();

			HandleAttackingInput();

			UpdateAnimator();
		}
	}

	private void FixedUpdate() {
		if (_health.IsAlive) {
			Move();
			TickAttacks();
		}
	}

	#endregion

	#region MOVEMENT

	private void SetupPhysics() {
		_localTransform = transform;
		_directionIsRight = _localTransform.eulerAngles.y == 0;
		_groundCheckTransform = _groundCheck.transform;
		_rigidbody = GetComponent<Rigidbody2D>();
	}

	private void HandleMovementInput() {
		_horizontalInput = Input.GetAxisRaw("Horizontal");

		if (Input.GetButtonDown("Jump")) {
			Jump();
		}
	}

	private void RotateToMovementDirection() {
		if (_horizontalInput > 0 && !_directionIsRight) {
			_directionIsRight = true;
			_localTransform.eulerAngles = Vector3.zero;
		} else if (_horizontalInput < 0 && _directionIsRight) {
			_directionIsRight = false;
			_localTransform.eulerAngles = new Vector3(0, 180, 0);
		}
	}

	private void Move() {
		_rigidbody.velocity = new Vector2(_horizontalInput * _movementSpeed, _rigidbody.velocity.y);
	}

	private void Jump() {
		if (IsGrounded) {
			_rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpingPower);
		}
	}

	public void TeleportTo(Vector3 target) {
		_localTransform.position = new Vector3(target.x, target.y, 0);
	}

	#endregion

	#region ATTACKING

	private void ResetAttackTimers() {
		_piercingAttackSettings.ResetTimer();
		_devastatingBlowSettings.ResetTimer();
	}

	private void HandleAttackingInput() {
		if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) {
			if (_piercingAttackSettings.IsReady) {
				OnPiercingAttack();
			}

		} else if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)) {
			if (_devastatingBlowSettings.IsReady) {
				OnDevastatingBlowAttack();
			}
		}
	}

	private void TickAttacks() {
		_piercingAttackSettings.TickTimer(Time.fixedDeltaTime);
		_devastatingBlowSettings.TickTimer(Time.fixedDeltaTime);
	}

	private void OnPiercingAttack() {
		_animator.SetTrigger("attack1");
		_piercingAttackSettings.SetupTimer();
	}

	private void OnDevastatingBlowAttack() {
		_animator.SetTrigger("attack2");
		_devastatingBlowSettings.SetupTimer();
	}

	#endregion

	private void UpdateAnimator() {
		int animatorGravity = 0;
		if (!IsGrounded)
			animatorGravity = _rigidbody.velocity.y < -0.3f ? -1 : 1;

		_animator.SetInteger("gravity", animatorGravity);

		if (!_animator.GetBool("running")) {
			_animator.SetBool("running", Mathf.Abs(_horizontalInput) > 0);

		} else if (Mathf.Abs(_horizontalInput) == 0 && !_runningToggleStart) {
			StartCoroutine(ToggleIdleCoroutine());
		}
	}

	private IEnumerator ToggleIdleCoroutine() {
		_runningToggleStart = true;
		yield return new WaitForSeconds(0.1f);
		if (_horizontalInput == 0)
			_animator.SetBool("running", false);

		_runningToggleStart = false;
	}

	public void OnPlayerDeath() {
		_animator.SetTrigger("death");
		_gameView.OnLose();
		SoundController.Instance.PlaySound(_deathSound);
	}
}