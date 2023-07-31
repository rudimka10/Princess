using UnityEngine;

public static class Prefs {

	private static readonly string _keyLevelReached = "level_reached";
	
	private static readonly string _keyHealthData = "health_data";

	public static int LevelReached
	{
		get => PlayerPrefs.GetInt(_keyLevelReached, 0);
		set => PlayerPrefs.SetInt(_keyLevelReached, value);
	}

	public static int HealthData
	{
		get => PlayerPrefs.GetInt(_keyHealthData, 5);
		set => PlayerPrefs.SetInt(_keyHealthData, value);
	}

	public static void ResetPlayerData() {
		LevelReached = 0;
		HealthData = 5;
	}
}