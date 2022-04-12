using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthState : MonoBehaviour
{
    [Range(1, 100)]
    [SerializeField] int initialHealth = 1;

    public int Health { get; private set; }

    public void AddHealth(int health)
    {
        Health += health;
    }

    public void RemoveHealth(int health)
    {
        Health -= health;
    }

    public void ResetHealth()
    {
        Health = initialHealth;
    }
}
