using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PickingUp : MonoBehaviourPunCallbacks
{
    private Transform camera;
    [SerializeField] private float pickUpDistence = 2;
    [SerializeField] private LayerMask pickupLayer;
    [SerializeField] private LayerMask spaceShipLayer;
    [SerializeField] private Inventory inventory;
    private GameObject otherPlayer;

    private RaycastHit pickup;

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if (Physics.Raycast(camera.position,
            camera.TransformDirection(Vector3.forward),
            out pickup,
            pickUpDistence,
            pickupLayer))
            {

                if (Input.GetKey(KeyCode.P))
                {
                    Pickup_Typs.Pickup typ = pickup.collider.gameObject.GetComponent<Pickup>().getTyp();
                    if (typ == Pickup_Typs.Pickup.Metal)
                    {
                        inventory.addMetal(pickup.transform.gameObject.GetComponent<Pickup>().amount);
                        PhotonNetwork.Destroy(pickup.transform.gameObject);
                    }
                    else if (typ == Pickup_Typs.Pickup.GreenGoo)
                    {
                        inventory.addGreenGoo(pickup.transform.gameObject.GetComponent<Pickup>().amount);
                        PhotonNetwork.Destroy(pickup.transform.gameObject);
                    }
                    else if (typ == Pickup_Typs.Pickup.AlienMeat)
                    {
                        inventory.addAlienMeat(pickup.transform.gameObject.GetComponent<Pickup>().amount);
                        PhotonNetwork.Destroy(pickup.transform.gameObject);
                    }
                    else if (typ == Pickup_Typs.Pickup.Revive)
                    {
                        inventory.HasReviveBadge = true;
                        otherPlayer = pickup.transform.gameObject.GetComponent<Pickup>().getPlayerToRevive();
                        PhotonNetwork.Destroy(pickup.transform.gameObject);
                    }
                    else if (typ == Pickup_Typs.Pickup.Fire)
                    {
                        inventory.cook();
                    }
                }

                if (Physics.Raycast(camera.position,
                    camera.TransformDirection(Vector3.forward),
                    out pickup,
                    pickUpDistence,
                    spaceShipLayer))
                {
                    if (Input.GetKey(KeyCode.L))
                    {
                        if (inventory.HasReviveBadge)
                        {
                            inventory.HasReviveBadge = false;
                            otherPlayer.GetComponent<HealthState>().Revive();
                        }
                    }
                }
            }
        }
    }
}
