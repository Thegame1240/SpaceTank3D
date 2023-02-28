using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);
        GameManager.Instance.EndGame();
    }
}
