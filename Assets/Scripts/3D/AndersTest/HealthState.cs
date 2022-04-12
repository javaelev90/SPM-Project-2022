using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class HealthState : MonoBehaviourPunCallbacks
{
    private bool isMine;
    [Range(1, 100)]
    [SerializeField] int initialHealth = 10;
    Vector3 startPosition;

    public int Health { get; private set; }

    private void Start()
    {
        Health = initialHealth;
        isMine = photonView.IsMine;
        startPosition = transform.position;
    }

    [PunRPC]
    public void AddHealth(int health)
    {
        if (isMine)
            Health += health;
    }

    [PunRPC]
    public void RemoveHealth(int health)
    {
        if (isMine)
        {
            Health -= health;
            Debug.Log(Health);
        }
            
    }

    [PunRPC]
    public void ResetHealth()
    {
        if (isMine)
            Health = initialHealth;
    }

    [PunRPC]
    public void KillObject()
    {
        if (isMine)
        {
            Health = 0;
            transform.root.gameObject.SetActive(false);
        } 
    }

    [PunRPC]
    public void Revive()
    {
        if (isMine)
        {
            transform.root.gameObject.SetActive(true);
            transform.position = startPosition;
        }
    }

}
