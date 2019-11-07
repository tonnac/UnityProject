using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    private float time;

    private void Awake() {
    }

    private void Update() {
        time += Time.deltaTime;
        gameObject.transform.rotation = Quaternion.AngleAxis(time * 360, new Vector3(0, 0, 1)) * Quaternion.Euler(90, 0, 0);
    }
}
