using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_VFX_FromTo : MonoBehaviour
{
    public GameObject clickedVFX;
    public GameObject glowVFX;
    public GameObject UIvfx;
    public GameObject origin;
    public GameObject destination;


    public void SpawnClickedVFX()
    {
        if(clickedVFX != null)
        {
            var vfx = Instantiate(clickedVFX, origin.transform) as GameObject;
            var ps = vfx.GetComponentInChildren<ParticleSystem>();
            Destroy(vfx, ps.main.duration + ps.main.startLifetime.constantMax);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
