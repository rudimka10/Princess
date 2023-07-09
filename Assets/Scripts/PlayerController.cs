using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private bool _rotateToMoveDirection = true;

    [Header("Movement Settings")]

    [SerializeField]
    private float _movementSpeed = 5;

    [Header("Jumping Settings")]

    [SerializeField]
    private float _jumpingPower = 7;

    [SerializeField]
    private int _jumpsLimit = 2;

    [Header("Ground Detecting Settings")]

    [SerializeField]
    private Vector2 _groundCheckOffset = new Vector2(0, -0.5f);

    [Min(0)][SerializeField]
    private float _groundCheckRadius = 0.4f;

    [SerializeField]
    private LayerMask _groundLayers = ~0;

    [Header("Input Settings")]

    [SerializeField]
    private string _horizontalAxis = "Horizontal";

    [SerializeField]
    private string _jumpingAxis = "Jump";

    [Space]

    [SerializeField]
    private Animator animator;

    private Transform _localTransform;
    private Rigidbody2D _rigidbody;

    private float _horizontalInput;
    private bool _directionIsRight;
    private int _jumpsDone;
    private bool _isAlive = true;

    public float GroundCheckRadius => _groundCheckRadius;

    public Vector2 GroundCheckOffset => _groundCheckOffset;

    private void Start()
    {
        _localTransform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();

        _directionIsRight = _localTransform.eulerAngles.y == 0;
    }

    private void Update()
    {
        if (!_isAlive)
            return;

        _horizontalInput = Input.GetAxisRaw(_horizontalAxis);

        RotateToMovementDirection();

        if (Input.GetButtonDown(_jumpingAxis))
            Jump();

        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        int animatorGravity = 0;

        if (!IsGrounded())
            animatorGravity = _rigidbody.velocity.y < -0.3f ? -1 : 1;

        animator.SetInteger("gravity", animatorGravity);
        animator.SetBool("running", Mathf.Abs(_horizontalInput) > 0);
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_horizontalInput * _movementSpeed, _rigidbody.velocity.y);
    }

    private void RotateToMovementDirection()
    {
        if (!_rotateToMoveDirection)
            return;

        if (_horizontalInput > 0 && !_directionIsRight)
        {
            _directionIsRight = true;
            _localTransform.eulerAngles = Vector3.zero;
        }
        else if (_horizontalInput < 0 && _directionIsRight)
        {
            _directionIsRight = false;
            _localTransform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_localTransform.position + (Vector3) _groundCheckOffset, _groundCheckRadius, _groundLayers);
    }

    private void Jump()
    {
        if (IsGrounded())
            _jumpsDone = 0;

        if (_jumpsDone < _jumpsLimit)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpingPower);
            _jumpsDone++;
        }
    }

    public void TeleportTo(Vector3 target)
    {
        _localTransform.position = new Vector3(target.x, target.y, 0);
    }

    public void OnPlayerDeath()
    {
        if (_isAlive)
        {
            _isAlive = false;
            animator.SetTrigger("death");
            var att = GetComponent<PlayerAttack>();
            if (att != null)
                att.enabled = false;

            Invoke("Reload", 2);
        }
    }

    private void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}