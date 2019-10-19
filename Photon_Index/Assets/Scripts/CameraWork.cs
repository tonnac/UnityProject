using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWork : MonoBehaviour
{
#region Private Fields
    [SerializeField]
    private float offset = 3f;

    [SerializeField]
    private float distance = 4f;

    float yVelocity = 0f;

    Transform cameraTransform;
    
    [SerializeField]
    float dampSpeed = 0.15f;
#endregion

#region MonoBehaviour CallBacks

    private void LateUpdate() {

        if(null == cameraTransform)
        {
            return;
        }

        Vector3 newTransform = transform.position + (-transform.forward * distance);
        cameraTransform.position = new Vector3(newTransform.x, newTransform.y + offset, newTransform.z);

        float y = 
        Mathf.SmoothDampAngle(cameraTransform.eulerAngles.y, transform.eulerAngles.y, ref yVelocity, dampSpeed);

                cameraTransform.rotation = 
        Quaternion.Euler(transform.eulerAngles.x, y, transform.eulerAngles.z);
    }
#endregion

    public void OnStartFollowing()
    {
        cameraTransform = Camera.main.transform;
    }
}
