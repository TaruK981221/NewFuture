using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamProject
{
    public class Player : MonoBehaviour
    {
        //ここはプレイヤーの状態

        //実際にプレイヤーの状態を受け取るモノ
        [SerializeField, Header("スタイル")]
        private P_STYLE m_style;
        [SerializeField, Header("状態")]
        private P_STATE m_state;
        [SerializeField, Header("接地状況")]
        private P_GROUND m_ground;
        [SerializeField, Header("方向")]
        private P_DIRECTION m_direction;

        [Header("プレイヤーの開始時の設定")]
        public P_STYLE     m_startStyle;
        public P_STATE     m_startState;
        public P_GROUND    m_startGround;
        public P_DIRECTION m_startDirection;

        public PlayerState m_playerState;
        private PlayerState m_playerPrevState;

        [Header("座標計算用速度")]
        //public Vector2 m_speed;
       // public GameObject m_Player;
        private GameObject m_playerSpriteObj;
        private PlayerAnimation m_playerAnim;
        //前フレームの座標
        public Vector3 m_prevPosition;

        [Header("スタイルチェンジにかかる時間・仮")]
        public float m_styleChangeTime;
        public float m_timeCount;


        //インスペクターで設定する
        [SerializeField, Header("水平速度")]
        private float speed = 0.0f;
        [SerializeField, Header("重力速度")]
        private float gravity = 0.0f;
        [SerializeField, Header("ジャンプ速度")]
        private float jumpSpeed = 0.0f;
        [SerializeField, Header("ジャンプ高さ制限")]
        private float jumpHeight = 0.0f;
        [SerializeField, Header("ジャンプ制限時間")]
        private float jumpLimitTime = 0.0f;
        [SerializeField, Header("最低ジャンプ時間")]
        private float jumpMinTime = 0.0f;
        [Header("水平速度の挙動")]
        public AnimationCurve horizonSpCurve;
        [Header("ジャンプ速度の挙動")]
        public AnimationCurve jumpSpCurve;

        [Header("当たり判定用オブジェクトをアタッチ")]
        public GroundCheck_k ground; //接地判定
        public GroundCheck_k head;//頭ぶつけた判定
        //public GroundCheck_k wallL;//頭ぶつけた判定
        //public GroundCheck_k wallR;
        //頭ぶつけた判定
        public GroundCheck_k[] wall_Collision = new GroundCheck_k[(int)P_DIRECTION.MAX_DIRECT];

        //プライベート変数
        //private Animator anim = null;
        private Rigidbody2D rb = null;
        //床との当たり判定用フラグ
        public bool isGround = false;
        //天井との当たり判定用フラグ
        public bool isHead = false;
        //壁との当たり判定用フラグ
        public bool[] isWalls = new bool[(int)P_DIRECTION.MAX_DIRECT];

        public GameObject MagicAttackCollisionObj;
        public GameObject ScytheAttackCollisionObj;
        public bool flag = false;

        private void Awake()
        {
            //コンポーネントのインスタンスを捕まえる
            rb = GetComponent<Rigidbody2D>();
            m_playerAnim = GetComponent<PlayerAnimation>();
            m_playerState = new IdleState();
            m_playerPrevState = m_playerState;

            if (ground == null)
            {
                Debug.LogError("床との判定用オブジェクトアタッチし忘れ");
            }
            if (head == null)
            {
                Debug.LogError("天井との判定用オブジェクトアタッチし忘れ");
            }
            if (wall_Collision[(int)P_DIRECTION.RIGHT] ==null)
            {
                Debug.LogError("右側壁接触判定用オブジェクトアタッチし忘れ");
            }
            if (wall_Collision[(int)P_DIRECTION.LEFT] ==null)
            {
                Debug.LogError("左側壁接触判定用オブジェクトアタッチし忘れ");
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            FadeManager.FadeIn(0.5f);
          　m_state = m_startState;
            m_ground = m_startGround;
            m_direction = m_startDirection;
            m_style = m_startStyle;
            //m_Player = this.gameObject;

            SetParameter();

            //スプライトのオブジェクトを取得（左右の向きを切り替えるために）
            m_playerSpriteObj = transform.GetChild(0).gameObject;
            Vector3 vec3work = this.transform.position;

            m_playerState.SetPosition(vec3work);
            //速度のセット
            m_playerState.SetBaseSpeed(speed, gravity, jumpSpeed);
            m_playerState.SetSpeed();
            //  //スタイルアニメーションの変更
            m_playerAnim.ChangeStyleAnimation((int)m_startStyle);

            m_playerState.SetAnimationCurve(horizonSpCurve, jumpSpCurve);
    }

    // Update is called once per frame
    void Update()
        {
            //前フレームの座標をセット
            m_prevPosition = this.transform.position;

            //アニメーションのリセット
            m_playerAnim.AnimOFF(m_playerState.GetCurrentState());
          //プレイヤーの入力確認
            bool keyinput = m_playerState.PlayerInput();

            Vector3 vec3work = this.transform.position;


            //キー入力されたら行動する
            //m_Position = Vector3.zero;
            //入力を受けたかどうか
            if (keyinput)
            {

                Debug.Log("playerState:MAIN:" + m_playerState);
                GetParameter();
                //GetParameter(m_playerState.m_Param);
                m_playerState.NextState.SetPosition(vec3work);
                ChangeState(m_playerState.NextState);
                SetParameter();
                m_playerState.SetJumpParameter(jumpHeight, jumpLimitTime, jumpMinTime);

            }

            //速度のセット
            m_playerState.SetBaseSpeed(speed, gravity, jumpSpeed);
            m_playerState.SetSpeed();

            //プレイヤーの更新処理
            bool stateUpdate = m_playerState.Update();
            if (stateUpdate)
            {
                GetParameter();
                //GetParameter(m_playerState.m_Param);
                m_playerState.NextState.SetPosition(vec3work);
                ChangeState(m_playerState.NextState);
                SetParameter();

            }

            //後で整理していきたい部分
            ////スタイル変更があれば
            //if (m_playerState!=m_playerState.NextState)
            //{
            //    GetParameter();
            //    //m_playerState.NextState.SetPosition(vec3work);
            //    ChangeState(m_playerState.NextState);
            //    SetParameter();
            //     m_playerState.SetJumpParameter(jumpHeight, jumpLimitTime, jumpMinTime);
            //}
            //else{   m_direction = m_playerState.m_Param.m_PlayerDirection;
            //}

            //接地判定を得る
            isGround = ground.IsGround();
            //天井判定を得る
            isHead = head.IsGround();
            m_playerState.SetIsGround(isGround);
            m_playerState.SetIsHead(isHead);


            //座標の更新
            vec3work = m_playerState.GetPosition();
            this.transform.position = vec3work;
            //m_Position = m_playerState.GetPosition();
            m_state = m_playerState.GetCurrentState();

            //壁接触判定
            WallCollisionCheck();

            //プレイヤーの向きを変更する
            DirectionChange();
            
            //状態ごとのオブジェクト処理

            if (m_playerState.GetCurrentState() == P_STATE.ATTACK)
            {
                switch (m_style)
                {
                    case P_STYLE.SCYTHE:
                        Debug.Log("SCYTHESTYLE:" + m_style);

                        break;
                    case P_STYLE.CLAW:
                        Debug.Log("CLAWSTYLE:" + m_style);
                        break;
                    case P_STYLE.MAGIC:
                        Debug.Log("MAGICSTYLE:" + m_style);
                //  if (!flag)
                //{
                //    flag = true;
                //    Vector3 pospos = new Vector3(20, 50, 0);
                //    Vector3 pos = this.transform.position + pospos;
                //Instantiate(Object_A, pos, new Quaternion(0, 0, 0, 0));
                //}
                      break;
                    case P_STYLE.MAX_STYLE:
                        break;
                    default:
                        break;
                }
                //攻撃
            }
            //Debug.Log("m_endAnimation:::" + m_playerState.m_endAnimation);
            Debug.Log("EndAnimation():::" + m_playerSpriteObj.transform.GetComponent<IsAnimationCheck>().EndAnimation());
            bool finishStyleChange = m_playerSpriteObj.transform.GetComponent<IsAnimationCheck>().EndAnimation();
            m_playerState.SetEndAnimFlag(finishStyleChange);
           // Debug.Log("finishStyleChange==" + finishStyleChange);
           if (finishStyleChange)
            {
                //スタイルアニメーションの変更
                m_playerAnim.ChangeStyleAnimation((int)m_style);
                //アニメーションのリセット
                m_playerAnim.AnimOFF(m_playerState.GetCurrentState());

            }
            //アニメーションの更新
            m_playerAnim.AnimON(m_playerState.GetCurrentState());

        }//void Update() END


        private void ChangeState(PlayerState _state)
        {
            if (_state == null)
            {
                return;
            }
            m_playerState = _state;
        }
        private void GetParameter(P_PARAMETER _param)
        {

            m_state = _param.m_PlayerState;
            m_ground = _param.m_PlayerGround;
            m_direction = _param.m_PlayerDirection;
            m_style = _param.m_playerStyle;

        }
        private void GetParameter()
        {

            m_state = m_playerState.m_Param.m_PlayerState;
            m_ground = m_playerState.m_Param.m_PlayerGround;
            m_direction = m_playerState.m_Param.m_PlayerDirection;
            m_style = m_playerState.m_Param.m_playerStyle;

        }
        private void SetParameter()
        {
            m_playerState.m_Param.m_PlayerState = m_state;
            m_playerState.m_Param.m_PlayerGround = m_ground;
            m_playerState.m_Param.m_PlayerDirection = m_direction;
            m_playerState.m_Param.m_playerStyle = m_style;
        }

        /// <summary>
        /// プレイヤーのスプライトのみかけの向きを切り替える
        /// </summary>
        private void DirectionChange()
        {
            Vector3 spritescale_work = m_playerSpriteObj.transform.localScale;
            //方向の更新（逆向きなら変更する）
            switch (m_direction)
            {
                case P_DIRECTION.RIGHT:
                    if (spritescale_work.x > 0.0f)
                    {
                        spritescale_work.x *= -1.0f;

                        m_playerSpriteObj.transform.localScale = spritescale_work;
                    }
                    break;
                case P_DIRECTION.LEFT:
                    if (spritescale_work.x < 0.0f)
                    {
                        spritescale_work.x *= -1.0f;

                        m_playerSpriteObj.transform.localScale = spritescale_work;
                    }
                    break;
            }

        }

        private void WallCollisionCheck()
        {
            //作業用
            Vector3 vec3work;
            //壁接触判定を得る
            isWalls[(int)P_DIRECTION.RIGHT] = wall_Collision[(int)P_DIRECTION.RIGHT].IsGround();
            isWalls[(int)P_DIRECTION.LEFT] = wall_Collision[(int)P_DIRECTION.LEFT].IsGround();
            //壁接触後の座標調整
            switch (m_direction)
            {
                case P_DIRECTION.RIGHT:
                    if (isWalls[(int)P_DIRECTION.RIGHT])
                    {
                        Debug.Log("右壁接触してます");
                        vec3work = new Vector3(m_prevPosition.x, this.transform.position.y, this.transform.position.z);
                        this.transform.position = vec3work;
                    }
                    break;
                case P_DIRECTION.LEFT:
                    if (isWalls[(int)P_DIRECTION.LEFT])
                    {
                        Debug.Log("左壁接触してます");
                        vec3work = new Vector3(m_prevPosition.x, this.transform.position.y, this.transform.position.z);
                        this.transform.position = vec3work;
                    }
                    break;
            }

        }
    }//    public class Player : MonoBehaviour END
}//namespace TeamProject END

