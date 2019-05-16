using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleLookAT : MonoBehaviour
{
    void Update()
    {
        Transform target = GameObject.Find("DirectionalLight").transform;
        var newRotation = Quaternion.LookRotation(transform.position - target.position, Vector3.forward);
        newRotation.x = 0.0f;
        newRotation.y = 0.0f;
        transform.localRotation = newRotation;
    }
}
