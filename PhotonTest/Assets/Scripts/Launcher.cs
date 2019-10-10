using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace Com.PUN
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        #region Private Serializable Fields

        [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
        [SerializeField]
        private byte maxPlayersPerRoom = 4;

        #endregion

        #region Private Fields

        string gameVersion = "1";

        [Tooltip("The Ui Panel to let the user enter name, connect and play")]
        [SerializeField]
        private GameObject controlPanel = null;
        [Tooltip("The UI Label to inform the user that the connection is in progress")]
        [SerializeField]
        private GameObject progressLabel = null;

        #endregion

        #region MonoBehaviour CallBacks

        private void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        private void Start()
        {
            SetProgressActive(false);
        }

        public override void OnConnectedToMaster()
        {
            // #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnJoinRandomFailed()
            Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");
            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            SetProgressActive(false);
            Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was claaed by PUN with reason{0}", cause);
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this cliend is in a room.");

            if(PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                Debug.Log("We load the 'Room for 1' ");
                PhotonNetwork.LoadLevel("Room for 1");
            }
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
            Debug.Log("PUN Basics Tutorial/Launcher: OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("PUN Basics Tutorial/Launcher: OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
        }

        #endregion

        #region Public Methods
        public void Connect()
        {
            SetProgressActive(true);

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

        private void SetProgressActive(bool isShow)
        {
            progressLabel.SetActive(isShow);
            controlPanel.SetActive(!isShow);
        }
    }
}