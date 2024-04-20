using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtingObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player")) return;
        EventManager.Instance.RaiseOnGameOver();
    }
}
