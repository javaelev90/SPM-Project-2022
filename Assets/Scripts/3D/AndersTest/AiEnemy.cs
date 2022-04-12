using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AiEnemy : MonoBehaviourPunCallbacks
{
    [Range(1, 10)] [SerializeField] float movementSpeed = 1f;
    List<Vector3> patrolPoints = new List<Vector3>();
    Vector3 target;
    int targetIndex;
    // Start is called before the first frame update
    void Start()
    {
        patrolPoints.Add(new Vector3(transform.position.x + 10, transform.position.y, transform.position.z + 10));
        patrolPoints.Add(new Vector3(transform.position.x + 10, transform.position.y, transform.position.z));
        patrolPoints.Add(new Vector3(transform.position.x + 5, transform.position.y, transform.position.z + 5));
        if (patrolPoints.Count > 0)
        {
            target = patrolPoints[0];
            targetIndex = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, target) < 0.05f)
        {
            targetIndex++;
            targetIndex %= patrolPoints.Count;
            target = patrolPoints[targetIndex];
        }
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * movementSpeed);
    }
}
