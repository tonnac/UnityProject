using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerAnimatorManager : MonoBehaviourPun
{
#region Private Fields

    Animator animator = null;

    [SerializeField]
    float directionDampTime = 0.25f;

#endregion

    #region MonoBehiviour CallBacks
    private void Awake() {
        animator = GetComponent<Animator>();
        if(null == animator)
        {
            Debug.LogError("animator Componenet is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!photonView.IsMine && PhotonNetwork.IsConnected)
        {
            return;
        }

        if(null == animator)
        {
            return;
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if(v < 0)
        {
            v = 0;
        }

        animator.SetFloat("Speed", h * h + v * v);
        animator.SetFloat("Direction", h, directionDampTime, Time.deltaTime);

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if(stateInfo.IsName("Base Layer.Run"))
        {
            if(Input.GetButtonDown("Fire2"))
            {
                animator.SetTrigger("Jump");
            }
        }
    }
    #endregion
}
