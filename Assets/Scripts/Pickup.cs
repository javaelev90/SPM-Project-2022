using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private Pickup_Typs.Pickup typ;
    [SerializeField] public int amount;
    [SerializeField] private GameObject playerToRevive;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public Pickup_Typs.Pickup getTyp()
    {
        return typ;
    }

}
