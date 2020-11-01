using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TeamProject
{

    public class IdleState : PlayerState
    {
        public IdleState()
        {
            Debug.Log("コンストラクタ:IDLE");
        }
        // Start is called before the first frame update
        //void Start()
        //{

        //}

        // Update is called once per frame
       override public bool Update()
        {
            if (!m_isGround)
            {
                Debug.Log("No接地:IDLEステート");
                SetNextState(m_fallState);
                //SetIsGround(false);
                return true;
            }


            return false;
        }
        public override Vector2 SetSpeed(P_ADDSPEED _addSpeed)
        {
            Vector2 returnSpeed;
            returnSpeed.x = +0.0f * _addSpeed.runSpeed;
            returnSpeed.y = +0.0f * _addSpeed.jumpSpeed;
            //returnSpeed.y = +0.0f * _addSpeed.fallSpeed;

            return returnSpeed;

        }

        // Update is called once per frame
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
            // bool flag = false;

            //右入力
            if (R_input)
            {
                m_Param.m_PlayerState = P_STATE.RUN;
                m_Param.m_PlayerDirection = P_DIRECTION.RIGHT;
                Debug.Log("right:IDLE");
                m_nextState = m_runState;
                keyinput = true;
            }
            //左入力
            else if (L_input)
            {           

                m_Param.m_PlayerState = P_STATE.RUN;
                m_Param.m_PlayerDirection = P_DIRECTION.LEFT;
                Debug.Log("left:IDLE");
                m_nextState = m_runState;
                keyinput = true;

            }
            //ジャンプ入力
            if (J_key)
            {
                m_Param.m_PlayerState = P_STATE.RISE;
                m_Param.m_PlayerGround = P_GROUND.JUMP_1ST;
                //m_speed.y = jumpSpeed;
                //jumpPos = transform.position.y; //ジャンプした位置を記録する

                ////isJump = true;

                //jumpTime = 0.0f;
                Debug.Log("jump:IDLE");
                SetNextState(m_riseState);
                keyinput = true;


            }
            //攻撃入力
            if (InputManager.InputManager.Instance.GetKeyDown(InputManager.ButtonCode.Attack))
            {
                //m_PlayerState = P_STATE.ATTACK;

                //ChangeState(new AttackState());
                Debug.Log("attack:IDLE");

            }
            //入力
            if (InputManager.InputManager.Instance.GetKeyDown(InputManager.ButtonCode.StyleNext))
            {
                //m_PlayerState = P_STATE.STYLE_CHANGE;
                Debug.Log("style:IDLE");

            }

            return keyinput;

        }


    }//    public class IdleState : PlayerState END
}//namespace TeamProject END
