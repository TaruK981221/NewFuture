using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamProject
{
    /// <summary>
    /// プレイヤーの移動状態クラス
    /// </summary>
    public class FallState : PlayerState
    {
        //方向によるスピード変化用（左：-1、右：+1、入力無し：0）
        private float m_speedDirection = 0.0f;

        public FallState()
        {
            SetPrevState(this);
            SetNextState(this);
            Debug.Log("コンストラクタ:FALL");
        }

        override public void SetSelfState() { m_selfState = P_STATE.FALL; }

        //// Update is called once per frame
        override public bool Update()
        {
            if (m_isGround)
            {
                Debug.Log("接地しました:FALLステート");
                SetPrevState(this);
                SetNextState(m_idleState);

                //SetIsGround(false);
                return true;
            }
            PositionUpdate();

            return false;
        }
        override public void SetSpeed()
        {
            m_speed.x = m_speedDirection * m_horizontalSpeed;
            m_speed.y = -1.0f * m_gravitySpeed;
        }

        override public bool PlayerInput()
        {
            bool keyinput = false;
            bool R_arrow = Input.GetKey(KeyCode.RightArrow);
            bool L_arrow = Input.GetKey(KeyCode.LeftArrow);
            bool J_key = InputManager.InputManager.Instance.GetKey(InputManager.ButtonCode.Jump);

            //左入力
            if (L_arrow)
            {
                m_Param.m_PlayerDirection = P_DIRECTION.LEFT;
                m_speedDirection = SetDirectionSpeed(-1.0f);
                SetPrevState(this);
                SetNextState(this);

                keyinput = true;
            }
            //右入力
            if (R_arrow)
            {
                m_Param.m_PlayerDirection = P_DIRECTION.RIGHT;
                m_speedDirection = SetDirectionSpeed(1.0f);
                SetPrevState(this);
                SetNextState(this);
                Debug.Log("右入力:"+this);

                keyinput = true;
            }
            //左右入力無し
            if (!L_arrow && !R_arrow)
            {
                m_speedDirection = SetDirectionSpeed(0.0f);
                SetPrevState(this);
                SetNextState(this);
            }

            return keyinput;
        }
    }//    public class FallState : PlayerState END
}//namespace TeamProject END
