using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamProject
{
    /// <summary>
    /// プレイヤーの移動状態クラス
    /// </summary>
    public class RunState : PlayerState
    {
        public RunState()
        {
            Debug.Log("コンストラクタ:RUN");
            Debug.Log("m_PlayerState:RUN:" + m_Param.m_PlayerDirection);
        }

        //// Start is called before the first frame update
        //void Start()
        //{

        //}

        //// Update is called once per frame
        override public bool Update()
        {
            // Debug.Log("m_PlayerState:RUN:" + m_Param.m_PlayerDirection);
            return false;
        }
        public override Vector2 SetSpeed(P_ADDSPEED _addSpeed)
        {
            Vector2 returnSpeed;
            float speedDirection = 0.0f;
            switch (m_Param.m_PlayerDirection)
            {
                case P_DIRECTION.RIGHT:
                    speedDirection = +1.0f;
                    break;
                case P_DIRECTION.LEFT:
                    speedDirection = -1.0f;
                    break;
                case P_DIRECTION.MAX_DIRECT:
                    break;
            }
            returnSpeed.x = speedDirection * _addSpeed.runSpeed;
            returnSpeed.y = +0.0f * _addSpeed.fallSpeed;
            //returnSpeed.y = +0.0f * _addSpeed.jumpSpeed;


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

            switch (m_Param.m_PlayerDirection)
            {
                case P_DIRECTION.RIGHT:

                    //右入力を解除
                    if (!R_input)
                    {
                        m_Param.m_PlayerState = P_STATE.IDLE;
                        m_Param.m_PlayerDirection = P_DIRECTION.RIGHT;
                        // Debug.Log("right:RUNRUN");
                        m_nextState = m_idleState;
                        keyinput = true;
                    }
                    //左入力
                    else if (L_input)
                    {

                        m_Param.m_PlayerState = P_STATE.RUN;
                        m_Param.m_PlayerDirection = P_DIRECTION.LEFT;
                        // Debug.Log("left:RUN");
                        m_nextState = m_runState;
                        keyinput = true;

                    }

                    break;
                case P_DIRECTION.LEFT:
                    //右入力
                    if (R_input)
                    {
                        m_Param.m_PlayerState = P_STATE.RUN;
                        m_Param.m_PlayerDirection = P_DIRECTION.RIGHT;
                        // Debug.Log("right:IDLE");
                        m_nextState = m_runState;
                        keyinput = true;
                    }
                    //左入力を解除
                    else if (!L_input)
                    {

                        m_Param.m_PlayerState = P_STATE.IDLE;
                        m_Param.m_PlayerDirection = P_DIRECTION.LEFT;
                        //Debug.Log("left:IDLE");
                        m_nextState = m_idleState;
                        keyinput = true;

                    }
                    break;
                case P_DIRECTION.MAX_DIRECT:
                    break;
            }
            //ジャンプ入力
            if (J_key)
            {
                m_Param.m_PlayerState = P_STATE.RISE;
                m_Param.m_PlayerGround = P_GROUND.JUMP_1ST;

                SetNextState(m_riseState);
                keyinput = true;
            }

            return keyinput;
        }
    }//    public class RunState : PlayerState END
}//namespace TeamProject END
