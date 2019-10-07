using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    private Rigidbody r = null;
    [HideInInspector]public float halfLength;

    private MeshRenderer maxRenderer;

    public void SetInitialPosition(PlatformMove p)
    {
        Vector3 move = new Vector3(TipXValue(p) + halfLength, 0.0f, 0.0f);
        r.MovePosition(move);
    }

    private void Awake()
    {
        Debug.Log("Awake");

        r = GetComponent<Rigidbody>();

        MeshRenderer[] e = GetComponentsInChildren<MeshRenderer>();

        float xMin = float.MaxValue;
        float xMax = 0.0f;
        float temp = 0.0f;

        for (int i = 0; i < e.Length; ++i)
        {
            xMin = Mathf.Min(xMin, e[i].transform.position.x - e[i].bounds.extents.x);
            temp = Mathf.Max(xMax, e[i].transform.position.x + e[i].bounds.extents.x);

            
            if(Mathf.Abs(temp - xMax) > 0.1f)
            {
                maxRenderer = e[i];
            }
            xMax = temp;
        }
        Debug.Log(maxRenderer.name);
        float Length = xMax - xMin;
        halfLength = Length * 0.5f;
    }

    private float TipXValue(PlatformMove p)
    {
        return p.maxRenderer.transform.position.x + p.maxRenderer.bounds.extents.x;
    }

    void Start()
    {
        Debug.Log("Start");
    }

    // Update is called once per frame
    void Update()
    {
        float platformSpeed = GameManager.instance.speed;
        r.velocity = -transform.right * platformSpeed * Time.deltaTime;
    }
}
