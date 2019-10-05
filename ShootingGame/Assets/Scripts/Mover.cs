using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed;
    void Start()
    {
        Rigidbody r = GetComponent<Rigidbody>();
        Transform t = GetComponent<Transform>();
        r.velocity = t.forward * speed;
    }

}
