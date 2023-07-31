using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoad : MonoBehaviour {

	[SerializeField]
	private Health _playerHealth;

	public void OnEnable()
	{
		_playerHealth.CurrentHealth = Prefs.HealthData;

		if (SceneManager.GetActiveScene().buildIndex > 1)
		{
			Prefs.LevelReached = SceneManager.GetActiveScene().buildIndex;
		}
	}

	public void OnDestroy()
	{
		if (_playerHealth.IsAlive)
		{
			Prefs.HealthData = _playerHealth.CurrentHealth;
		}
	}
}