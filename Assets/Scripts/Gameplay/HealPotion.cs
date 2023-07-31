using UnityEngine;

public class HealPotion : Interactable
{

	[SerializeField]
	private int _amountOfHeal = 1;

	private void Start()
	{
		OnInteract.AddListener(Drink);
	}

	private void OnDestroy()
	{
		OnInteract.RemoveListener(Drink);
	}

	private void Drink(Player player)
	{
		player.Health.Heal(_amountOfHeal);
		Destroy(gameObject);
	}
}