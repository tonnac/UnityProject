using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorManager : MonoBehaviour
{
    #region Private Fiedls
    private Animator animator;

    [SerializeField]
    private float directionDampTime = 15.25f;
    #endregion
    #region MonoBehaviour CallBacks
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if(null == animator)
        {
            Debug.LogError("PlayerAnimatorManager is Missing Animator Component", this);
        }
    }

    // Update is called once per frame
    void Update()
    {
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
        animator.SetFloat("Speed",h * h + v * v);
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
