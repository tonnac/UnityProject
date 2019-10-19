
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;


public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
{
    #region IPunObservable Implementation

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(isFiring);
            stream.SendNext(Health);
        }
        else
        {
            this.isFiring = (bool)stream.ReceiveNext();
            this.Health = (float)stream.ReceiveNext();
        }
    }

    #endregion
    #region Private Fields
    [Tooltip("The Beams GameObject to control")]
    [SerializeField]
    private GameObject beams = null;

    bool isFiring;

    [Tooltip("The Player's UI GameObject Prefab")]
    [SerializeField]
    private GameObject playerUiPrefab = null;

    #endregion
    #region Public Fields

    public float Health = 100f;

    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;

    #endregion
    #region MonoBehaviour CallBacks
    private void Awake() {
        if(null == beams)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> Beams Reference.", this);
        }
        else
        {
            beams.SetActive(false);
        }

        if(photonView.IsMine)
        {
            PlayerManager.LocalPlayerInstance = this.gameObject;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start() {
        CameraWork _cameraWork = this.gameObject.GetComponent<CameraWork>();
        if(null != _cameraWork)
        {
            if(photonView.IsMine)
            {
                _cameraWork.OnStartFollowing();
            }
        }
        else
        {
            Debug.LogError("<color=red><a>Missing</a></color> CameraWork Component on playerPrefab", this);
        }

        if(null != playerUiPrefab)
        {
            GameObject _uiGo = Instantiate(playerUiPrefab);
            _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
        }
        else
        {
            Debug.LogWarning("<color=red><a>Missing</a></color> PlayerUIPrefab reference on player Prefab", this);
        }

        #if UNITY_5_4_OR_NEWER
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        #endif
    }
    // Update is called once per frame
    void Update()
    {
        if(photonView.IsMine)
        {
            ProcessInputs();
        }

        if(Health <= 0f)
        {
            GameManager.Instance.LeaveRoom();
        }

        if(null != beams && isFiring != beams.activeSelf)
        {
            beams.SetActive(isFiring);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(!photonView.IsMine)
        {
            return;
        }

        if(!other.name.Contains("Beam"))
        {
            return;
        }

        Health -= 10.0f;
    }

    private void OnTriggerStay(Collider other) {
        if(!photonView.IsMine)
        {
            return;
        }

        if(!other.name.Contains("Beam"))
        {
            return;
        }

        Health -= 10.0f * Time.deltaTime;
    }

    #if !UNITY_5_4_OR_NEWER
    public void OnLevelWasLoaded(int level)
    {
        this.CalledOnLevelWasLoaded(level);
    }
    #endif

    void CalledOnLevelWasLoaded(int level)
    {
        if (!Physics.Raycast(transform.position, -Vector3.up, 5f))
        {
            transform.position = new Vector3(0f, 5f, 0f);
        }
        GameObject _uiGo = Instantiate(this.playerUiPrefab);
        _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);   
    }

    public override void OnDisable() {
        base.OnDisable();

        #if UNITY_5_4_OR_NEWER
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
        #endif
    }

    #if UNITY_5_4_OR_NEWER
    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode loadingMode)
    {
        this.CalledOnLevelWasLoaded(scene.buildIndex);
    }
    
    #endif
    #endregion
    #region custom
    void ProcessInputs()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if(!isFiring)
            {
                isFiring = true;
            }
        }

        if(Input.GetButtonUp("Fire1"))
        {
            if(isFiring)
            {
                isFiring = false;
            }
        }
    }
    #endregion


}
