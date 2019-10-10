using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.PUN
{
    public class PlayerUI : MonoBehaviour
    {
        #region Private Fields

        [Tooltip("UI Text to display Player's Name")]
        [SerializeField]
        private Text playerNameText;

        [Tooltip("UI Slider to display Player's Health")]
        [SerializeField]
        private Slider playerHealthSlider;

        private PlayerManager target;

        float characterControllerHeight = 0f;
        Transform targetTransform = null;
        Vector3 targetPosition;

        #endregion

        #region Public Fields

        [Tooltip("Pixel offset from the player target")]
        [SerializeField]
        private Vector3 screenOffset = new Vector3(0f, +30f, 0f);

        #endregion

        #region MonoBehaviour CallBacks

        private void Awake()
        {
            this.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(null != playerHealthSlider)
            {
                playerHealthSlider.value = target.Health;
            }

            if(null == target)
            {
                Destroy(this.gameObject);
                return;
            }
        }

        private void LateUpdate()
        {
            if(null != targetTransform)
            {
                targetPosition = targetTransform.position;
                targetPosition.y += characterControllerHeight;
                this.transform.position = Camera.main.WorldToScreenPoint(targetPosition) + screenOffset;
            }
        }
        #endregion

        #region Public Methods

        public void SetTarget(PlayerManager _target)
        {
            if(null == _target)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
                return;
            }

            target = _target;
            targetTransform = target.transform;

            if(null != playerNameText)
            {
                playerNameText.text = target.photonView.Owner.NickName;
            }

            CharacterController _characterController = _target.GetComponent<CharacterController>();
            if(null != _characterController)
            {
                characterControllerHeight = 900f;// _characterController.height;
            }
        }

        #endregion
    }

}