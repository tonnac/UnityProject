using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    #region Private Fields
    [Tooltip("The Beams GameObject to control")]
    [SerializeField]
    private GameObject beams = null;

    bool isFiring;

    #endregion

    #region Public Fields

    public float Health = 100f;

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

    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();

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
