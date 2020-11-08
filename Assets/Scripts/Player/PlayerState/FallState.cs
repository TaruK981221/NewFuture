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
            SetNextState();
            Debug.Log("コンストラクタ:FALL");
        }

        override public void SetSelfState() { m_selfState = P_STATE.FALL; }

        //// Update is called once per frame
        override public bool Update()
        {
            if (m_isGround)
            {
                Debug.Log("接地しました:FALLステート");
                SetNextState(m_idleState);
                SetIsGround(false);
                return true;
            }
            PositionUpdate();

            return false;
        }
        public override Vector2 SetSpeed(P_ADDSPEED _addSpeed)
        {
            Vector2 returnSpeed;
            returnSpeed.x = m_horizontalSpeed * _addSpeed.runSpeed;
            //returnSpeed.y = +0.0f * _addSpeed.jumpSpeed;
            returnSpeed.y = -1.0f * _addSpeed.fallSpeed;

            return returnSpeed;

        }
        override public void SetSpeed()
        {
            m_speed.x = m_speedDirection * m_horizontalSpeed;
            m_speed.y = -1.0f * m_gravitySpeed;
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
                m_Param.m_PlayerDirection = P_DIRECTION.LEFT;
                m_speedDirection = SetDirectionSpeed(-1.0f);
                SetNextState();

                keyinput = true;
            }
            //右入力
            if (R_key || R_arrow)
            {
                m_Param.m_PlayerDirection = P_DIRECTION.RIGHT;
                m_speedDirection = SetDirectionSpeed(1.0f);
                SetNextState();
                Debug.Log("右入力:"+this);

                keyinput = true;
            }
            //左右入力無し
            if (!L_input && !R_input)
            {
                m_speedDirection = SetDirectionSpeed(0.0f);
                SetNextState();
            }

            return keyinput;
        }
    }//    public class FallState : PlayerState END
}//namespace TeamProject END
