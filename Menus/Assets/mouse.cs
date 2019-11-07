using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse : MonoBehaviour
{
    GameObject moveObj;

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {            
            if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit rayHit))
            {
                Debug.Log("Hit");
            }
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 Mouseposition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.5f);
            Debug.Log(Camera.main.ScreenToWorldPoint(Mouseposition));
        }
        else if (Input.GetMouseButtonUp(0))
        {
        }
    }
}
