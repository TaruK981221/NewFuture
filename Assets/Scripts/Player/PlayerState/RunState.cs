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
        //方向によるスピード変化用（左：-1、右：+1、入力無し：0）
        private float m_speedDirection = 0.0f;

        public RunState()
        {
            SetNextState(this);
            SetPrevState(this);
            Debug.Log("コンストラクタ:RUN");
            Debug.Log("m_PlayerState:RUN:" + m_Param.m_PlayerDirection);
        }

        override public void SetSelfState() { m_selfState = P_STATE.RUN; }
        //// Update is called once per frame
        override public bool Update()
        {
            // Debug.Log("m_PlayerState:RUN:" + m_Param.m_PlayerDirection);
            PositionUpdate();

            return false;
        }
        public override void SetSpeed()
        {
            switch (m_Param.m_PlayerDirection)
            {
                case P_DIRECTION.RIGHT:
                    m_speedDirection = +1.0f;
                    break;
                case P_DIRECTION.LEFT:
                    m_speedDirection = -1.0f;
                    break;
                case P_DIRECTION.MAX_DIRECT:
                    break;
            }
            m_speed.x = m_speedDirection * m_horizontalSpeed;
            m_speed.y = +0.0f * m_gravitySpeed;
        }


        override public bool PlayerInput()
        {
            bool keyinput = false;
            bool R_arrow = Input.GetKey(KeyCode.RightArrow);
            bool L_arrow = Input.GetKey(KeyCode.LeftArrow);
            bool J_key = InputManager.InputManager.Instance.GetKey(InputManager.ButtonCode.Jump);

            switch (m_Param.m_PlayerDirection)
            {
                case P_DIRECTION.RIGHT:

                    //右入力を解除
                    if (!R_arrow)
                    {
                        m_Param.m_PlayerState = P_STATE.IDLE;
                        m_Param.m_PlayerDirection = P_DIRECTION.RIGHT;
                        // Debug.Log("right:RUNRUN");
                        SetPrevState(this);
                        SetNextState(m_idleState);

                        keyinput = true;
                    }
                    //左入力
                    else if (L_arrow)
                    {

                        m_Param.m_PlayerState = P_STATE.RUN;
                        m_Param.m_PlayerDirection = P_DIRECTION.LEFT;
                        // Debug.Log("left:RUN");
                        SetPrevState(this);
                        SetNextState(m_runState);
                        keyinput = true;

                    }

                    break;
                case P_DIRECTION.LEFT:
                    //右入力
                    if (R_arrow)
                    {
                        m_Param.m_PlayerState = P_STATE.RUN;
                        m_Param.m_PlayerDirection = P_DIRECTION.RIGHT;
                        // Debug.Log("right:IDLE");
                        SetPrevState(this);
                        SetNextState(m_runState);
                        keyinput = true;
                    }
                    //左入力を解除
                    else if (!L_arrow)
                    {

                        m_Param.m_PlayerState = P_STATE.IDLE;
                        m_Param.m_PlayerDirection = P_DIRECTION.LEFT;
                        //Debug.Log("left:IDLE");
                        SetPrevState(this);
                        SetNextState(m_idleState);
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
                SetPrevState(this);
                SetNextState(m_riseState);
                keyinput = true;
            }

            return keyinput;
        }
    }//    public class RunState : PlayerState END
}//namespace TeamProject END
