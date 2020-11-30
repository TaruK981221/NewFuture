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
        }
        // Start is called before the first frame update
        //void Start()
        //{

        //}
        override public void SetSelfState() { SelfState = P_STATE.IDLE; }

        // Update is called once per frame
        override public bool Update()
        {
            if (!m_isGround)
            {
                Debug.Log("No接地:IDLEステート");
                m_Param.m_PlayerGround = P_GROUND.FALL;


                SetPrevState(this);
                SetNextState(m_fallState);
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


        #region//左入力時の処理
        /// <summary>
        /// 左入力時の処理
        /// </summary>
        public override void LeftKeyDownInput()
        {
            m_Param.m_PlayerState = P_STATE.RUN;
            m_Param.m_PlayerDirection = P_DIRECTION.LEFT;
            Debug.Log("left:IDLE");
            SetPrevState(this);
            SetNextState(m_runState);

        }
        public override void LeftKeyHoldInput()
        {
            LeftKeyDownInput();
        }
        public override void LeftKeyUpInput() { }
        #endregion

        #region//右入力時の処理
        /// <summary>
        /// 右入力時の処理
        /// </summary>
        public override void RightKeyDownInput()
        {
            m_Param.m_PlayerState = P_STATE.RUN;
            m_Param.m_PlayerDirection = P_DIRECTION.RIGHT;
            Debug.Log("right:IDLE");
            SetPrevState(this);
            SetNextState(m_runState);
        }
        public override void RightKeyHoldInput()
        {
            RightKeyDownInput();
        }
        public override void RightKeyUpInput() { }
        #endregion

        /// <summary>
        /// 左右入力無しの時の処理
        /// </summary>
        public override void NoMoveInput()
        {
            SetPrevState(this);
            SetNextState(this);
        }

        #region//攻撃入力時の処理
        /// <summary>
        /// 攻撃入力時の処理
        /// </summary>
        public override void AttackKeyDownInput()
        {

            //SetPrevState(this);
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
            m_Param.m_PlayerState = P_STATE.RISE;
            m_Param.m_PlayerGround = P_GROUND.JUMP_1ST;
            Debug.Log("jump:IDLE");
            SetPrevState(this);
            SetNextState(m_riseState);
        }
        public override void JumpKeyHoldInput() {}
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

            Debug.Log("nextstyle:IDLE");
        }

        /// <summary>
        /// 前スタイルチェンジ入力時の処理
        /// </summary>
        public override void PrevStyleInput()
        {
            m_Param.m_PlayerState = P_STATE.STYLE_CHANGE_PREV;
            SetPrevState(this);
            SetNextState(m_prevStyleChangeState);

            Debug.Log("prevstyle:IDLE");
        }
    }//    public class IdleState : PlayerState END
}//namespace TeamProject END
