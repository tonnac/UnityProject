using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : MonoBehaviour
{
    [SerializeField]
    private Color[] colors = new Color[4];
    public static GameInstance Instance;

    public Color[] Colors
    {
        get{ return colors; }
    }

    private void Awake() {
        if(null != Instance)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;

        colors[0] = new Color(0f,0f,0f,0f);
        colors[1] = new Color(1f,1f,1f,0f);
        colors[2] = new Color(0.75f, 0.23f, 0.55f);
        colors[3] = new Color(0.22f, 0.77f, 0.64f);

        DontDestroyOnLoad(this);
        
    }
}
