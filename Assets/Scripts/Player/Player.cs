using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
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
        public P_STYLE m_startStyle;
        public P_STATE m_startState;
        public P_GROUND m_startGround;
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
        [SerializeField, Header("無敵時間")]
        private float invincibleTime = 0.0f;
        private float invincibleCounter = 0.0f;

        [Header("当たり判定用オブジェクトをアタッチ")]
        public GroundCheck_k ground; //接地判定
        public GroundCheck_k head;//頭ぶつけた判定
        public BodyCollisionCheck enemyCollision;//敵接触判定
        //public GroundCheck_k wallR;
        //頭ぶつけた判定
        public GroundCheck_k[] wall_Collision = new GroundCheck_k[(int)P_DIRECTION.MAX_DIRECT];

        //プライベート変数
        //private Animator anim = null;
        private Rigidbody2D rb = null;
        //敵との当たり判定用フラグ
        public bool isEnemyHit = false;
        //床との当たり判定用フラグ
        public bool isGround = false;
        //天井との当たり判定用フラグ
        public bool isHead = false;
        //壁との当たり判定用フラグ
        public bool[] isWalls = new bool[(int)P_DIRECTION.MAX_DIRECT];

        //無敵中かどうか
        public bool isInvincibleflag = false;
        //スタイルチェンジ後かどうか
        public bool isStyleFlag = false;

        public GameObject bullet;
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
            if (wall_Collision[(int)P_DIRECTION.RIGHT] == null)
            {
                Debug.LogError("右側壁接触判定用オブジェクトアタッチし忘れ");
            }
            if (wall_Collision[(int)P_DIRECTION.LEFT] == null)
            {
                Debug.LogError("左側壁接触判定用オブジェクトアタッチし忘れ");
            }
            if (enemyCollision == null)
            {
                Debug.LogError("敵接触判定用オブジェクトアタッチし忘れ");
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
            //

            //アニメーションのリセット
            //m_playerAnim.AnimOFF(m_playerState.GetCurrentState());

            //各種キー入力の判定
            //プレイヤーの入力確認
            bool keyinput = m_playerState.PlayerInput();

            //直前のフレームの座標をわたす用変数
            Vector3 vec3work = this.transform.position;


            //キー入力されたら行動する
            //m_Position = Vector3.zero;

            //入力を受けたかどうか
            //特定の入力があるかどうか
            if (keyinput)
            {

                Debug.Log("playerState:MAIN:" + m_playerState);

                //プレイヤーの状態・接地状況を・方向・現在のスタイルを受け取る
                //このスクリプトに受け取る
                GetParameter();

                //GetParameter(m_playerState.m_Param);
                //次の状態の座標に現在の状態の座標をわたす
                m_playerState.NextState.SetPosition(vec3work);

                //次の状態を自分の現在の状態にセットする
                ChangeState(m_playerState.NextState);

                //
                //playerStateスクリプト側に
                //プレイヤーの状態・接地状況・方向・現在のスタイルを渡す
                SetParameter();

                //playerStateスクリプト側に
                //ジャンプの設定をわたす
                m_playerState.SetJumpParameter(jumpHeight, jumpLimitTime, jumpMinTime);

            }

            //速度のセット
            //水平速度・重力速度・上昇速度
            m_playerState.SetBaseSpeed(speed, gravity, jumpSpeed);

            //プレイヤーの状態ごとに速度を計算させる
            //水平方向と垂直方向の速度を設定する
            m_playerState.SetSpeed();

            //プレイヤーの状態ごとの更新関数の処理
            //状態に変化があればtrue返す
            bool stateUpdate = m_playerState.Update();
            if (stateUpdate)
            {
                //プレイヤーの状態・接地状況を・方向・現在のスタイルを受け取る
                //このスクリプトに受け取る2回目
                GetParameter();
                //GetParameter(m_playerState.m_Param);

                //次の状態の座標に現在の状態の座標をわたす
                m_playerState.NextState.SetPosition(vec3work);

                //次の状態を自分の現在の状態にセットする
                ChangeState(m_playerState.NextState);
                //playerStateスクリプト側に
                //プレイヤーの状態・接地状況・方向・現在のスタイルを渡す
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

            //敵接触判定を得る(毎ループ呼ぶ必要アリ)
            isEnemyHit = enemyCollision.IsCollision();
            if (isInvincibleflag)
            {
                //無敵時間中
                if (invincibleCounter > invincibleTime)
                {
                    //無敵時間終了
                    //カウンターリセット
                    invincibleCounter = 0.0f;
                    //フラグOFF
                    isInvincibleflag = false;
                    //点滅終了処理
                    m_playerSpriteObj.GetComponent<Blink>().BlinkEnd();
                }
                else
                {

                    invincibleCounter += Time.deltaTime;
                }

            }
            else
            {
                //無敵ではないとき
                if (isEnemyHit)
                {
                    Debug.Log("hitsitemasu");
                    //敵接触アリ
                    //カウンターリセット
                    invincibleCounter = 0.0f;
                    //フラグON
                    isInvincibleflag = true;
                    //点滅開始処理
                    m_playerSpriteObj.GetComponent<Blink>().BlinkStart();
                    //playerStateスクリプト側に
                    //敵接触判定情報をわたす
                    m_playerState.SetIsEnemy(isEnemyHit);

                    isEnemyHit = false;
                }
            }

            //接地判定を得る
            isGround = ground.IsGround();
            //playerStateスクリプト側に
            //接地判定情報をわたす
            m_playerState.SetIsGround(isGround);

            //天井判定を得る
            isHead = head.IsGround();
            //playerStateスクリプト側に
            //天井接触判定情報をわたす
            m_playerState.SetIsHead(isHead);



            //座標の更新
            //playerStateスクリプト側の座標を作業用変数に渡す
            vec3work = m_playerState.GetPosition();
            //作業用変数からこのスクリプトの座標に渡す
            this.transform.position = vec3work;

            //playerStateスクリプト側の状態をこのスクリプトの状態変数に渡す
            m_state = m_playerState.GetCurrentState();

            //壁接触判定
            WallCollisionCheck();

            //プレイヤーの向きを変更する
            //＝左右によってスプライトのスケールの反転
            DirectionChange();


            //----------------------
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
                        //Shot();
                        Debug.Log("MAGICSTYLE:" + m_style);
                        break;
                    case P_STYLE.MAX_STYLE:
                        break;
                    default:
                        break;
                }
                //攻撃
            }
            Animator animation = m_playerSpriteObj.GetComponent<Animator>();
            switch (m_playerState.GetCurrentState())
            {
                case P_STATE.IDLE:
                    break;
                case P_STATE.RUN:
                    break;
                case P_STATE.RISE:
                    break;
                case P_STATE.FALL:
                    break;
                case P_STATE.ATTACK:
                    //if (animation.GetBool("idle")
                    bool attack =
    m_playerSpriteObj.transform
    .GetComponent<IsAnimationCheck>()
    .EndAnimation();

                    if (attack)
                    {
                        m_playerState.StateChangeFlagOn();
                        Debug.Log("確認用" + m_playerState.GetCurrentState());
                    }
                    break;
                case P_STATE.JUMPATTACK:
                    bool jumpattack =
  m_playerSpriteObj.transform
  .GetComponent<IsAnimationCheck>()
  .EndAnimation();
                    if (jumpattack)
                    {
                        m_playerState.StateChangeFlagOn();
                        Debug.Log("確認用"+ m_playerState.GetCurrentState());
                    }
                    break;
                case P_STATE.DAMAGE:
                    break;
                case P_STATE.STYLE_CHANGE_NEXT:
                case P_STATE.STYLE_CHANGE_PREV:

                    break;
                case P_STATE.MAX_STATE:
                    break;
                default:
                    break;
            }

            //アニメーションが終わっていたらtrueを受け取る
            bool finishStyleChange =
                m_playerSpriteObj.transform
                .GetComponent<IsAnimationCheck>()
                .EndAnimation();
            //上のフラグをplayerStateスクリプト側に渡す
            //ほぼスタイルチェンジ終了確認用変数
            m_playerState.SetEndAnimFlag(finishStyleChange);
            if (finishStyleChange)
            {
                //アニメーション終了したら通る
                //スタイルアニメーションの変更
                m_playerAnim.ChangeStyleAnimation((int)m_style);

            }


            //アニメーションの更新
            //状態によって変化させる
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
            //プレイヤーの状態を受け取る
            m_state = m_playerState.m_Param.m_PlayerState;
            //接地状況を受け取る
            m_ground = m_playerState.m_Param.m_PlayerGround;
            //方向を受け取る
            m_direction = m_playerState.m_Param.m_PlayerDirection;
            //現在のスタイルを受け取る
            m_style = m_playerState.m_Param.m_playerStyle;

        }
        private void SetParameter()
        {

            //playerStateスクリプト側に
            //プレイヤーの状態を渡す
            m_playerState.m_Param.m_PlayerState = m_state;
            //接地状況を渡す
            m_playerState.m_Param.m_PlayerGround = m_ground;
            //方向を渡す
            m_playerState.m_Param.m_PlayerDirection = m_direction;
            //現在のスタイルを渡す
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

        /// <summary>
        /// 弾の生成
        /// </summary>
        private void Shot()
        {
            //ここに弾処理
            if ((m_playerState.GetCurrentState() == P_STATE.ATTACK))
            {
                //クオータニオン用意
                Quaternion abc = Quaternion.identity;

                //方向の設定（向きによって回転させる）
                switch (m_direction)
                {
                    case P_DIRECTION.RIGHT:
                        abc = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                        break;
                    case P_DIRECTION.LEFT:
                        abc = Quaternion.Euler(0.0f, 0.0f, 180.0f);
                        break;
                }
                //弾の実体の生成
                Instantiate(bullet, this.transform.position, abc);
            }
        }

    }//    public class Player : MonoBehaviour END
}//namespace TeamProject END

