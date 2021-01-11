using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsAnimationCheck : MonoBehaviour
{
    private bool m_endAnimFlag = false;
    private Animator animator;
    
    public bool EndAnimation()
    {
        return m_endAnimFlag;
    }

    private void AnimationEndFlagON()
    {
        Debug.Log("AnimationEndFlagON():::"+ m_endAnimFlag);
        m_endAnimFlag = true;
    }

    private void AnimationEndFlagOFF()
    {
        Debug.Log("AnimationEndFlagOFF():::"+ m_endAnimFlag);
        m_endAnimFlag = false;
    }

    private void AnimationEnd()
    {
        animator.SetBool("next_stylechange", false);
        animator.SetBool("prev_stylechange", false);
        animator.SetBool("idle", true);
        Debug.Log("update::攻撃終わり");
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
