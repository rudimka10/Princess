using UnityEngine;

public class MainMenuView : MonoBehaviour
{

	[SerializeField]
	private GameObject _newGamePanel;

	[SerializeField]
	private GameObject _continuePanel;

	private void OnEnable()
	{
		_newGamePanel.SetActive(Prefs.LevelReached == 0);
		_continuePanel.SetActive(Prefs.LevelReached > 0);
	}
}