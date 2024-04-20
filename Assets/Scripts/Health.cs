using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int HealthPoints
    {
        get => healthPoints;
        set => healthPoints = value;
    }

    [SerializeField] private int healthPoints;

    public void ReceiveDamage()
    {
        HealthPoints--;
        if(HealthPoints <= 0)
        {
            Debug.Log("Player died");
        }
    }
}
