using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBorder : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Platform")
        {
            Debug.Log(other.name + "qqqqqqqqqqqqqqqq");

            Destroy(other.gameObject);
            GameManager.instance.CreatePlatform();
        }
    }
}
