using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsAnimationCheck : MonoBehaviour
{
    private bool m_endAnimFlag = false;
    
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
