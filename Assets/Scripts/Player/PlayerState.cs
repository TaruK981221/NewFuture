using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TeamProject
{
    //プレイヤーの状態
    public enum P_STATE
    {
        IDLE = 0,       //待機
        RUN,            //走る
        JUMP,           //ジャンプ
        RISE,           //上昇
        FALL,           //下降
        ATTACK,          //攻撃
        JUMP_ATTACK,          //ジャンプ攻撃
        DAMAGE,          //被ダメージ
        STYLE_CHANGE,          //スタイルチェンジ
        MAX_STATE       //全状態数
    }
    //プレイヤーの方向
    public enum P_DIRECTION
    {
        RIGHT = 0,    //右
        LEFT,         //左
        MAX_DIRECT    //全方向
    }
    //プレイヤーの接地状態
    public enum P_GROUND
    {
        GROUND = 0,    //接地
        JUMP_1ST,      //ジャンプ1回目
        JUMP_2ND,      //ジャンプ2回目
        FALL,          //落下
        MAX_STATE      //全状態数
    }

    public enum P_STYLE
    {
        BLADE = 0,
        SPEED,
        MAGIC,
        MAX_STYLE
    }

    public struct P_PARAMETER
    {
        public P_STATE m_PlayerState;
        public P_GROUND m_PlayerGround;
        public P_DIRECTION m_PlayerDirection;
        public P_STYLE m_playerStyle;

    }

    public struct P_ADDSPEED
    {
        public float runSpeed;
        public float jumpSpeed;
        public float fallSpeed;
    }
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

        //次の状態を持たせるもの
        public PlayerState m_nextState;

        //ここはプレイヤーの状態
        public P_PARAMETER m_Param;

        //着地してるかどうか
        public bool m_isGround = false;

        //天井に当たっているかどうか
        public bool m_isHead = false;

        //座標受け取り用
        public Vector2 m_pos;

        //ジャンプした時の高さ
        public float m_StartHeight;
        //最大ジャンプ高さ受け取り用
        public float m_jumpHeight;

        //時間計測用
        public float m_timer = 0.0f;
        //最大時間
        public float m_maxTime = 0.0f;
        //
        public float m_horizontalSpeed = 0.0f;

        public PlayerState()
        {
            Debug.Log("コンストラクタ:PLAYER");
        }
        //~PlayerState()
        //{
        //    Debug.Log("デストラクタ:PLAYER");

        //}



        //// Start is called before the first frame update
        //void Start()
        //{
        //    Debug.Log("Start:PLAYER");
        //}

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
            return false;
        }

        /// <summary>
        /// スタイル変更直後の速度設定
        /// </summary>
        /// <param name="_speed"></param>
        /// <returns></returns>
        virtual public Vector2 SetSpeed(P_ADDSPEED _speed)
        {
            return new Vector2(0.0f, 0.0f);
        }

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
        public void SetPos(Vector2 _pos)
        {
            m_pos = _pos;
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
        /// <param name="_speed"></param>
        public void SetHorizontalSpeed(float _speed)
        {
            m_horizontalSpeed = _speed;
        }

        public void SetNextState(PlayerState _next)
        {
            m_nextState = _next;
        }
        public void SetJumpStartPos(float _jumpstart)
        {
            m_StartHeight = _jumpstart;
        }
    }//public class PlayerState : MonoBehaviour END
}//namespace TeamProject END


