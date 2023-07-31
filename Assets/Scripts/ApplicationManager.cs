using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationManager : MonoBehaviour
{

	public void OnContinue()
	{
		SceneManager.LoadScene(Prefs.LevelReached);
	}

	public void OnPlay()
	{
		Prefs.ResetPlayerData();
		SceneManager.LoadScene(1);
	}

	public void OnExit()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
	}

	public void OnMenu()
	{
		SceneManager.LoadScene(0);
	}

	public void OnReload()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}