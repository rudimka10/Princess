using UnityEngine;

[System.Serializable]
public class LayersDetectorSettings
{
    public float radius = 0.2f;
    public Vector2 offset = new Vector2(0, -0.5f);
    public LayerMask layersToDetect = ~0;

    public bool IsDetectedFor(Transform origin)
    {
        var directionSign = origin.localEulerAngles.y == 0 ? 1 : -1;
        var point = origin.position + new Vector3(offset.x * directionSign, offset.y, 0);
        return Physics2D.OverlapCircle(point, radius, layersToDetect);
    }
}