using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public float speed = 50.0f;

    public GameObject[] platformList;

    public GameObject firstPlatform;

    private PlatformMove CurrentPlatform;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
        CurrentPlatform = firstPlatform.GetComponent<PlatformMove>();
    }

    void Update()
    {

    }

    public void CreatePlatform()
    {
        GameObject inst = Instantiate(platformList[1], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity) as GameObject;
        PlatformMove p = inst.GetComponent<PlatformMove>();
        p.SetInitialPosition(CurrentPlatform);
        CurrentPlatform = p;
    }
}
