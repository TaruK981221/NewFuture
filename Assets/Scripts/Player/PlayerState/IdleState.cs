using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TeamProject
{

    public class IdleState : PlayerState
    {
        public IdleState()
        {
            SetPrevState(this);
            SetNextState(this);
            Debug.Log("コンストラクタ:IDLE");
        }
        // Start is called before the first frame update
        //void Start()
        //{

        //}
        override public void SetSelfState() { m_selfState = P_STATE.IDLE; }

        // Update is called once per frame
        override public bool Update()
        {
            if (!m_isGround)
            {
                Debug.Log("No接地:IDLEステート");
                SetPrevState(this);
                SetNextState(m_fallState);
                //SetIsGround(false);
                return true;
            }

            PositionUpdate();

            return false;
        }
        public override void SetSpeed()
        {
            m_speed.x = +0.0f * m_horizontalSpeed;
            m_speed.y = +0.0f * m_gravitySpeed;
        }


        // Update is called once per frame
        override public bool PlayerInput()
        {
            bool keyinput = false;
            bool R_arrow = Input.GetKey(KeyCode.RightArrow);
            bool L_arrow = Input.GetKey(KeyCode.LeftArrow);
            bool J_key = InputManager.InputManager.Instance.GetKey(InputManager.ButtonCode.Jump);
            // bool flag = false;

            //右入力
            if (R_arrow)
            {
                m_Param.m_PlayerState = P_STATE.RUN;
                m_Param.m_PlayerDirection = P_DIRECTION.RIGHT;
                Debug.Log("right:IDLE");
                SetPrevState(this);
                SetNextState(m_runState);
                keyinput = true;
            }
            //左入力
            else if (L_arrow)
            {

                m_Param.m_PlayerState = P_STATE.RUN;
                m_Param.m_PlayerDirection = P_DIRECTION.LEFT;
                Debug.Log("left:IDLE");
                SetPrevState(this);
                SetNextState(m_runState);

                keyinput = true;

            }
            else
            {
                SetNextState(this);

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
                SetPrevState(this);
                SetNextState(m_riseState);
                keyinput = true;


            }
            //攻撃入力
            if (InputManager.InputManager.Instance.GetKeyDown(InputManager.ButtonCode.Attack))
            {

                //SetPrevState(this);
                SetNextState(m_attackState);

                Debug.Log("attack:IDLE");

            }
            //次スタイルチェンジ入力
            if (InputManager.InputManager.Instance.GetKeyDown(InputManager.ButtonCode.StyleNext))
            {
                //m_PlayerState = P_STATE.STYLE_CHANGE;
                //SetPrevState(this);
                SetNextState(this);

                Debug.Log("style:IDLE");

            }
            //前スタイルチェンジ入力
            else if (InputManager.InputManager.Instance.GetKeyDown(InputManager.ButtonCode.StyleNext))
            {
                //m_PlayerState = P_STATE.STYLE_CHANGE;
                //SetPrevState(this);
                SetNextState(this);
                Debug.Log("style:IDLE");

            }

            return keyinput;

        }


    }//    public class IdleState : PlayerState END
}//namespace TeamProject END
