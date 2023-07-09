using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] private Ladder _ladderToSend;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Player")
        {
            col.transform.position = new Vector3(_ladderToSend.transform.position.x - 10, _ladderToSend.transform.position.y, _ladderToSend.transform.position.z);
        }
    }
}
