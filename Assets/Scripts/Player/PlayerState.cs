using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TeamProject
{
    /// <summary>
    /// プレイヤーの状態の基底クラス
    /// </summary>
    public class PlayerState
    {
        //各スタイルのクラスの実体をスタティックで用意する
        public static IdleState m_idleState = new IdleState();
        public static AttackState m_attackState = new AttackState();
        public static RunState m_runState = new RunState();
        public static RiseState m_riseState = new RiseState();
        public static FallState m_fallState = new FallState();
        public static JumpAttackState m_jumpattackState = new JumpAttackState();
        public static DamageState m_damageState = new DamageState();
        public static NextStyleChangeState m_nextStyleChangeState = new NextStyleChangeState();
        public static PrevStyleChangeState m_prevStyleChangeState = new PrevStyleChangeState();
        //座標受け取り用
        static public Vector3 m_Position;
        //速度用
       static public Vector2 m_speed;
        //水平速度
        public float m_horizontalSpeed = 0.0f;
        //重力速度
        public float m_gravitySpeed = 0.0f;
        //ジャンプする速度
        public float m_jumpSpeed = 0.0f;

        //前の状態を持たせるもの
        public PlayerState PrevState { get; set; }

        //次の状態を持たせるもの
        public PlayerState NextState { get; set; }

        //自分の状態を明示する変数
        public P_STATE SelfState { get; set; }
        //ここはプレイヤーの状態
        public P_PARAMETER m_Param;

        //着地してるかどうか
        static public bool m_isGround = false;

        //天井に当たっているかどうか
        static public bool m_isHead = false;

        //ジャンプした時の高さ
        public float m_StartHeight;
        //最大ジャンプ高さ受け取り用
        public float m_jumpHeight;

        //時間計測用
        public float m_timer = 0.0f;
        //最大時間
        public float m_maxTime = 0.0f;

        //
        static public InputFlag attack = new InputFlag();
        static public InputFlag jump = new InputFlag();
        static public InputFlag LArrow = new InputFlag();
        static public InputFlag RArrow = new InputFlag();

        //アニメーション終了フラグ
        /*static*/ public bool m_endAnimation = false;

       static protected AnimationCurve m_horisontalAnimCurve;
       static protected AnimationCurve m_jumpAnimCurve;
        public PlayerState()
        {
            SetSelfState();
        }

        //======================================
        //オーバライド用関数

        /// <summary>
        /// 自分の状態をセットしておく
        /// </summary>
        virtual public void SetSelfState() { }

        // Update is called once per frame
        virtual public bool Update()
        {
            Debug.Log("Update:PLAYER");
            return false;
        }

        /// <summary>
        /// 入力処理
        /// </summary>
        /// <returns></returns>
        virtual public bool PlayerInput()
        {

            Debug.Log("PInput:PLAYER");
            bool keyinput = false;
            // bool flag = false;
            //各入力フラグの確認
            attack.KeyFlagCheck(attack, InputManager.ButtonCode.Attack);
            jump.KeyFlagCheck(jump, InputManager.ButtonCode.Jump);
            LArrow.KeyFlagCheck(LArrow, KeyCode.LeftArrow);
            RArrow.KeyFlagCheck(RArrow, KeyCode.RightArrow);

            //攻撃入力
            if (AttackKeyInput())
            {
                return true;
            }

            //右入力
            bool isRArrow = RightKeyInput();
            //左入力
            bool isLArrow = LeftKeyInput();
            if (isRArrow)
            {
                keyinput = true;
            }
            else if (isLArrow)
            {
                keyinput = true;
            }
            //左右入力無しの時
            else if (!isRArrow && !isLArrow)
            {
                NoMoveInput();
            }


            //ジャンプ入力
            if (JumpKeyInput())
            {
                keyinput = true;
            }

            bool nextStyle_key = InputManager.InputManager.Instance.GetKeyDown(InputManager.ButtonCode.StyleNext);
            bool prevStyle_key = InputManager.InputManager.Instance.GetKeyDown(InputManager.ButtonCode.StylePrev);

            //次スタイルチェンジ入力
            if (nextStyle_key)
            {
                NextStyleInput();
                return true;

            }
            //前スタイルチェンジ入力
            else if (prevStyle_key)
            {
                PrevStyleInput();
                return true;

            }
            //-------------------------------------------
            //ここの戻り値についてはあとで修正する事
            //-------------------------------------------

            return keyinput;
        }
        #region//左入力時の処理
        /// <summary>
        /// 左入力時の処理
        /// </summary>
        private bool LeftKeyInput()
        {
            if (LArrow.m_keyFlag[(int)KEYSTATE.DOWN])
            {
                //右入力処理
                LeftKeyDownInput();
                return true;
            }
            else if (LArrow.m_keyFlag[(int)KEYSTATE.HOLD])
            {
                //右ホールド処理
                LeftKeyHoldInput();
                return true;
            }
            else if (LArrow.m_keyFlag[(int)KEYSTATE.UP])
            {
                //右リリース処理
                LeftKeyUpInput();
                return true;
            }
            return false;
        }
        virtual public void LeftKeyDownInput() { }
        virtual public void LeftKeyHoldInput() { }
        virtual public void LeftKeyUpInput() { }
        #endregion

        #region//右入力時の処理
        /// <summary>
        /// 右入力時の処理
        /// </summary>
        private bool RightKeyInput()
        {
            if (RArrow.m_keyFlag[(int)KEYSTATE.DOWN])
            {
                //右入力処理
                RightKeyDownInput();
                return true;
            }
            else if (RArrow.m_keyFlag[(int)KEYSTATE.HOLD])
            {
                //右ホールド処理
                RightKeyHoldInput();
                return true;
            }
            else if (RArrow.m_keyFlag[(int)KEYSTATE.UP])
            {
                //右リリース処理
                RightKeyUpInput();
                return true;
            }
            return false;
        }
        virtual public void RightKeyDownInput() { }
        virtual public void RightKeyHoldInput() { }
        virtual public void RightKeyUpInput() { }
        #endregion

        /// <summary>
        /// 左右入力無しの時の処理
        /// </summary>
        virtual public void NoMoveInput() { }

        #region//攻撃入力時の処理
        /// <summary>
        /// 攻撃入力時の処理
        /// </summary>
        private bool AttackKeyInput()
        {
            if (attack.m_keyFlag[(int)KEYSTATE.DOWN])
            {

                //攻撃ボタン入力処理
                AttackKeyDownInput();
                //溜め攻撃用タイマーのカウントリセット
                //timer=0.0f;
                return true;
            }
            else if (attack.m_keyFlag[(int)KEYSTATE.HOLD])
            {
                //溜め攻撃用タイマーのカウントアップ
                //timer+=Time.deltaTime;
                return false;
            }
            else if (attack.m_keyFlag[(int)KEYSTATE.UP])
            {

                //攻撃ボタンリリース処理
                AttackKeyUpInput();
                //溜め攻撃用タイマーのカウントリセット
                //timer=0.0f;
                return true;
            }
            return false;
        }
        virtual public void AttackKeyDownInput() { }
        virtual public void AttackKeyHoldInput() { }
        virtual public void AttackKeyUpInput() { }
        #endregion

        #region//ジャンプ入力時の処理
        /// <summary>
        /// ジャンプ入力時の処理
        /// </summary>
        private bool JumpKeyInput()
        {
            if (jump.m_keyFlag[(int)KEYSTATE.DOWN])
            {
                //ジャンプボタン入力処理
                JumpKeyDownInput();
                return true;
            }
            else if (jump.m_keyFlag[(int)KEYSTATE.HOLD])
            {
                //ジャンプボタンホールド処理
                JumpKeyHoldInput();
                return true;
            }
            else if (jump.m_keyFlag[(int)KEYSTATE.UP])
            {
                //ジャンプボタンリリース処理
                JumpKeyUpInput();
                return true;
            }
            return false;
        }
        virtual public void JumpKeyDownInput() { }
        virtual public void JumpKeyHoldInput() { }
        virtual public void JumpKeyUpInput() { }
        #endregion

        /// <summary>
        /// 次スタイルチェンジ入力時の処理
        /// </summary>
        virtual public void NextStyleInput() { }

        /// <summary>
        /// 前スタイルチェンジ入力時の処理
        /// </summary>
        virtual public void PrevStyleInput() { }

        /// <summary>
        /// スタイル変更直後の速度設定
        /// </summary>
        /// <param name="_speed"></param>
        /// <returns></returns>
        virtual public void SetSpeed() { }

        virtual public void SetJumpParameter(float _maxheight, float _maxtime, float _mintime) { }

        //オーバライド用ここまで
        //===============================================

        //-----------------------------------------------
        //共通用関数

        /// <summary>
        /// 接地判定受け取り
        /// </summary>
        /// <param name="_ground"></param>
        public void SetIsGround(bool _ground)
        {
            m_isGround = _ground;
        }
        /// <summary>
        /// 頭上接触判定受け取り
        /// </summary>
        /// <param name="_head"></param>
        public void SetIsHead(bool _head)
        {
            m_isHead = _head;
        }
        public void SetMaxJumpHeight(float _jumpheight)
        {
            m_jumpHeight = _jumpheight;
        }
        public void SetMaxJumpTime(float _jumptime)
        {
            m_maxTime = _jumptime;
        }

        /// <summary>
        /// 水平速度の設定
        /// </summary>
        /// <param name="_directionspeed"></param>
        public float SetDirectionSpeed(float _directionspeed)
        {
            return _directionspeed;
        }

        /// <summary>
        /// 次の状態をセットする
        /// </summary>
        /// <param name="_prev"></param>
        public void SetPrevState(PlayerState _prev)
        {
            PrevState = _prev;
        }
        /// <summary>
        /// 状態に変更なしの場合、現在の状態をセットする
        /// </summary>
        public void SetPrevState()
        {
            PrevState = this;
        }
        /// <summary>
        /// 次の状態をセットする
        /// </summary>
        /// <param name="_next"></param>
        public void SetNextState(PlayerState _next)
        {
            NextState = _next;
        }
        /// <summary>
        /// 状態に変更なしの場合、現在の状態をセットする
        /// </summary>
        public void SetNextState()
        {
            NextState = this;
        }

        /// <summary>
        /// 座標を受け取る
        /// </summary>
        /// <param name="_pos"></param>
        public void SetPosition(Vector3 _pos)
        {
            m_Position = _pos;
        }
        /// <summary>
        /// 座標を渡す
        /// </summary>
        /// <param name="_pos"></param>
        public Vector3 GetPosition()
        {
            return m_Position;
        }
        public void PositionUpdate()
        {
            //座標の更新
            m_Position.x += Time.deltaTime * m_speed.x;
            m_Position.y += Time.deltaTime * m_speed.y;

        }

        public void SetBaseSpeed(float _horizontal, float _gravity, float _jump)
        {
            //水平速度
            m_horizontalSpeed = _horizontal;
            //重力速度
            m_gravitySpeed = _gravity;
            //ジャンプする速度
            m_jumpSpeed = _jump;

        }

        public P_STATE GetCurrentState()
        {
            return SelfState;
        }

        public void SetParameter(PlayerState _prevstate)
        {
            PlayerState _stateWork = this;
            _stateWork = _prevstate;
            // m_nextState.m_Position = this.m_Position;
        }

        public void SetEndAnimFlag(bool _bool)
        {
            m_endAnimation = _bool;
        }

        public void ChangeNextStyle()
        {
            switch (m_Param.m_playerStyle)
            {
                case P_STYLE.SCYTHE:
                    m_Param.m_playerStyle = P_STYLE.CLAW;
                    break;
                case P_STYLE.CLAW:
                    m_Param.m_playerStyle = P_STYLE.MAGIC;
                    break;
                case P_STYLE.MAGIC:
                    m_Param.m_playerStyle = P_STYLE.SCYTHE;
                    break;
                case P_STYLE.MAX_STYLE:
                    break;

            }
        }
        public void ChangePrevStyle()
        {
            switch (m_Param.m_playerStyle)
            {
                case P_STYLE.SCYTHE:
                    m_Param.m_playerStyle = P_STYLE.MAGIC;
                    break;
                case P_STYLE.CLAW:
                    m_Param.m_playerStyle = P_STYLE.SCYTHE;
                    break;
                case P_STYLE.MAGIC:
                    m_Param.m_playerStyle = P_STYLE.CLAW;
                    break;
                case P_STYLE.MAX_STYLE:
                    break;

            }
        }

        //各アニメーションカーブをセットする
        public void SetAnimationCurve(AnimationCurve _horizontal, AnimationCurve _jump)
        {
            m_horisontalAnimCurve = _horizontal;
            m_jumpAnimCurve = _jump;
        }
        //共通用関数ここまで
        //-----------------------------------------------

    }//public class PlayerState : MonoBehaviour END
}//namespace TeamProject END


/*仮置き
          #region//左入力時の処理
        /// <summary>
        /// 左入力時の処理
        /// </summary>
      public override void LeftKeyDownInput() { }
        public override void LeftKeyHoldInput() { }
        public override void LeftKeyUpInput() { }
        #endregion

        #region//右入力時の処理
        /// <summary>
        /// 右入力時の処理
        /// </summary>
        public override void RightKeyDownInput() { }
        public override void RightKeyHoldInput() { }
        public override void RightKeyUpInput() { }
        #endregion

        /// <summary>
        /// 左右入力無しの時の処理
        /// </summary>
        public override void NoMoveInput() { }

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
        */
