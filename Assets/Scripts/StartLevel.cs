using System.Collections;
using System.Collections.Generic;
using Controllers.Sound;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLevel : MonoBehaviour
{
    [SerializeField] private int _nextLevelSceneIndex;

    public void StartLevelWithIndex()
    {
        SceneManager.LoadScene(_nextLevelSceneIndex);
        if (_nextLevelSceneIndex == 3)
        {
            SoundController.Instance.PlayEmbient(true);
        }
    }
    
}
