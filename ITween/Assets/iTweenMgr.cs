using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iTweenMgr : MonoBehaviour
{
    public GameObject obj1, obj2, obj3;

    [SerializeField]
    private float time;

    [SerializeField]
    private float targetScale;

    // Start is called before the first frame update
    private void Start()
    {
        //iTween.MoveTo(obj1, iTween.Hash("y", 10f, "time", time, "easetype", iTween.EaseType.easeOutSine, "oncomplete", "DownD", "oncompletetarget", this.gameObject));
        iTween.MoveTo(obj2, iTween.Hash("y", 10f, "time", time, "easetype", iTween.EaseType.easeOutSine, "looptype", iTween.LoopType.pingPong));
        iTween.ScaleTo(obj2, iTween.Hash("scale", new Vector3(targetScale, targetScale, targetScale), "time", time, "looptype", iTween.LoopType.pingPong));

        Hashtable h = new Hashtable();
        h.Add("path", iTweenPath.GetPath("Fly"));
        h.Add("time", 20f);
        h.Add("easetype", iTween.EaseType.linear);
        h.Add("orienttopath", true);
        h.Add("looktarget", obj2.transform.position);
        h.Add("looktime", 0.2f);

        //iTween.MoveTo(Camera.main.gameObject, h);
    }

    public void DownD()
    {
        iTween.MoveTo(obj1, iTween.Hash("y", 0.82f, "time", time, "easetype", iTween.EaseType.easeInSine));
    }

    public void MoveObj2()
    {
        iTween.MoveBy(obj2, iTween.Hash("y", 10f, "time", 1f, "oncomplete", "MoveObj3", "oncompletetarget", this.gameObject));
    }

    public void MoveObj3()
    {
        Hashtable h = new Hashtable();
        h.Add("y", 200f);
        h.Add("delay", 4f);
        h.Add("time", 2f);
        h.Add("easetype", iTween.EaseType.easeInBounce);
        iTween.MoveFrom(obj3, h);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}