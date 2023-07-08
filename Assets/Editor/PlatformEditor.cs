using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Platform))]
public class PlatformEditor : Editor
{

    Platform _platform;

    private void OnEnable()
    {
        _platform = (Platform) target;
    }

    private void OnSceneGUI()
    {
        var points = _platform.Points;

        if (points == null || points.Count < 2)
            return;

        Handles.color = Color.red;

        var style = new GUIStyle();
        style.normal.textColor = Color.red;

        var posDelta = new Vector2(0.15f, -0.15f);

        for (int i = 0; i < points.Count; i++)
        {
            Handles.DrawWireDisc(points[i], Vector3.forward, 0.2f);
            Handles.Label(points[i] + posDelta, $"{i}", style);
        }

        Handles.color = Color.yellow;

        for (int i = 0; i < points.Count - 1; i++)
            Handles.DrawDottedLine(points[i], points[i + 1], 3f);

        if (points.Count > 2 && _platform.LoopPath)
            Handles.DrawDottedLine(points[0], points[points.Count - 1], 3f);

    }
}