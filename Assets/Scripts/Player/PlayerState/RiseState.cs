using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamProject
{
    /// <summary>
    /// プレイヤーの移動状態クラス
    /// </summary>
    public class RiseState : PlayerState
    {
        public RiseState()
        {
            Debug.Log("コンストラクタ:RISE");
        }

        //// Start is called before the first frame update
        //void Start()
        //{

        //}

        //// Update is called once per frame
        override public bool Update()
        {
            if (m_isHead)
            {
                Debug.Log("頭ぶつけました:RISEステート");
                SetNextState(m_fallState);
                SetIsHead(false);
                m_timer = 0.0f;

                return true;   
            }
            if (m_pos.y>m_jumpHeight+m_StartHeight)
            {
                Debug.Log("最高到達点:RISEステート");
                m_timer = 0.0f;
                SetNextState(m_fallState);
                return true;

            }
            if (m_timer>m_maxTime)
            {
                Debug.Log("滞空時間終了:RISEステート");
                m_timer = 0.0f;
                SetNextState(m_fallState);
                return true;

            }
            m_timer += Time.deltaTime;
            return false;
        }
        public override Vector2 SetSpeed(P_ADDSPEED _addSpeed)
        {
            Vector2 returnSpeed;
            returnSpeed.x = +m_horizontalSpeed * _addSpeed.runSpeed;
            returnSpeed.y = +1.0f * _addSpeed.jumpSpeed;
            //returnSpeed.y = +0.0f * _addSpeed.fallSpeed;

            return returnSpeed;

        }

        override public bool PlayerInput()
        {
            bool keyinput = false;
            bool R_key = InputManager.InputManager.Instance.GetArrow(InputManager.ArrowCode.RightArrow);
            bool R_arrow = Input.GetKey(KeyCode.RightArrow);
            bool L_key = InputManager.InputManager.Instance.GetArrow(InputManager.ArrowCode.LeftArrow);
            bool L_arrow = Input.GetKey(KeyCode.LeftArrow);
            bool J_key = InputManager.InputManager.Instance.GetKey(InputManager.ButtonCode.Jump);
            bool L_input = (L_key || L_arrow);
            bool R_input = (R_key || R_arrow);

            //左入力
            if (L_key || L_arrow)
            {
                SetHorizontalSpeed(-1.0f);
                SetNextState(m_riseState);
                keyinput = true;
            }
            //右入力
            if (R_key || R_arrow)
            {
                SetHorizontalSpeed(1.0f);
                SetNextState(m_riseState);
                keyinput = true;
            }
            //左右入力無し
            if (!L_input && !R_input)
            {
                SetHorizontalSpeed(0.0f);
                SetNextState(m_riseState);
            }

            //ジャンプボタン押し続け
            if (J_key)
            {
                Debug.Log("jump:JUMP");
            }
            //ジャンプボタンを離す
            else if (!J_key)
            {

                m_Param.m_PlayerState = P_STATE.FALL;
                SetNextState(m_fallState);
                keyinput = true;
            }
            //入力無し
            return keyinput;
        }
    }//    public class RiseState : PlayerState END
}//namespace TeamProject END
