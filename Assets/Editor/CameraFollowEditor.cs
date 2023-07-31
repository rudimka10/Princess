using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CameraFollower))]
public class CameraFollowEditor : Editor
{

    CameraFollower _cameraFollow;
    Camera _camera;

    Vector3 _bottomLeft, _bottomRight, _topRight, _topLeft;
    float _left, _right, _top, _bottom, _dX;

    private void OnEnable()
    {
        _cameraFollow = (CameraFollower) target;
        _camera = _cameraFollow.GetComponent<Camera>();
    }

    private void OnSceneGUI()
    {
		_dX = _camera.orthographicSize * _camera.aspect;

        _left = _cameraFollow.AccessibleArea.xMin - _dX;
        _right = _cameraFollow.AccessibleArea.xMax + _dX;
        _top = _cameraFollow.AccessibleArea.yMax + _camera.orthographicSize;
        _bottom = _cameraFollow.AccessibleArea.yMin - _camera.orthographicSize;

        _bottomLeft = new Vector3(_left, _bottom, 0);
        _bottomRight = new Vector3(_right, _bottom, 0);
        _topRight = new Vector3(_right, _top, 0);
        _topLeft = new Vector3(_left, _top, 0);

        Handles.color = Color.red;
        Handles.DrawDottedLine(_topLeft, _topRight, 3f);
        Handles.DrawDottedLine(_topRight, _bottomRight, 3f);
        Handles.DrawDottedLine(_bottomRight, _bottomLeft, 3f);
        Handles.DrawDottedLine(_bottomLeft, _topLeft, 3f);
    }
}