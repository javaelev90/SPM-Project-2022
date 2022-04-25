using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using Photon.Realtime;

public class AIEnemyFollow : MonoBehaviour
{
    // public NavMeshAgent enemyNav;
    // public Transform player;

    [SerializeField] Transform interpolationTarget;

    [SerializeField] float movementSpeed;
    [SerializeField] float stopDist; // Distance from player where it stops
    [SerializeField] float retreatDist; // Distance when the enemy should back away from player
    [SerializeField] float followRange; // How far away does the player have to be fot the enemy to follow

    [SerializeField] bool targetShip;
    private GameObject targetPlayer;
    private GameObject shipTarget;
    [SerializeField] private HealthState healthState;
    //public void OnPhotonInstantiate(PhotonMessageInfo info)
    //{
    //    Debug.Log("initialized");
    //    object[] instantiationData = info.photonView.InstantiationData;
    //    targetShip = (bool)instantiationData[0];
    //}

    // Start is called before the first frame update
    GameObject[] targets;
    void Start()
    {
        StartCoroutine(Wait(5));
        //Wait(5);
        shipTarget = GameObject.FindGameObjectWithTag("Ship");
        //healthState = GetComponent<HealthState>();
        targets = GameObject.FindGameObjectsWithTag("Player");
    }

    // Wait for player to spawn
     IEnumerator Wait(float sec)
    { while (targetPlayer == null)
        {
            yield return new WaitForSeconds(sec);
            targetPlayer = GameObject.FindGameObjectWithTag("Player");
        }
        
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, interpolationTarget.position, Time.deltaTime / Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //while (LoadBalancingPeer.DispatchIncomingCommands()) ; //Dispatch until everything is Dispatched...
        //LoadBalancingPeer.SendOutgoingCommands(); //Send a UDP/TCP package with outgoing messages
        if (targetShip)
        {
            MoveToShip();
        } 
        else
        {
            FollowPlayer();
        }
        if (healthState.Health <= 0)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }

    private void MoveToShip()
    {
        interpolationTarget.position = Vector3.MoveTowards(transform.position, shipTarget.transform.position, movementSpeed * Time.deltaTime);
    }

    private void FollowPlayer()
    {
        if (targetPlayer != null)
        {
            // Find all the Players 
           
            foreach (GameObject t in targets)
            {
                // Find the one that is closest and make it the target
                if (Vector3.Distance(t.transform.position, gameObject.transform.position) < Vector3.Distance(targetPlayer.transform.position, gameObject.transform.position))
                {
                    targetPlayer = t;
                }
            }

            if (Vector3.Distance(transform.position, targetPlayer.transform.position) < followRange)
            {
                // How far are we from the enemy
                if (Vector3.Distance(transform.position, targetPlayer.transform.position) > stopDist)
                {
                    // move twoards player
                    interpolationTarget.position = Vector3.MoveTowards(transform.position, targetPlayer.transform.position, movementSpeed * Time.deltaTime);
                }
                // Is enemy near enogh to stop moving twoards player
                else if (Vector3.Distance(transform.position, targetPlayer.transform.position) < stopDist && Vector3.Distance(transform.position, targetPlayer.transform.position) > retreatDist)
                {
                    // Stop moving
                    interpolationTarget.position = this.transform.position;
                }
                // If the enemy is to close
                else if (Vector3.Distance(transform.position, targetPlayer.transform.position) < retreatDist)
                {
                    // Back away
                    interpolationTarget.position = Vector3.MoveTowards(transform.position, targetPlayer.transform.position, -movementSpeed * Time.deltaTime);

                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(interpolationTarget.position, 0.5f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Projectile>())
        {
            //photonView.RPC("RemoveHealth", RpcTarget.All, 1);
            //other.gameObject.GetComponent<PhotonView>().RPC("DestoryProjectile", RpcTarget.All);
            //PhotonNetwork.Destroy(gameObject);
            //photonView.RPC("RemoveHealth", RpcTarget.All, 1);
            //photonView.RPC("UpdateHealthBar", RpcTarget.All);

            //other.gameObject.GetComponent<PhotonView>().RPC("DestoryProjectile", RpcTarget.All);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("colliding witha" + collision.gameObject.name);
        //if (collision.gameObject.GetComponent<Projectile>())
        //{
        //    PhotonNetwork.Destroy(gameObject);
        //}
        //if (collision.gameObject.GetComponent<HealthState>())
        //{
        //    collision.gameObject.GetComponent<PhotonView>().RPC("RemoveHealth", RpcTarget.All, 1);
        ////}
        //if (collision.gameObject.GetComponent<Projectile>())
        //{
        //    photonView.RPC("RemoveHealth", RpcTarget.All, 1);
        //    photonView.RPC("UpdateHealthBar", RpcTarget.All, 1);

        //    collision.gameObject.GetComponent<PhotonView>().RPC("DestoryProjectile", RpcTarget.All);
        //    //PhotonNetwork.Destroy(gameObject);
        //}
    }

    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
    //        stream.SendNext(transform.position);
    //        stream.SendNext(transform.rotation);
    //    }
    //    else
    //    {
    //        transform.position = (Vector3)stream.ReceiveNext();
    //        transform.rotation = (Quaternion)stream.ReceiveNext();

    //        //float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
    //        //networkPosition += (this.m_Body.velocity * lag);
    //    }
    //}
}
