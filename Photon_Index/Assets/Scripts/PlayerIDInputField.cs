using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

[RequireComponent(typeof(InputField))]
public class PlayerIDInputField : MonoBehaviour
{
    #region Private Fields

    const string playerNamePrefKey = "PlayerID";

    #endregion

    #region MonoBehiviour

    private void Start() {
        string defaultName = string.Empty;
        InputField _inputField = GetComponent<InputField>();
        if(null != _inputField)
        {
            if(PlayerPrefs.HasKey(playerNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                _inputField.text = defaultName;
            }
        }

        PhotonNetwork.NickName = defaultName;
    }


    #endregion

    #region Public Methods

    public void SetPlayerName(string value)
    {
        if(string.IsNullOrEmpty(value))
        {
            Debug.LogError("ID Empty");
            return;
        }
        PhotonNetwork.NickName = value;

        PlayerPrefs.SetString(playerNamePrefKey, value);
    }

    #endregion
}
