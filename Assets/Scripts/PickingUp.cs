using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickingUp : MonoBehaviour
{
    [SerializeField] private Transform camera;
    [SerializeField] private float pickUpDistence = 2;
    [SerializeField] private LayerMask pickupLayer;
    [SerializeField] private Inventory inventory;
    [SerializeField] private int addAmount;

    private RaycastHit pickup;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Physics.Raycast(camera.position,
            camera.TransformDirection(Vector3.forward),
            out pickup,
            pickUpDistence,
            pickupLayer))
        {
            if (Input.GetKey("P"))
            {
                Pickup_Typs.Pickup typ = pickup.collider.gameObject.GetComponent<Pickup>().getTyp();
                if (typ == Pickup_Typs.Pickup.Metal)
                {
                    inventory.addMetal(addAmount);
                }
                else if (typ == Pickup_Typs.Pickup.GreenGoo)
                {
                    inventory.addGreenGoo(addAmount);
                }
                else if (typ == Pickup_Typs.Pickup.AlienMeat)
                {
                    inventory.addAlienMeat(addAmount);
                }
            }
        }
    }
}
