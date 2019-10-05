using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float tilt;
    public Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;

    public float fireRate;
    private float nextFire;

    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        Rigidbody r = GetComponent<Rigidbody>();
        r.velocity = movement * speed;

        r.position = new Vector3
            (
                Mathf.Clamp(r.position.x, boundary.xMin, boundary.xMax), 
                0.0f,
                Mathf.Clamp(r.position.z, boundary.zMin, boundary.zMax)
            );

        r.rotation = Quaternion.Euler(0.0f, 0.0f, r.velocity.x * -tilt);
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            //  GameObject Clone = 
            Instantiate(shot, transform.position, transform.rotation);// as GameObject;
            AudioSource a = GetComponent<AudioSource>();
            a.Play();
        }
    }
}
