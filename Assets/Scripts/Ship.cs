using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] private GameObject Panel;
    [SerializeField] private float radius = 10f;
    private bool triggerActive = false;

    private void Update()
    {

        Collider[] colliderHits = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider col in colliderHits)
        {
            if (col.tag == ("Player") && Input.GetKeyDown(KeyCode.L) && Panel != null)
            {
                //Debug.Log("Inside");
                OpenUpgradePanel();
            }
            else
            {
                //Debug.Log("Outside");
            }
        }

        /*
        if (Physics.SphereCast(ray, radius, out hit, 10f))
        {
            if (hit.collider.tag == "Player")
            {
                Debug.Log("Inside");
                OpenUpgradePanel();
            }
        }
        */

        /*
        if (Physics.Raycast(ray, out hit, height))
        {
            
            if (hit.collider.tag == ("Player"))
            {
                OpenUpgradePanel();
            }
        }
        */
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void OpenUpgradePanel()
    {
        //triggerActive = true;
        if(Panel != null)
        {
            bool isActive = Panel.activeSelf;
            Panel.SetActive(!isActive);
        }
    }
}