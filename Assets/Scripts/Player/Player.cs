using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamProject
{
    public class Player : MonoBehaviour
    {
        //ここはプレイヤーの状態
        public P_STATE m_PlayerState;
        public P_GROUND m_PlayerGround;
        public P_DIRECTION m_PlayerDirection;
        public P_STYLE m_playerStyle;

        //
        public Vector2 m_speed;
        public GameObject m_Player;
        public Vector3 m_Position;

        [Header("スタイルチェンジにかかる時間・仮")]
        public float m_styleChangeTime;
        public float m_timeCount;


        //インスペクターで設定する
        public float speed;     //速度
        public float gravity;   //重力
        public float jumpSpeed; //ジャンプする速度
        public float jumpHeight;//高さ制限
        public float jumpLimitTime;//ジャンプ制限時間
        public GroundCheck_k ground; //接地判定
        public GroundCheck_k head;//頭ぶつけた判定

        //プライベート変数
        //private Animator anim = null;
        private Rigidbody2D rb = null;
        private bool isGround = false;
        //private bool isJump = false;
        private bool isHead = false;
        private float jumpPos = 0.0f;
        private float jumpTime = 0.0f;
        // Start is called before the first frame update
        void Start()
        {
            m_PlayerState = P_STATE.IDLE;
            m_PlayerGround = P_GROUND.GROUND;
            m_PlayerDirection = P_DIRECTION.RIGHT;
            m_playerStyle = P_STYLE.BLADE;
                        m_Player = this.gameObject;

            //コンポーネントのインスタンスを捕まえる
            //anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();

        }

        // Update is called once per frame
        void Update()
        {
            //接地判定を得る
            isGround = ground.IsGround();
            isHead = head.IsGround();

            //キー入力されたら行動する
            m_speed.x = 0.0f;
            m_speed.y = 0.0f;
            m_Position = Vector3.zero;
            switch (m_PlayerState)
            {
                case P_STATE.IDLE:
                    IdleUpdate();
                    break;
                case P_STATE.RUN:
                    RunUpdate();
                    break;

                case P_STATE.JUMP:
                    JumpUpdate();
                    break;
                case P_STATE.RISE:
                    RiseUpdate();
                    break;
                case P_STATE.FALL:
                    break;
                case P_STATE.ATTACK:
                    AttackUpdate();
                    break;
                case P_STATE.JUMP_ATTACK:
                    JumpAttackUpdate();
                    break;
                case P_STATE.DAMAGE:
                    DamageUpdate();
                    break;
                case P_STATE.STYLE_CHANGE:
                    StyleChangeUpdate();
                    break;
                case P_STATE.MAX_STATE:

                    break;
            }

            //座標の更新
            m_Position.x += Time.deltaTime * m_speed.x;
            m_Position.y += Time.deltaTime * m_speed.y;
            m_Player.transform.position += m_Position;

        }

        //更新関数
        private void IdleUpdate()
        {
            //右入力
            if (InputManager.InputManager.Instance.GetArrow(InputManager.ArrowCode.RightArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                m_PlayerState = P_STATE.RUN;
                m_PlayerDirection = P_DIRECTION.RIGHT;

            }
            else if (InputManager.InputManager.Instance.GetArrow(InputManager.ArrowCode.LeftArrow) || Input.GetKey(KeyCode.LeftArrow))
            {            //左入力

                m_PlayerState = P_STATE.RUN;
                m_PlayerDirection = P_DIRECTION.LEFT;

            }
            //入力
            if (InputManager.InputManager.Instance.GetKeyDown(InputManager.ButtonCode.Jump))
            {
                m_PlayerState = P_STATE.RISE;
                m_speed.y = jumpSpeed;
                jumpPos = transform.position.y; //ジャンプした位置を記録する

                m_PlayerGround = P_GROUND.JUMP_1ST;
                //isJump = true;

                jumpTime = 0.0f;

            }
            //入力
            if (InputManager.InputManager.Instance.GetKeyDown(InputManager.ButtonCode.Attack))
            {
                m_PlayerState = P_STATE.ATTACK;

            }
            //入力
            if (InputManager.InputManager.Instance.GetKeyDown(InputManager.ButtonCode.StyleNext))
            {
                m_PlayerState = P_STATE.STYLE_CHANGE;

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
                m_PlayerState = P_STATE.JUMP;
                // m_PlayerDirection = P_DIRECTION.RIGHT;

                m_speed.y = jumpSpeed;

                jumpTime += Time.deltaTime;
            }
            else
            {
                if (isGround)
                {
                m_PlayerState = P_STATE.IDLE;

                m_PlayerGround = P_GROUND.GROUND;
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
                m_PlayerDirection = P_DIRECTION.RIGHT;

            }
            else if (InputManager.InputManager.Instance.GetArrow(InputManager.ArrowCode.LeftArrow) || Input.GetKey(KeyCode.LeftArrow))
            {            //左入力

                //m_Position.x -= Time.deltaTime * m_speed.x;
                //m_Player.transform.position += m_Position;
                m_speed.x = -1.0f * speed;
                // m_PlayerState = P_STATE.RUN;
                m_PlayerDirection = P_DIRECTION.LEFT;

            }
            //else
            //{
            //    m_PlayerState = P_STATE.IDLE;

            //}
            //入力
            if (InputManager.InputManager.Instance.GetKeyDown(InputManager.ButtonCode.Attack))
            {
                m_PlayerState = P_STATE.JUMP_ATTACK;

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
                m_PlayerState = P_STATE.JUMP;
                // m_PlayerDirection = P_DIRECTION.RIGHT;

                m_speed.y = jumpSpeed;

                jumpTime += Time.deltaTime;
            }
            else
            {
                if (isGround)
                {
                m_PlayerState = P_STATE.IDLE;

                m_PlayerGround = P_GROUND.GROUND;
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
                m_PlayerDirection = P_DIRECTION.RIGHT;

            }
            else if (InputManager.InputManager.Instance.GetArrow(InputManager.ArrowCode.LeftArrow) || Input.GetKey(KeyCode.LeftArrow))
            {            //左入力

                //m_Position.x -= Time.deltaTime * m_speed.x;
                //m_Player.transform.position += m_Position;
                m_speed.x = -1.0f * speed;
                // m_PlayerState = P_STATE.RUN;
                m_PlayerDirection = P_DIRECTION.LEFT;

            }
            //else
            //{
            //    m_PlayerState = P_STATE.IDLE;

            //}
            //入力
            if (InputManager.InputManager.Instance.GetKeyDown(InputManager.ButtonCode.Attack))
            {
                m_PlayerState = P_STATE.JUMP_ATTACK;

            }
            //入力
            if (InputManager.InputManager.Instance.GetKeyDown(InputManager.ButtonCode.StyleNext))
            {
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

                m_PlayerDirection = P_DIRECTION.RIGHT;

            }
            else if (InputManager.InputManager.Instance.GetArrow(InputManager.ArrowCode.LeftArrow) || Input.GetKey(KeyCode.LeftArrow))
            {            //左入力

                //m_Position.x -= Time.deltaTime * m_speed.x;
                //m_Player.transform.position += m_Position;
                m_speed.x = -1.0f *speed;

                m_PlayerDirection = P_DIRECTION.LEFT;

            }
            else
            {
                m_PlayerState = P_STATE.IDLE;

            }
            //入力
            if (InputManager.InputManager.Instance.GetKeyDown(InputManager.ButtonCode.Jump))
            {
                m_PlayerState = P_STATE.JUMP;

            }
            //入力
            if (InputManager.InputManager.Instance.GetKeyDown(InputManager.ButtonCode.Attack))
            {
                m_PlayerState = P_STATE.ATTACK;

            }
            //入力
            if (InputManager.InputManager.Instance.GetKeyDown(InputManager.ButtonCode.StyleNext))
            {
                m_PlayerState = P_STATE.STYLE_CHANGE;
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
                m_PlayerState = P_STATE.IDLE;

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
                m_PlayerState = P_STATE.IDLE;

            }
        }

        private void StyleChange()
        {
            switch (m_playerStyle)
            {
                case P_STYLE.BLADE:
                    m_playerStyle = P_STYLE.SPEED;
                    break;
                case P_STYLE.SPEED:
                    m_playerStyle = P_STYLE.MAGIC;
                    break;
                case P_STYLE.MAGIC:
                    m_playerStyle = P_STYLE.BLADE;
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
                m_PlayerState = P_STATE.IDLE;

            }
        }


    }//    public class Player : MonoBehaviour END
}//namespace TeamProject END

