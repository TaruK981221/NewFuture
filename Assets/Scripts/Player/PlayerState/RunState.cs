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
        //方向によるスピード変化用（左：-1、右：+1、入力無し：0）
        //private float m_speedDirection = 0.0f;

        public RunState()
        {
            SetNextState(this);
            SetPrevState(this);
        }

        override public void SetSelfState() { SelfState = P_STATE.RUN; }
        //// Update is called once per frame
        override public bool Update()
        {
            if (!m_isGround)
            {
                Debug.Log("No接地:RUNステート");
                m_Param.m_PlayerGround = P_GROUND.FALL;

                SetPrevState(this);
                SetNextState(m_fallState);

                m_timer = 0.0f;
                return true;
            }
            m_timer += Time.deltaTime;

            // Debug.Log("m_PlayerState:RUN:" + m_Param.m_PlayerDirection);
            PositionUpdate();

            if (DamageCheck())
            {

                return true;
            }
          
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
            m_speed.x = m_speedDirection * m_horizontalSpeed * m_horisontalAnimCurve.Evaluate(m_timer);
            m_speed.y = +0.0f * m_gravitySpeed;
        }

        #region//左入力時の処理
        /// <summary>
        /// 左入力時の処理
        /// </summary>
        public override void LeftKeyDownInput()
        {
            m_Param.m_PlayerState = P_STATE.RUN;
            m_Param.m_PlayerDirection = P_DIRECTION.LEFT;
            // Debug.Log("left:RUN");
            SetPrevState(this);
            SetNextState(m_runState);
        }
        public override void LeftKeyHoldInput()
        {
            LeftKeyDownInput();
        }
        public override void LeftKeyUpInput()
        {
            m_Param.m_PlayerState = P_STATE.IDLE;
            m_Param.m_PlayerDirection = P_DIRECTION.LEFT;
            m_timer = 0.0f;
            SetPrevState(this);
            SetNextState(m_idleState);
        }
        #endregion

        #region//右入力時の処理
        /// <summary>
        /// 右入力時の処理
        /// </summary>
        public override void RightKeyDownInput()
        {
            m_Param.m_PlayerState = P_STATE.RUN;
            m_Param.m_PlayerDirection = P_DIRECTION.RIGHT;

            SetPrevState(this);
            SetNextState(m_runState);
        }
        public override void RightKeyHoldInput()
        {
            RightKeyDownInput();
        }
        public override void RightKeyUpInput()
        {
            m_Param.m_PlayerState = P_STATE.IDLE;
            m_Param.m_PlayerDirection = P_DIRECTION.RIGHT;
            m_timer = 0.0f;

            SetPrevState(this);
            SetNextState(m_idleState);
        }
        #endregion

        /// <summary>
        /// 左右入力無しの時の処理
        /// </summary>
        public override void NoMoveInput() { }

        #region//攻撃入力時の処理
        /// <summary>
        /// 攻撃入力時の処理
        /// </summary>
        /// <summary>
        /// 攻撃入力時の処理
        /// </summary>
        public override void AttackKeyDownInput()
        {

            SetPrevState(this);
            SetNextState(m_attackState);

            Debug.Log("attack:IDLE");

        }
        public override void AttackKeyHoldInput() { }
        public override void AttackKeyUpInput() { }
        #endregion

        #region//ジャンプ入力時の処理
        /// <summary>
        /// ジャンプ入力時の処理
        /// </summary>
        public override void JumpKeyDownInput()
        {
            //m_timer = 0.0f;

            m_Param.m_PlayerState = P_STATE.RISE;
            m_Param.m_PlayerGround = P_GROUND.JUMP_1ST;
            SetPrevState(this);
            SetNextState(m_riseState);
        }
        public override void JumpKeyHoldInput() { }
        public override void JumpKeyUpInput() { }
        #endregion

        /// <summary>
        /// 次スタイルチェンジ入力時の処理
        /// </summary>
        public override void NextStyleInput()
        {
            m_Param.m_PlayerState = P_STATE.STYLE_CHANGE_NEXT;
            SetPrevState(this);
            SetNextState(m_nextStyleChangeState);

            Debug.Log("nextstyle:RUN");
        }

        /// <summary>
        /// 前スタイルチェンジ入力時の処理
        /// </summary>
        public override void PrevStyleInput()
        {
            m_Param.m_PlayerState = P_STATE.STYLE_CHANGE_PREV;
            SetPrevState(this);
            SetNextState(m_prevStyleChangeState);

            Debug.Log("prevstyle:RUN");
        }


    }//    public class RunState : PlayerState END
}//namespace TeamProject END
