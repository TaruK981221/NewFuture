using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamProject
{
    public class Player : MonoBehaviour
    {
        //ここはプレイヤーの状態
        [Header("")]
        public P_STATE m_state;
        public P_GROUND m_ground;
        public P_DIRECTION m_direction;
        public P_STYLE m_style;

        public PlayerState m_playerState;
        private PlayerState m_playerPrevState;

        [Header("座標計算用速度")]
        public Vector2 m_speed;
        [Header("追加する速度(移動,ジャンプ,落下)")]
        public P_ADDSPEED m_addSpeed;
        //public Vector2 m_addSpeed;
        public GameObject m_Player;
        private PlayerAnimation m_playerAnim;
        public Vector3 m_Position;

        [Header("スタイルチェンジにかかる時間・仮")]
        public float m_styleChangeTime;
        public float m_timeCount;


        //インスペクターで設定する
        public float speed;     //速度
        public float gravity;   //重力
        public float jumpSpeed; //ジャンプする速度
        [Header("ジャンプ高さ制限")]
        public float jumpHeight;
        [Header("ジャンプ制限時間")]
        public float jumpLimitTime;
        [Header("最低ジャンプ時間")]
        public float jumpMinTime;

        public GroundCheck_k ground; //接地判定
        public GroundCheck_k head;//頭ぶつけた判定

        //プライベート変数
        //private Animator anim = null;
        private Rigidbody2D rb = null;
        public bool isGround = false;
        public bool isHead = false;
        //private bool isJump = false;
        private float jumpPos = 0.0f;
        private float jumpTime = 0.0f;
        // Start is called before the first frame update
        void Start()
        {
            m_state = P_STATE.IDLE;
            m_ground = P_GROUND.GROUND;
            m_direction = P_DIRECTION.RIGHT;
            m_style = P_STYLE.BLADE;
            m_Player = this.gameObject;

            //コンポーネントのインスタンスを捕まえる
            //anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            m_playerAnim = GetComponent<PlayerAnimation>();
            m_playerState = new IdleState();
            m_playerPrevState = m_playerState;

            Vector3 vec3work = this.transform.position;
            m_playerState.SetPosition(vec3work);
            //速度のセット
            m_playerState.SetBaseSpeed(speed, gravity, jumpSpeed);
            m_playerState.SetSpeed();

        }

        // Update is called once per frame
        void Update()
        {
            //プレイヤーの入力確認
            bool keyinput = m_playerState.PlayerInput();
            Vector3 vec3work = this.transform.position;
            //アニメーションのリセット
            m_playerAnim.AnimOFF(m_playerState.GetCurrentState());


            //キー入力されたら行動する
            //m_Position = Vector3.zero;
            //入力を受けたかどうか
            if (keyinput)
            {

                Debug.Log("playerState:MAIN:" + m_playerState);
                GetParameter(m_playerState.m_Param);
                m_playerState.m_nextState.SetPosition(vec3work);
                ChangeState(m_playerState.m_nextState);
                SetParameter();
                m_playerState.SetJumpParameter(jumpHeight, jumpLimitTime, jumpMinTime);
                //

                //m_playerState.SetPosition(vec3work);
                //m_playerState.SetMaxJumpHeight(jumpHeight);
                //m_playerState.SetMaxJumpTime(jumpLimitTime);
                //if (m_playerState == PlayerState.m_riseState)
                //{
                //    m_playerState.SetJumpStartPos(m_Player.transform.position.y);
                //}
            }

            //m_playerState.SetPosition(vec3work);
            //m_speed = m_playerState.SetSpeed(m_addSpeed);
            //速度のセット
            m_playerState.SetBaseSpeed(speed, gravity, jumpSpeed);
            m_playerState.SetSpeed();

            //プレイヤーの更新処理
            bool stateUpdate = m_playerState.Update();
            if (stateUpdate)
            {
                GetParameter(m_playerState.m_Param);
                m_playerState.m_nextState.SetPosition(vec3work);
                ChangeState(m_playerState.m_nextState);
                SetParameter();

                //m_speed = m_playerState.SetSpeed(m_addSpeed);

                //m_playerState.SetPosition(vec3work);
           }
            //接地判定を得る
            isGround = ground.IsGround();
            isHead = head.IsGround();
            m_playerState.SetIsGround(isGround);
            m_playerState.SetIsHead(isHead);


            //座標の更新
            vec3work = m_playerState.GetPosition();
            m_Player.transform.position = vec3work;
            //m_Position = m_playerState.GetPosition();
            m_state = m_playerState.GetCurrentState();
            //アニメーションの更新
            m_playerAnim.AnimON(m_playerState.GetCurrentState());

        }

        //更新関数
        private void IdleUpdate()
        {
            //右入力
            if (InputManager.InputManager.Instance.GetArrow(InputManager.ArrowCode.RightArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                m_state = P_STATE.RUN;
                m_direction = P_DIRECTION.RIGHT;

            }
            else if (InputManager.InputManager.Instance.GetArrow(InputManager.ArrowCode.LeftArrow) || Input.GetKey(KeyCode.LeftArrow))
            {            //左入力

                m_state = P_STATE.RUN;
                m_direction = P_DIRECTION.LEFT;

            }
            //入力
            if (InputManager.InputManager.Instance.GetKeyDown(InputManager.ButtonCode.Jump))
            {
                m_state = P_STATE.RISE;
                m_speed.y = jumpSpeed;
                jumpPos = transform.position.y; //ジャンプした位置を記録する

                m_ground = P_GROUND.JUMP_1ST;
                //isJump = true;

                jumpTime = 0.0f;

            }
            //入力
            if (InputManager.InputManager.Instance.GetKeyDown(InputManager.ButtonCode.Attack))
            {
                m_state = P_STATE.ATTACK;

                ChangeState(new AttackState());

            }
            //入力
            if (InputManager.InputManager.Instance.GetKeyDown(InputManager.ButtonCode.StyleNext))
            {
                m_state = P_STATE.STYLE_CHANGE_NEXT;

            }

        }
        private void RunUpdate()
        {
            //右入力
            if (InputManager.InputManager.Instance.GetArrow(InputManager.ArrowCode.RightArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                //m_Position.x += Time.deltaTime * m_speed.x;
                //m_Player.transform.position += m_Position;
                m_speed.x = speed;

                m_direction = P_DIRECTION.RIGHT;

            }
            else if (InputManager.InputManager.Instance.GetArrow(InputManager.ArrowCode.LeftArrow) || Input.GetKey(KeyCode.LeftArrow))
            {            //左入力

                //m_Position.x -= Time.deltaTime * m_speed.x;
                //m_Player.transform.position += m_Position;
                m_speed.x = -1.0f * speed;

                m_direction = P_DIRECTION.LEFT;

            }
            else
            {
                m_state = P_STATE.IDLE;

            }
            //入力
            if (InputManager.InputManager.Instance.GetKeyDown(InputManager.ButtonCode.Jump))
            {
                m_state = P_STATE.RISE;

            }
            //入力
            if (InputManager.InputManager.Instance.GetKeyDown(InputManager.ButtonCode.Attack))
            {
                m_state = P_STATE.ATTACK;

            }
            //入力
            if (InputManager.InputManager.Instance.GetKeyDown(InputManager.ButtonCode.StyleNext))
            {
                m_state = P_STATE.STYLE_CHANGE_NEXT;
            }
        }
        private void JumpUpdate()
        {
            //上方向キーを押しているか
            bool pushUpKey = InputManager.InputManager.Instance.GetKey(InputManager.ButtonCode.Jump);
            //現在の高さが飛べる高さより下か
            bool canHeight = (jumpPos + jumpHeight) > transform.position.y;
            //ジャンプ時間が長くなりすぎてないか
            bool canTime = jumpLimitTime > jumpTime;

            //入力
            if (pushUpKey && canHeight && canTime)// && !isHead)
            {
                m_state = P_STATE.RISE;
                // m_PlayerDirection = P_DIRECTION.RIGHT;

                m_speed.y = jumpSpeed;

                jumpTime += Time.deltaTime;
            }
            else
            {
                if (isGround)
                {
                    m_state = P_STATE.IDLE;

                    m_ground = P_GROUND.GROUND;
                    jumpTime = 0.0f;

                }
                m_speed.y = -gravity;

            }

            //右入力
            if (InputManager.InputManager.Instance.GetArrow(InputManager.ArrowCode.RightArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                //m_Position.x += Time.deltaTime * m_speed.x;
                //m_Player.transform.position += m_Position;
                //m_PlayerState = P_STATE.RUN;
                m_speed.x = speed;
                m_direction = P_DIRECTION.RIGHT;

            }
            else if (InputManager.InputManager.Instance.GetArrow(InputManager.ArrowCode.LeftArrow) || Input.GetKey(KeyCode.LeftArrow))
            {            //左入力

                //m_Position.x -= Time.deltaTime * m_speed.x;
                //m_Player.transform.position += m_Position;
                m_speed.x = -1.0f * speed;
                // m_PlayerState = P_STATE.RUN;
                m_direction = P_DIRECTION.LEFT;

            }
            //else
            //{
            //    m_PlayerState = P_STATE.IDLE;

            //}
            //入力
            if (InputManager.InputManager.Instance.GetKeyDown(InputManager.ButtonCode.Attack))
            {
                m_state = P_STATE.JUMP_ATTACK;

            }
            //入力
            if (InputManager.InputManager.Instance.GetKeyDown(InputManager.ButtonCode.StyleNext))
            {
            }
        }
        private void RiseUpdate()
        {
            //上方向キーを押しているか
            bool pushUpKey = InputManager.InputManager.Instance.GetKey(InputManager.ButtonCode.Jump);
            //現在の高さが飛べる高さより下か
            bool canHeight = (jumpPos + jumpHeight) > transform.position.y;
            //ジャンプ時間が長くなりすぎてないか
            bool canTime = jumpLimitTime > jumpTime;

            //入力
            if (pushUpKey && canHeight && canTime)// && !isHead)
            {
                m_state = P_STATE.RISE;
                // m_PlayerDirection = P_DIRECTION.RIGHT;

                m_speed.y = jumpSpeed;

                jumpTime += Time.deltaTime;
            }
            else
            {
                if (isGround)
                {
                    m_state = P_STATE.IDLE;

                    m_ground = P_GROUND.GROUND;
                    jumpTime = 0.0f;

                }
                m_speed.y = -gravity;

            }

            //右入力
            if (InputManager.InputManager.Instance.GetArrow(InputManager.ArrowCode.RightArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                //m_Position.x += Time.deltaTime * m_speed.x;
                //m_Player.transform.position += m_Position;
                //m_PlayerState = P_STATE.RUN;
                m_speed.x = speed;
                m_direction = P_DIRECTION.RIGHT;

            }
            else if (InputManager.InputManager.Instance.GetArrow(InputManager.ArrowCode.LeftArrow) || Input.GetKey(KeyCode.LeftArrow))
            {            //左入力

                //m_Position.x -= Time.deltaTime * m_speed.x;
                //m_Player.transform.position += m_Position;
                m_speed.x = -1.0f * speed;
                // m_PlayerState = P_STATE.RUN;
                m_direction = P_DIRECTION.LEFT;

            }
            //else
            //{
            //    m_PlayerState = P_STATE.IDLE;

            //}
            //入力
            if (InputManager.InputManager.Instance.GetKeyDown(InputManager.ButtonCode.Attack))
            {
                m_state = P_STATE.JUMP_ATTACK;

            }
            //入力
            if (InputManager.InputManager.Instance.GetKeyDown(InputManager.ButtonCode.StyleNext))
            {
            }
        }
        private void AttackUpdate()
        {
            //ひとまず時間で待機に戻る処理
            ToIdling();
            //入力
            if (InputManager.InputManager.Instance.GetKeyDown(InputManager.ButtonCode.Attack))
            {

            }
            else
            {
                m_state = P_STATE.IDLE;

            }
        }
        private void JumpAttackUpdate()
        {
            //ひとまず時間で待機に戻る処理
            ToIdling();
        }
        private void DamageUpdate()
        {
            //ひとまず時間で待機に戻る処理
            ToIdling();

        }
        private void StyleChangeUpdate()
        {
            //ひとまず時間で待機に戻る処理
            m_timeCount += Time.deltaTime;
            if (m_timeCount > m_styleChangeTime)
            {
                m_timeCount = 0.0f;
                StyleChange();
                m_state = P_STATE.IDLE;

            }
        }

        private void StyleChange()
        {
            switch (m_style)
            {
                case P_STYLE.BLADE:
                    m_style = P_STYLE.SPEED;
                    break;
                case P_STYLE.SPEED:
                    m_style = P_STYLE.MAGIC;
                    break;
                case P_STYLE.MAGIC:
                    m_style = P_STYLE.BLADE;
                    break;
                case P_STYLE.MAX_STYLE:
                    break;

            }
        }

        //ひとまずの時間で待機に戻る処理
        private void ToIdling()
        {
            m_timeCount += Time.deltaTime;
            if (m_timeCount > m_styleChangeTime)
            {
                m_timeCount = 0.0f;
                m_state = P_STATE.IDLE;
                m_playerState = new IdleState();

            }
        }

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
        private void SetParameter()
        {
            m_playerState.m_Param.m_PlayerState = m_state;
            m_playerState.m_Param.m_PlayerGround = m_ground;
            m_playerState.m_Param.m_PlayerDirection = m_direction;
            m_playerState.m_Param.m_playerStyle = m_style;
        }

    }//    public class Player : MonoBehaviour END
}//namespace TeamProject END

