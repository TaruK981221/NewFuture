using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    [SerializeField, Header("消灯時間")]
    private float m_unlitTime = 0.0f;
    [SerializeField, Header("点灯時間")]
    private float m_lightTime = 0.0f;

    private float m_blinkTimer = 0.0f;
    private float m_endTimer = 0.0f;

    private bool m_blinkStartFlag = false;
    private bool m_blinkEndFlag = false;
    private SpriteRenderer sr = null;

    private enum BLINKSTATE
    {
        NONE,//点滅無し
        ON,//点灯中
        OFF,//消灯中
        MAX_STATE//全状態数
    }
    private BLINKSTATE m_blinkState = BLINKSTATE.NONE;
    // Start is called before the first frame update
    void Start()
    {
        sr = this.transform.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        switch (m_blinkState)
        {
            case BLINKSTATE.NONE:
                BlinkStateNone();
                break;
            case BLINKSTATE.ON:
                BlinkStateOn();
                BlinkEndFlagCheck();
                break;
            case BLINKSTATE.OFF:
                BlinkStateOff();
                BlinkEndFlagCheck();
                break;
            case BLINKSTATE.MAX_STATE:
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 点滅開始フラグON
    /// </summary>
    public void BlinkStart()
    {
        m_blinkStartFlag = true;

    }
    /// <summary>
    /// 点滅終了フラグON
    /// </summary>
    public void BlinkEnd()
    {
        m_blinkEndFlag = true;

    }

    private void BlinkStateNone()
    {
        if (m_blinkStartFlag)
        {
            m_blinkTimer = 0.0f;
            m_blinkStartFlag = false;
            m_blinkState = BLINKSTATE.OFF;
        }

    }
    private void BlinkStateOn()
    {
        if (m_blinkTimer > m_lightTime)
        {
            m_blinkTimer = 0.0f;
            sr.enabled = false;
            m_blinkState = BLINKSTATE.OFF;
        }
        else
        {

        m_blinkTimer += Time.deltaTime;
        }
    }
    private void BlinkStateOff()
    {
        if (m_blinkTimer > m_unlitTime)
        {
            m_blinkTimer = 0.0f;
            sr.enabled = true;
            m_blinkState = BLINKSTATE.ON;
        }
        else
        {

        m_blinkTimer += Time.deltaTime;
        }
    }

    private void BlinkEndFlagCheck()
    {
        if (m_blinkEndFlag)
        {
            m_blinkTimer = 0.0f;
            m_blinkEndFlag = false;
            m_blinkState = BLINKSTATE.NONE;
            sr.enabled = true;

        }

    }
}