using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLevel : MonoBehaviour
{
    [SerializeField] private int _nextLevelSceneIndex;

    public void StartLevelWithIndex()
    {
        SceneManager.LoadScene(_nextLevelSceneIndex);
    }
}
