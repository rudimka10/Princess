using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerController))]
public class PlayerControllerEditor : Editor
{

    PlayerController _player;
    Vector3 _groundCheckPosition;

    private void OnEnable()
    {
        _player = (PlayerController) target;
    }

    private void OnSceneGUI()
    {
        if (_player.GroundCheckRadius == 0)
            return;

        _groundCheckPosition = _player.transform.position + (Vector3) _player.GroundCheckOffset;

        Handles.color = Color.green;
        Handles.DrawWireDisc(_groundCheckPosition, Vector3.forward, _player.GroundCheckRadius);
    }
}