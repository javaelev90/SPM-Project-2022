using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;


public class AIEnemyFollow : MonoBehaviourPunCallbacks
{
    // public NavMeshAgent enemyNav;
    // public Transform player;

    [SerializeField] float movementSpeed;
    [SerializeField] float stopDist; // Distance from player where it stops
    [SerializeField] float retreatDist; // Distance when the enemy should back away from player
    [SerializeField] float followRange; // How far away does the player have to be fot the enemy to follow

    [SerializeField] bool targetShip;
    private GameObject targetPlayer;
    private GameObject shipTarget;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Wait(5));
        //Wait(5);
        shipTarget = GameObject.FindGameObjectWithTag("Ship");
    }

    // Wait for player to spawn
     IEnumerator Wait(float sec)
    { while (targetPlayer == null)
        {
            yield return new WaitForSeconds(sec);
            targetPlayer = GameObject.FindGameObjectWithTag("Player");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (targetShip)
        {
            MoveToShip();
        } 
        else
        {
            FollowPlayer();
        }
        
    }

    private void MoveToShip()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPlayer.transform.position, movementSpeed * Time.deltaTime);
    }

    private void FollowPlayer()
    {
        if (targetPlayer != null)
        {
            // Find all the Players 
            GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");
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
                    transform.position = Vector3.MoveTowards(transform.position, targetPlayer.transform.position, movementSpeed * Time.deltaTime);
                }
                // Is enemy near enogh to stop moving twoards player
                else if (Vector3.Distance(transform.position, targetPlayer.transform.position) < stopDist && Vector3.Distance(transform.position, targetPlayer.transform.position) > retreatDist)
                {
                    // Stop moving
                    transform.position = this.transform.position;
                }
                // If the enemy is to close
                else if (Vector3.Distance(transform.position, targetPlayer.transform.position) < retreatDist)
                {
                    // Back away
                    transform.position = Vector3.MoveTowards(transform.position, targetPlayer.transform.position, -movementSpeed * Time.deltaTime);

                }
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Projectile>())
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("colliding witha" + collision.gameObject.name);
        //if (collision.gameObject.GetComponent<Projectile>())
        //{
        //    PhotonNetwork.Destroy(gameObject);
        //}
        if (collision.gameObject.GetComponent<HealthState>())
        {
            collision.gameObject.GetComponent<PhotonView>().RPC("RemoveHealth", RpcTarget.All, 1);
        }
    }
}
