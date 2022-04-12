using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AIEnemyFollow : MonoBehaviour
{
    // public NavMeshAgent enemyNav;
    // public Transform player;

    [SerializeField] float movementSpeed;
    [SerializeField] float stopDist; // Distance from player where it stops
    [SerializeField] float retreatDist; // Distance when the enemy should back away from player

    [SerializeField] GameObject player; 
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // How far are we from the enemy
        if (Vector3.Distance(transform.position, player.transform.position) > stopDist)
        {
            // move twoards player
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
        }
        // Is enemy near enogh to stop mving twoards player
        else if (Vector3.Distance(transform.position, player.transform.position) < stopDist && Vector3.Distance(transform.position, player.transform.position) > retreatDist)
        {
            // Stop moving
            transform.position = this.transform.position;
        }
        // If the enemy is to close
        else if(Vector3.Distance(transform.position, player.transform.position) < retreatDist)
        {
            // Back away
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, -movementSpeed * Time.deltaTime);

        }
        /*
         // Solution with NavMesh
        if (enemyNav.isOnNavMesh == false)
        {
            enemyNav.Warp(player.position);
        }

        enemyNav.SetDestination(player.position);
        */
    }
}
