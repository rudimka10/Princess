using UnityEngine;
using UnityEngine.Tilemaps;

public class HiddenArea : MonoBehaviour
{

	[SerializeField]
	private Tilemap _tilemap;

	[Range(0, 1)][SerializeField]
	private float _revealedTransparency = 0.5f;

	[SerializeField]
	private bool _disposable;

	private Color _cachedColor;

	private void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.TryGetComponent(out Player _))
		{
			ModifyTransparency(_revealedTransparency);
		}
	}

	private void OnTriggerExit2D(Collider2D c)
	{
		if (c.gameObject.TryGetComponent(out Player _))
		{
			ModifyTransparency(1);
		}
	}

	private void ModifyTransparency(float alpha)
	{
		if (_tilemap != null)
		{
			_cachedColor = _tilemap.color;
			_cachedColor.a = alpha;
			_tilemap.color = _cachedColor;
		}

		if (_disposable)
		{
			Destroy(this);
		}
	}
}