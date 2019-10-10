﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;


namespace Com.PUN
{
    public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
    {
        #region IPunObservable implementation

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if(stream.IsWriting)
            {
                stream.SendNext(IsFiring);
                stream.SendNext(Health);
            }
            else
            {
                this.IsFiring = (bool)stream.ReceiveNext();
                this.Health = (float)stream.ReceiveNext();
            }
        }

        #endregion


        #region Private Fields

        [Tooltip("The Player's UI GameObject Prefab")]
        [SerializeField]
        private GameObject playerUiPrefab = null;

        [Tooltip("The Beams GameObject to control")]
        [SerializeField]
        private GameObject beams = null;

        bool IsFiring;

        #endregion
        #region Public Fields

        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance = null;

        public float Health = 1f;

        #endregion
        #region MonoBehiviour Callbacks

        public override void OnDisable()
        {
            base.OnDisable();

#if UNITY_5_4_OR_NEWER
            SceneManager.sceneLoaded -= OnSceneLoaded;
#endif
        }

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
        /// </summary>
        private void Awake()
        {
            if(beams == null)
            {
                Debug.LogError("<Color=Blue><a>Missing</a></Color> Beams Reference.", this);
            }
            else
            {
                beams.SetActive(false);
            }

            // #Important
            // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
            if (photonView.IsMine)
            {
                LocalPlayerInstance = gameObject;
            }

            // #Critical
            // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            CamaraWork _cameraWork = this.gameObject.GetComponent<CamaraWork>();
            if(null != _cameraWork)
            {
                if(photonView.IsMine)
                {
                    _cameraWork.OnStartFollowing();
                }
            }
            else
            {
                Debug.LogError("<Color=Yellow><a>Missing</a></Color> CameraWork Component on playerPrefab.", this);
            }

            if(null != playerUiPrefab)
            {
                GameObject _uiGo = Instantiate(playerUiPrefab);
                _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
            }
            else
            {
                Debug.LogWarning("<Color=Red><a>Missing</a></Color> PlayerUiPrefab reference on player Prefab.", this);
            }


#if UNITY_5_4_OR_NEWER
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
#endif
        }

        // Update is called once per frame
        void Update()
        {
            if (photonView.IsMine)
            {
                ProcessInput();

                if (Health <= 0f)
                {
                    GameManager.Instance.LeaveRoom();
                }
            }

            if (null != beams && IsFiring != beams.activeSelf)
            {
                beams.SetActive(IsFiring);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(!photonView.IsMine)
            {
                return;
            }

            if(!other.name.Contains("Beam"))
            {
                return;
            }

            Health -= 0.1f;
        }

        private void OnTriggerStay(Collider other)
        {
            if(!photonView.IsMine)
            {
                return;
            }

            if(!other.name.Contains("Beam"))
            {
                return;
            }

            Health -= 0.1f * Time.deltaTime;
        }


#if !UNITY_5_4_OR_NEWER
        void OnLevelWasLoaded(int level)
        {
            this.CalledOnLevelWasLoaded(level);
        }
#endif

        #endregion
        #region Private Methods

        void CalledOnLevelWasLoaded(int level)
        {
            if (!Physics.Raycast(transform.position, -Vector3.up, 5f))
            {
                transform.position = new Vector3(0f, 5f, 0f);
            }
            GameObject _uiGo = Instantiate(this.playerUiPrefab);
            _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
        }
        void ProcessInput()
        {
            if(Input.GetButton("Fire1"))
            {
                if(!IsFiring)
                {
                    IsFiring = true;
                }
            }

            if(Input.GetButtonUp("Fire1"))
            {
                if(IsFiring)
                {
                    IsFiring = false;
                }
            }
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode loadingMode)
        {
            this.CalledOnLevelWasLoaded(scene.buildIndex);
        }

        #endregion
        #region Public Methods
        #endregion


    }

}