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
        }

        override public void SetSelfState() { SelfState = P_STATE.FALL; }

        //// Update is called once per frame
        override public bool Update()
        {
            if (m_isGround)
            {
                Debug.Log("接地しました:FALLステート");
                m_Param.m_PlayerGround = P_GROUND.GROUND;

                SetPrevState(this);
                SetNextState(m_idleState);
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

        #region//左入力時の処理
        /// <summary>
        /// 左入力時の処理
        /// </summary>
        public override void LeftKeyDownInput()
        {
            m_Param.m_PlayerDirection = P_DIRECTION.LEFT;
            m_speedDirection = SetDirectionSpeed(-1.0f);
            SetPrevState(this);
            SetNextState(this);
        }

        public override void LeftKeyHoldInput()
        {
            LeftKeyDownInput();
        }
        public override void LeftKeyUpInput()
        {
            m_Param.m_PlayerDirection = P_DIRECTION.LEFT;
            m_speedDirection = SetDirectionSpeed(0.0f);
            SetPrevState(this);
            SetNextState(this);
        }
        #endregion

        #region//右入力時の処理
        /// <summary>
        /// 右入力時の処理
        /// </summary>
        public override void RightKeyDownInput()
        {
            m_Param.m_PlayerDirection = P_DIRECTION.RIGHT;
            m_speedDirection = SetDirectionSpeed(1.0f);
            SetPrevState(this);
            SetNextState(this);
        }
        public override void RightKeyHoldInput()
        {
            RightKeyDownInput();
        }
        public override void RightKeyUpInput()
        {
            m_Param.m_PlayerDirection = P_DIRECTION.RIGHT;
            m_speedDirection = SetDirectionSpeed(0.0f);
            SetPrevState(this);
            SetNextState(this);
        }
        #endregion

        /// <summary>
        /// 左右入力無しの時の処理
        /// </summary>
        public override void NoMoveInput()
        {
            m_speedDirection = SetDirectionSpeed(0.0f);
            SetPrevState(this);
            SetNextState(this);
        }

        #region//攻撃入力時の処理
        /// <summary>
        /// 攻撃入力時の処理
        /// </summary>
        public override void AttackKeyDownInput() { }
        public override void AttackKeyHoldInput() { }
        public override void AttackKeyUpInput() { }
        #endregion

        #region//ジャンプ入力時の処理
        /// <summary>
        /// ジャンプ入力時の処理
        /// </summary>
        public override void JumpKeyDownInput()
        {
            if (m_Param.m_playerStyle==P_STYLE.CLAW)/*ここにClawStyleの時のみtrueの条件を入れる*/
            {
                if (m_Param.m_PlayerGround == P_GROUND.JUMP_1ST || m_Param.m_PlayerGround == P_GROUND.FALL)
                {

                m_Param.m_PlayerState = P_STATE.RISE;
                m_Param.m_PlayerGround = P_GROUND.JUMP_2ND;
                SetPrevState(this);
                SetNextState(m_riseState);
                }
            }
        }
        public override void JumpKeyHoldInput() { }
        public override void JumpKeyUpInput() { }
        #endregion

        /// <summary>
        /// 次スタイルチェンジ入力時の処理
        /// </summary>
        public override void NextStyleInput() { }

        /// <summary>
        /// 前スタイルチェンジ入力時の処理
        /// </summary>
        public override void PrevStyleInput() { }

    }//    public class FallState : PlayerState END
}//namespace TeamProject END
