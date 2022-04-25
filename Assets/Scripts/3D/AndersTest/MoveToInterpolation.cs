using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToInterpolation : MonoBehaviour
{
    [SerializeField] Transform interpolationTarget;
  
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, interpolationTarget.position, Time.deltaTime / Time.fixedDeltaTime);
    }
}
