using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;

    [SerializeField]
    private GameObject playerPrefab = null;

    private void Awake() {
        if(null != instance)
        {
            Destroy(instance.gameObject);
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(null == playerPrefab)
        {
            Debug.LogError("playerPrefab is null");
            return;
        }
        GameObject obj = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0, 5f, 0), Quaternion.identity);
    }


    public override void OnJoinedRoom()
    {
        Debug.LogError("OnJoinedRoom");

        if(PhotonNetwork.InRoom)
        {
            Player player = PhotonNetwork.LocalPlayer;

            Room room = PhotonNetwork.CurrentRoom;

            if(null == room)
            {
                Debug.LogError("Room is null");
                return;
            }

            Hashtable h = room.CustomProperties;
            int[] indices = h["indices"] as int[];
            int index = GetIndices(indices);

            Hashtable playerTable = new Hashtable();
            playerTable.Add("index", index);

            if(index == -1)
            {
                Debug.LogError("Current Room has Full Players");
                PhotonNetwork.Disconnect();
            }
            Debug.LogError("My Index: " + index);
            player.SetCustomProperties(playerTable);
            room.SetCustomProperties(h);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.LogError("OnPlayerEnteredRoom");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if(PhotonNetwork.IsMasterClient)
        {
            Hashtable t = otherPlayer.CustomProperties;
            int index = (int)t["index"];
            LeavePlayer(index);
        }
    }

    private int GetIndices(int[] indices)
    {
        for(int i=0; i<indices.Length; ++i)
        {
            if(indices[i] == 0)
            {
                indices[i] = 1;
                return i;
            }
        }
        return -1;
    }

    private void LeavePlayer(int leavePlayerindex)
    {
        Hashtable table = PhotonNetwork.CurrentRoom.CustomProperties;
        int[] indices = table["indices"] as int[];
        indices[leavePlayerindex] = 0;

        PhotonNetwork.CurrentRoom.SetCustomProperties(table);
    }
}
