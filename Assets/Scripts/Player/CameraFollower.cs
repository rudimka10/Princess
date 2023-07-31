using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollower : MonoBehaviour
{

    enum CameraUpdateMethod
    {
        FixedUpdate, LateUpdate
    }

    [SerializeField]
    private Transform _target;

    [Min(0)][SerializeField]
    private float _smooth = 4;

    [Space]

    [SerializeField]
    private Rect _accessibleArea = new(0, 0, 40, 12);

    [Space]

    [SerializeField]
    private CameraUpdateMethod _updateMethod;

    private Transform _localTransform;
    private Vector3 _nextPosition;

    public Rect AccessibleArea => _accessibleArea;

    public Transform Target
    {
        get => _target;
        set => _target = value;
    }

    private void Start()
    {
        _localTransform = GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        if (_updateMethod == CameraUpdateMethod.LateUpdate && _target != null)
            Follow(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (_updateMethod == CameraUpdateMethod.FixedUpdate && _target != null)
            Follow(Time.fixedDeltaTime);
    }

    private void Follow(float deltaTime)
    {
        _nextPosition = _localTransform.position;

        _nextPosition.x = Mathf.Clamp(_target.position.x, _accessibleArea.xMin, _accessibleArea.xMax);
        _nextPosition.y = Mathf.Clamp(_target.position.y, _accessibleArea.yMin, _accessibleArea.yMax);

        if (_smooth != 0)
            _nextPosition = Vector3.Lerp(_localTransform.position, _nextPosition, _smooth * deltaTime);

        _localTransform.position = _nextPosition;
    }
}