using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private bool triggerActive = false;
    public GameObject Panel;

    
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggerActive = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggerActive = false;
        }
    }

    private void Update()
    {
        var ray = new Ray(this.transform.position, this.transform.position);
        if (triggerActive && Input.GetKeyDown(KeyCode.Space))
        {
            OpenUpgradePanel();
        }
    }

    public void OpenUpgradePanel()
    {
        if(Panel != null)
        {
            bool isActive = Panel.activeSelf;
            Panel.SetActive(!isActive);
        }
    }
}
