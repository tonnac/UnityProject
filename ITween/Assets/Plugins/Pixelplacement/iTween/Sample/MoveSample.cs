using UnityEngine;
using System.Collections;

public class MoveSample : MonoBehaviour
{
    private void Start()
    {
        iTween.MoveBy(gameObject, iTween.Hash("x", 2, "easeType", "easeInOutExpo", "loopType", "pingPong", "delay", .1));
    }
}