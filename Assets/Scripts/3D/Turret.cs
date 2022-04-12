using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    [SerializeField] private GameObject muzzlePoint;
    [SerializeField] private GameObject turretBody;
    [SerializeField] private Vector3 rotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Debug.DrawRay(muzzlePoint.transform.position, Vector3.forward * 5f);
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawLine(muzzlePoint.transform.position, Vector3.forward * 1f);
    }
}
