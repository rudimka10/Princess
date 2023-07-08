using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    public enum PlatformMoveDirection
    {
        Forward, Back
    }

    [SerializeField]
    private bool _attachPlayer = true;

    [Space]

    [SerializeField]
    private float _speed = 3;

    [SerializeField]
    private PlatformMoveDirection _direction;

    [SerializeField]
    private bool _loopPath = true;

    [SerializeField]
    private int _targetPointIndex = 1;

    [Space]

    [SerializeField]
    private List<Vector2> _points = new() { new Vector2(2, 0), new Vector2(-2, 0) };

    private Transform _localTransform;

    public List<Vector2> Points => _points;

    public bool LoopPath => _loopPath;

    private void Start()
    {
        _localTransform = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        UpdateTargetIfNeed();
        MoveToTarget();
    }

    private void UpdateTargetIfNeed()
    {
        Debug.Log($"target={_targetPointIndex} all={_points.Count}");
        if (_localTransform.position.x != _points[_targetPointIndex].x || _localTransform.position.y != _points[_targetPointIndex].y)
            return;

        if (_loopPath)
            UpdateLoopPathTarget();

        else
            UpdatePingPongPathTarget();
    }

    private void UpdateLoopPathTarget()
    {
        _targetPointIndex += _direction == PlatformMoveDirection.Forward ? 1 : -1;

        if (_targetPointIndex == -1)
            _targetPointIndex = _points.Count - 1;

        else if (_targetPointIndex == _points.Count)
            _targetPointIndex = 0;
    }

    private void UpdatePingPongPathTarget()
    {
        if (_direction == PlatformMoveDirection.Forward && _targetPointIndex == _points.Count - 1)
            _direction = PlatformMoveDirection.Back;

        else if (_direction == PlatformMoveDirection.Back && _targetPointIndex == 0)
            _direction = PlatformMoveDirection.Forward;

        _targetPointIndex += _direction == PlatformMoveDirection.Forward ? 1 : -1;
    }

    private void MoveToTarget()
    {
        _localTransform.position = Vector3.MoveTowards(_localTransform.position, _points[_targetPointIndex], _speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (_attachPlayer && c.gameObject.TryGetComponent(out PlayerController player))
            player.transform.parent = _localTransform;
    }

    private void OnCollisionExit2D(Collision2D c)
    {
        if (_attachPlayer && c.gameObject.TryGetComponent(out PlayerController player) && player.transform.parent == _localTransform)
            player.transform.parent = null;
    }
}