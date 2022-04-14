using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class EnemyHealthHandler : MonoBehaviour
{

    [SerializeField] Slider slider;
    [SerializeField] HealthState state;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = (float)state.Health / (float)state.initialHealth;
    }

    //[PunRPC]
    //public void UpdateHealthBar()
    //{
        
    //}
}
