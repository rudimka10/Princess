using System;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{

	protected Player _player;

	[SerializeField]
	protected UnityEvent<Player> OnInteract;

	private void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.TryGetComponent(out Player player))
		{
			_player = player;
		}
	}

	private void OnTriggerExit2D(Collider2D c)
	{
		if (c.gameObject.TryGetComponent(out Player _))
		{
			_player = null;
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
		{
			if (_player != null && OnInteract != null)
			{
				OnInteract.Invoke(_player);
			}
		}
	}
}