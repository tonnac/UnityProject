using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerUI : MonoBehaviour
{
#region Private Fields

    [Tooltip("UI Text to display Player's Name")]
    [SerializeField]
    private Text playerNameText = null;

    [Tooltip("UI Slider to display Player's Health")]
    [SerializeField]
    private Slider playerHealthSlider = null;
    private PlayerManager target;

    [Tooltip("Pixel offset from the player target")]
    [SerializeField]
    private Vector3 screeenOffset = new Vector3(0f, 2f, 0f);

    float characterControllerHeight = 0f;
    Transform targetTransform;
    Vector3 targetPosition;
#endregion


#region MonoBehaviour CallBacks

    private void Awake() {
        this.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
    }
    // Update is called once per frame
    void Update()
    {
        if(null == target)
        {
            Destroy(gameObject);
            return;
        }

        if(null != playerHealthSlider)
        {
            playerHealthSlider.value = target.Health;
        }

        if(null != playerNameText)
        {
            Color c = playerNameText.color;

            c.r = Mathf.Lerp(0f, 1f, 1f - target.Health / 100f);
            c.g = Mathf.Lerp(0f, 1f, target.Health / 100f);
            playerNameText.color = c;
        }
    }
#endregion

#region Public Methods

    public void SetTarget(PlayerManager _target)
    {
        if(null == _target)
        {
            Debug.LogError("<Color=red><a>Missing</a></color> PlayMakerManager target for PlayerUI.SetTarget.", this);
            return;
        }

        target = _target;
        if(null != playerNameText)
        {
            playerNameText.text = target.photonView.Owner.NickName;
        }

        CharacterController _characterController = _target.GetComponent<CharacterController>();

        if(null != _characterController)
        {
            characterControllerHeight = _characterController.height;
        }

        targetTransform = target.transform;
    }

    private void LateUpdate() {
        if(null != targetTransform)
        {
            targetPosition = targetTransform.position;
            targetPosition.y += characterControllerHeight;
            this.transform.position = Camera.main.WorldToScreenPoint(targetPosition);// + screeenOffset;
        }
    }

#endregion

}
