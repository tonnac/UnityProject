using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
public class PlayerManager : MonoBehaviourPunCallbacks
{    // Start is called before the first frame update
    void Start()
    {
        CameraWork _cameraWork = GetComponent<CameraWork>();
        if(null != _cameraWork)
        {
            if(photonView.IsMine)
            {
                _cameraWork.OnStartFollowing();
            }
        }

        Material a = GetComponentInChildren<Renderer>().material;
        if(null == a)
        {
            Debug.LogError("Player Manager Material is null");
            //Debug.Log(a);
        }

        Player player = photonView.Owner;
        Hashtable h = player.CustomProperties;

        if(null == h)
        {
            Debug.LogError("Player Hashtable is Null");
        }

        int index = (int)h["index"];

        a.color = GameInstance.Instance.Colors[index];
    }
}
