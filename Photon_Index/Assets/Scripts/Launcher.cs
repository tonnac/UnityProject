using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Launcher : MonoBehaviourPunCallbacks
{

#region Private Fiedls

    readonly string gameVersion = "1.0";

    [SerializeField]
    byte maxPlayersPerRoom = 4;
    bool isConnecting;

    [SerializeField]
    private GameObject controlPanel = null;

    [SerializeField]
    private GameObject progressPanel = null;

#endregion

#region Photon

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Master");
        if(isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Join Random Room Failed");

        RoomOptions op = new RoomOptions();
        
        Hashtable e = new Hashtable();
        int[] indecies = new int[4] {1, 0, 0, 0};
        e.Add("indices", indecies);
        op.MaxPlayers = maxPlayersPerRoom;
        op.CustomRoomProperties = e;
        PhotonNetwork.CreateRoom(null, op);
    }

    public override void OnCreatedRoom()
    {
        Debug.LogError("<color=red>Created Room!</color>");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        InteractPanel(true);
        Debug.LogFormat("Disconnected : {0}", cause);
    }

    public override void OnJoinedRoom()
    {
        Debug.LogError("Join Room");

        if(PhotonNetwork.InRoom)
        {
            Room room = PhotonNetwork.CurrentRoom;

            if(null == room)
            {
                Debug.LogError("Room is null");
                return;
            }

            Player player = PhotonNetwork.LocalPlayer;

            Hashtable h = new Hashtable();
            h.Add("index", 0);
            player.SetCustomProperties(h);
            PhotonNetwork.LoadLevel(1);
        }
    }

#endregion

#region MonoBehaviour CallBack
    private void Awake() {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start() {
        InteractPanel(true);
    }
#endregion

#region Public Methods

    public void Connect()
    {
        InteractPanel(false);
        isConnecting = true;
        if(PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

#endregion

#region Private Methods

    private void InteractPanel(bool isShowControlPanel)
    {
        controlPanel.SetActive(isShowControlPanel);
        progressPanel.SetActive(!isShowControlPanel);
    }

#endregion
}
