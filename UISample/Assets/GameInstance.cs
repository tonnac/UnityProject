using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : MonoBehaviour
{   
    public int totalNumber;
    static private GameInstance instance;
    // static public GameInstance Instance 
    // {
    //     get
    //     {
    //         if(null == instance)
    //         {
    //             GameInstance inst = Instantiate(this, Vector3.zero, Quaternion.identity);
    //             if(null != inst)
    //             {
    //                 instance = inst;
    //             }
    //         }
    //         return instance;
    //     }
    // }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
