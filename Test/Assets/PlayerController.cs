using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator a;
    private Rigidbody r;
    private float x;

    // Start is called before the first frame update
    void Start()
    {
        a = GetComponent<Animator>();
        r = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            a.Play("JUMP00", -1, 0);
        }

        // y = 50(x + 1);
        x = Input.GetAxis("Horizontal");
        a.SetFloat("XAxis", x);
        
        GameManager.instance.speed = 1000f * Mathf.Log10(x + 2);

   //     r.velocity = new Vector3(x * Time.deltaTime * 50.0f, 0.0f, 0.0f);
    }
}
