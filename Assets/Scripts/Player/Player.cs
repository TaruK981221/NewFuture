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
        ATACK,          //攻撃
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
    public class Player : MonoBehaviour
    {
        public P_STATE m_PlayerState;
        public P_GROUND m_PlayerGround;
        public P_DIRECTION m_PlayerDirection;
        public float m_Speed;
        public GameObject m_Player;
        public Vector3 m_Position;

        // Start is called before the first frame update
        void Start()
        {
            m_PlayerState = P_STATE.IDLE;
            m_PlayerGround = P_GROUND.GROUND;
            m_PlayerDirection = P_DIRECTION.RIGHT;

            m_Player = this.gameObject;
        }

        // Update is called once per frame
        void Update()
        {
            switch (m_PlayerState)
            {
                case P_STATE.IDLE:
                    break;
                case P_STATE.RUN:
                    break;
                case P_STATE.JUMP:
                    break;
                case P_STATE.ATACK:
                    break;
                case P_STATE.MAX_STATE:
                    break;
                default:
                    break;
            }
            m_Position = Vector3.zero;
            //右入力
            if (InputManager.InputManager.Instance.GetArrow(InputManager.ArrowCode.RightArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                m_Position.x += Time.deltaTime * m_Speed;
                m_Player.transform.position += m_Position;
                m_PlayerState = P_STATE.RUN;
                m_PlayerDirection = P_DIRECTION.RIGHT;

            }
            else if (InputManager.InputManager.Instance.GetArrow(InputManager.ArrowCode.LeftArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
            {            //左入力

                m_Position.x -= Time.deltaTime * m_Speed;
                m_Player.transform.position += m_Position;
                m_PlayerState = P_STATE.RUN;
                m_PlayerDirection = P_DIRECTION.LEFT;

            }
            else
            {
                m_PlayerState = P_STATE.IDLE;

            }
            //入力
            if (InputManager.InputManager.Instance.GetKeyDown(InputManager.ButtonCode.X))
            {
                m_Position.y += Time.deltaTime * m_Speed;
                m_Player.transform.position += m_Position;
                m_PlayerState = P_STATE.JUMP;
                m_PlayerDirection = P_DIRECTION.RIGHT;

            }

        }
    }

}
