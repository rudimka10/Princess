using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelGate : MonoBehaviour
{
    [SerializeField] private StartLevel _levelStarter;
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Player")
        {
            _levelStarter.StartLevelWithIndex();
        }
    }
}
