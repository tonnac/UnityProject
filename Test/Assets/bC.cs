using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bC : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Enter");
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Collision Exit");
    }
}
