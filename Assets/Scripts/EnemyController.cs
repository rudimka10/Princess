using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{

    [SerializeField]
    private float _movementSpeed;

    [SerializeField]
    private Vector2 _jumpingPower;

    [SerializeField]
    private float _xPatrolMin;

    [SerializeField]
    private float _xPatrolMax;

    [SerializeField]
    private bool _pursuitPlayer = true;

    [Space]

    [SerializeField]
    private LayersDetectorSettings _groundDetector = new();

    [SerializeField]
    private LayersDetectorSettings _wallDetector = new();

    [SerializeField]
    private LayersDetectorSettings _corniceDetector = new();

    [SerializeField]
    private LayersDetectorSettings _pitDetector = new();

    [Header("Player Detecting Settings")]

    [SerializeField]
    private float _maxAttackingAngle;

    [SerializeField]
    private float _minAttackingAngle;

    [SerializeField]
    private float _Range = 2f;

    [Header("Attacking Settings")]

    [SerializeField]
    private int _damage = 1;

    [SerializeField]
    private float _attackDelay = 0.8f;

    [SerializeField]
    private Rigidbody2D _rangedAttackProjectile;

    [SerializeField]
    private Transform _aimPoint;

    [Space]

    [SerializeField]
    private Animator animator;

    private Transform _localTransform;
    private Rigidbody2D _rigidbody;

    private bool _groundDetected;
    private bool _wallDetected;
    private bool _corniceDetected;
    private bool _pitDetected;
    private float _attackTimer;
    private bool _isAlive = true;

    public LayersDetectorSettings GroundDetector => _groundDetector;

    public LayersDetectorSettings WallDetector => _wallDetector;

    public LayersDetectorSettings CorniceDetector => _corniceDetector;

    public LayersDetectorSettings PitDetector => _pitDetector;

    public float AttackRange => _Range;

    private void Awake()
    {
        _localTransform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        animator.SetBool("running", true);
    }

    private void Update()
    {
        if (!_isAlive)
            UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        int animatorGravity = 0;

        if (!_groundDetected)
            animatorGravity = _rigidbody.velocity.y < -0.9f ? -1 : 1;

        Debug.Log($"{_rigidbody.velocity.y}");

        animator.SetInteger("gravity", animatorGravity);
    }

    private void FixedUpdate()
    {
        if (!_isAlive)
            return;
            if (_attackTimer > 0)
            _attackTimer -= Time.fixedDeltaTime;

        bool playerFinded = TryDetectPlayer(out PlayerController player, out bool playerInRange);

        if (playerFinded)
        {
            if (player.transform.position.x > _localTransform.position.x)
                _localTransform.localEulerAngles = Vector3.zero;
            else
                _localTransform.localEulerAngles = new Vector3(0, 180, 0);

            if (playerInRange)
            {
                if (_attackTimer <= 0 && player.TryGetComponent(out Health health))
                {
                    health.Damage(_damage);
                    _attackTimer = _attackDelay;
                    animator.SetTrigger("attack");
                }
            }
            else
            {
                UpdateEnvironmentInformation();
                ApplyMovementLogic();
            }
            return;
        }

        UpdateEnvironmentInformation();
        ApplyMovementLogic();
    }

    private void UpdateEnvironmentInformation()
    {
        _groundDetected = _groundDetector.IsDetectedFor(transform);
        _wallDetected = _wallDetector.IsDetectedFor(transform);
        _corniceDetected = _corniceDetector.IsDetectedFor(transform);
        _pitDetected = _pitDetector.IsDetectedFor(transform);
    }

    private void ApplyMovementLogic()
    {
        if (!_groundDetected)
            return;


        if (_wallDetected && !_corniceDetected)
        {
            StartCoroutine(Jump());
        }
        else if (_wallDetected || !_pitDetected)
        {
            _localTransform.localEulerAngles = new Vector3(0, _localTransform.localEulerAngles.y == 0 ? 180 : 0, 0);
        }
        else
        {
            Move();
        }

        RotateToMoveDirection();
    }

    private IEnumerator Jump()
    {
        yield return null;

        float directionSign = _localTransform.localEulerAngles.y == 0 ? 1 : -1;
        if (Mathf.Abs(_rigidbody.velocity.x) < 1)
            directionSign *= 2;
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x + _jumpingPower.x * directionSign, _jumpingPower.y);
    }

    private void Move()
    {
        float xMovement = (_localTransform.localEulerAngles.y == 0 ? 1 : -1) * _movementSpeed;
        _rigidbody.velocity = new Vector2(xMovement, _rigidbody.velocity.y);
    }

    private void RotateToMoveDirection()
    {
        _localTransform.localEulerAngles = new Vector3(0, _localTransform.localEulerAngles.y == 0 ? 0 : 180, 0);
    }

    private bool TryDetectPlayer(out PlayerController player, out bool inRange)
    {
        player = FindObjectOfType<PlayerController>();
        if (player == null)
        {
            inRange = false;
            return false;
        }

        inRange = Vector3.Distance(_localTransform.position, player.transform.position) <= _Range;

        if (!inRange)
            return false;


        var hit = Physics2D.Linecast(_aimPoint.position, player.transform.position);

        if (hit.collider == null)
            return false;

        if (hit.transform.TryGetComponent(out PlayerController findedPlayer))
            return true;

        return false;
    }

    public void OnEnemyDeath()
    {
        _isAlive = false;
        animator.SetTrigger("Death");
        foreach (var c in GetComponentsInChildren<Collider2D>())
            c.enabled = false;
    }
}