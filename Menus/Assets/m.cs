using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
    static public class Util
    {

    }
public class m : MonoBehaviour
{
    List<Decrease> decreaseList;
    private IEnumerator<Decrease> indices;

    public Decrease GetNextSlider()
    {
        indices.MoveNext();
        return indices.Current;
    }

    public IEnumerator<Decrease> GetEnumerator()
    {
        int index = 0;
        while(true)
        {
            yield return decreaseList[index];
            index = (index + 1) % decreaseList.Count;
        }
    }

    private void Awake() 
    {
        decreaseList = new List<Decrease>();
        foreach(Transform transfo in transform)
        {
            Decrease decrease = transfo.GetComponent<Decrease>();
            if(null != decrease)
            {
                decrease.OnEnd += () =>
                {
                    GetNextSlider().isPlay = true;
                };
                decreaseList.Add(decrease);
            }
        }
        indices = GetEnumerator();
        Debug.LogWarning($"{decreaseList.Count}");
    }

    private void Start() 
    {
        GetNextSlider().isPlay = true;
    }
}
