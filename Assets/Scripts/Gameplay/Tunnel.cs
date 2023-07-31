using UnityEngine;
using UnityEngine.SceneManagement;

public class Tunnel : Interactable
{

	[SerializeField]
	private Transform _targetPoint;

	[SerializeField]
	private GameObject _oldCamera;

	[SerializeField]
	private GameObject _newCamera;

	[SerializeField]
	private int _targetSceneBuildIndex;

	[SerializeField]
	private bool _disposable;

	[SerializeField]
	private SpriteRenderer spriteRenderer;

	[SerializeField]
	private Sprite closedSprite;

	private void Start()
	{
		OnInteract.AddListener(Pass);
	}

	private void OnDestroy()
	{
		OnInteract.RemoveListener(Pass);
	}

	private void Pass(Player player)
	{
		if (_targetPoint != null)
		{
			if (_oldCamera != null && _newCamera != null)
			{
				_oldCamera.SetActive(false);
				_newCamera.SetActive(true);
			}
			player.TeleportTo(_targetPoint.position);
			if (_disposable)
			{
				spriteRenderer.sprite = closedSprite;
				this.enabled = false;
			}
		} else
		{
			SceneManager.LoadScene(_targetSceneBuildIndex);
		}
	}

	private void OnDrawGizmos()
	{
		if (_targetPoint != null)
		{
			Gizmos.color = Color.green;
			Gizmos.DrawLine(transform.position, _targetPoint.position);
		}
	}
}