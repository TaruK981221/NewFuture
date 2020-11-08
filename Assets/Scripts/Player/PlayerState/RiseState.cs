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
         //ジャンプ開始時点の高さ
        private float m_StartHeight2;
        //最大ジャンプ高さ受け取り用
        private float m_jumpMaxHeight2;
        //最大時間
        private float m_maxTime2 = 0.0f;

        //ジャンプ開始地点登録フラグ
        private bool m_jumpHeightSetFlag = false;

        //方向によるスピード変化用（左：-1、右：+1、入力無し：0）
        private float m_speedDirection = 0.0f;

        public RiseState()
        {
            SetNextState();
            Debug.Log("コンストラクタ:RISE");
        }

        override public void SetSelfState() { m_selfState = P_STATE.RISE; }

        //// Update is called once per frame
        override public bool Update()
        {
            if (!m_jumpHeightSetFlag)
            {
                SetJumpStartFlag(true);
                //ジャンプ開始地点の座標を確保する
                SetJumpStartPos(m_Position.y);
            }
            if (m_isHead)
            {
                Debug.Log("頭ぶつけました:RISEステート");
                SetNextState(m_fallState);
                SetJumpStartFlag(false);
                SetIsHead(false);
                m_timer = 0.0f;

                return true;   
            }
            if (m_Position.y> m_jumpMaxHeight2 + m_StartHeight2)
            {
                Debug.Log("最高到達点:RISEステート");
                m_timer = 0.0f;
                SetNextState(m_fallState);
                SetJumpStartFlag(false);

                return true;

            }
            if (m_timer>m_maxTime)
            {
                Debug.Log("滞空時間終了:RISEステート");
                m_timer = 0.0f;
                SetNextState(m_fallState);
                SetJumpStartFlag(false);

                return true;

            }
            PositionUpdate();

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
        public override void SetSpeed()
        {
            m_speed.x = m_speedDirection * m_horizontalSpeed;
            m_speed.y = -1.0f * m_gravitySpeed;
            ////水平速度
            //m_horizontalSpeed
            ////重力速度
            //m_gravitySpeed 
            ////ジャンプする速度
            //m_jumpSpeed
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
                Debug.Log("右入力:" + this);

                keyinput = true;
            }
            //左右入力無し
            if (!L_input && !R_input)
            {
                m_speedDirection = SetDirectionSpeed(0.0f);
                SetNextState();
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
                SetJumpStartFlag(false);
                keyinput = true;
            }
            //入力無し
            return keyinput;
        }//override public bool PlayerInput() END

        public void SetJumpStartPos(float _jumpstartpos)
        {
            m_StartHeight2 = _jumpstartpos;
        }

        void SetJumpStartFlag(bool _flag)
        {
            m_jumpHeightSetFlag = _flag;
        }
    }//    public class RiseState : PlayerState END
}//namespace TeamProject END
