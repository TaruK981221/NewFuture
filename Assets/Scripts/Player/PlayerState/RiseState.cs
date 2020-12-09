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
        private float m_jumpMaxHeight;
        //最大時間
        private float m_jumpMaxTime = 0.0f;
        //最低ジャンプ時間
        private float m_jumpMinTime = 0.0f;

        //ジャンプ開始地点登録フラグ
        private bool m_jumpHeightSetFlag = false;
        //ジャンプボタンリリースフラグ
        private bool m_jumpKeyReleaseFlag = false;

        //方向によるスピード変化用（左：-1、右：+1、入力無し：0）
        private float m_speedDirection = 0.0f;

        public RiseState()
        {
            SetNextState(this);
            SetPrevState(this);
        }

        override public void SetSelfState() { SelfState = P_STATE.RISE; }

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
                SetIsHead(false);
                ChangeFallState();

                return true;
            }
            if (m_Position.y > m_jumpMaxHeight + m_StartHeight2)
            {
                Debug.Log("最高到達点:RISEステート");
                ChangeFallState();

                return true;

            }
            if (m_timer > m_jumpMaxTime)
            {
                Debug.Log("滞空時間終了:RISEステート");
                ChangeFallState();
                return true;

            }
            if (m_jumpKeyReleaseFlag && m_timer > m_jumpMinTime)
            {
                Debug.Log("最低jump時間");
                ChangeFallState();
                return true;

            }
            PositionUpdate();

            m_timer += Time.deltaTime;
            return false;
        }
        public override void SetSpeed()
        {
            m_speed.x = m_speedDirection * m_horizontalSpeed;
            float animationCurve = m_jumpAnimCurve.Evaluate(m_timer);
            m_speed.y = 1.0f * animationCurve * m_jumpSpeed;
        }

        override public void SetJumpParameter(float _maxheight, float _maxtime, float _mintime)
        {
            //最大ジャンプ高さ
            m_jumpMaxHeight = _maxheight;
            //最大時間
            m_jumpMaxTime = _maxtime;
            //最低時間
            m_jumpMinTime = _mintime;

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
        public override void JumpKeyDownInput() { }
        public override void JumpKeyHoldInput()
        {
            m_Param.m_PlayerState = P_STATE.RISE;

            SetNextState(this);
            SetPrevState(this);

            Debug.Log("jump:JUMP");
        }
        public override void JumpKeyUpInput()
        {
            m_jumpKeyReleaseFlag = true;
        }
        #endregion

        /// <summary>
        /// 次スタイルチェンジ入力時の処理
        /// </summary>
        public override void NextStyleInput()
        { }

        /// <summary>
        /// 前スタイルチェンジ入力時の処理
        /// </summary>
        public override void PrevStyleInput()
        { }


        public void SetJumpStartPos(float _jumpstartpos)
        {
            m_StartHeight2 = _jumpstartpos;
        }

        void SetJumpStartFlag(bool _flag)
        {
            m_jumpHeightSetFlag = _flag;
        }

        private void ChangeFallState()
        {
            //タイマーカウントリセット
            m_timer = 0.0f;
            m_jumpKeyReleaseFlag = false;
            //スピードリセット
            m_speedDirection = SetDirectionSpeed(0.0f);


            m_Param.m_PlayerState = P_STATE.FALL;
            SetPrevState(this);
            SetNextState(m_fallState);
            SetJumpStartFlag(false);
        }
    }//    public class RiseState : PlayerState END
}//namespace TeamProject END
