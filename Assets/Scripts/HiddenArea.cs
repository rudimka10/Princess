using UnityEngine;
using UnityEngine.Tilemaps;

public class HiddenArea : MonoBehaviour
{

    [Range(0, 1)][SerializeField]
    private float _revealedTransparency = 0.5f;
    
    [SerializeField]
    private bool _disposable;

    [SerializeField]
    private Tilemap _tilemap;

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.TryGetComponent(out PlayerController _))
            ModifyTransparency(_revealedTransparency);
    }

    private void OnTriggerExit2D(Collider2D c)
    {
        if (c.gameObject.TryGetComponent(out PlayerController _))
            ModifyTransparency(1);
    }

    private void ModifyTransparency(float alpha)
    {
        if (_tilemap != null)
        {
            var newColor = _tilemap.color;
            newColor.a = alpha;
            _tilemap.color = newColor;
        }

        if (_disposable)
            this.enabled = false;
    }
}