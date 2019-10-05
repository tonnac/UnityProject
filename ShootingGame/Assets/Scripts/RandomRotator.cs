using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour
{
    public float tumble;

    void Start()
    {
        Rigidbody r = GetComponent<Rigidbody>();
        r.angularVelocity = Random.insideUnitSphere * tumble;
    }
}
