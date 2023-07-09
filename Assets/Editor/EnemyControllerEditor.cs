using UnityEditor;
using UnityEngine;
using static UnityEngine.UI.Image;

[CustomEditor(typeof(EnemyController))]
public class EnemyControllerEditor : Editor
{

    EnemyController _ai;
    Transform _transform;

    private void OnEnable()
    {
        _ai = (EnemyController) target;
        _transform = _ai.GetComponent<Transform>();
    }

    private void OnSceneGUI()
    {
        Handles.color = Color.green;

        DrawDetectionCircleFor(_ai.GroundDetector);
        DrawDetectionCircleFor(_ai.WallDetector);
        DrawDetectionCircleFor(_ai.CorniceDetector);
        DrawDetectionCircleFor(_ai.PitDetector);

        Handles.color = Color.yellow;

        if (_ai.AttackRange > 0)
            Handles.DrawWireDisc(_transform.position, _transform.forward, _ai.AttackRange);
    }

    private void DrawDetectionCircleFor(LayersDetectorSettings layersDetector)
    {
        if (layersDetector.radius > 0)
        {
            var directionSign = _transform.localEulerAngles.y == 0 ? 1 : -1;
            var drawLocation = _transform.position + new Vector3(layersDetector.offset.x * directionSign, layersDetector.offset.y, 0);
            Handles.DrawWireDisc(drawLocation, _transform.forward, layersDetector.radius);
        }
    }
}