using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamProject
{
    enum P_STATE
    {
        IDLE = 0,       //待機
        RUN,            //走る
        JUMP,           //ジャンプ
        ATACK,          //攻撃
        MAX_STATE       //全状態数
    }

    public class Player : MonoBehaviour
    {
        P_STATE m_PlayerState;
        public float m_Speed;
        public GameObject m_Player;
        public Vector3 m_Position;

        // Start is called before the first frame update
        void Start()
        {
            m_PlayerState = P_STATE.IDLE;
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
            //上入力
            if (InputManager.InputManager.Instance.GetArrow(InputManager.ArrowCode.RightArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                m_Position.x += Time.deltaTime * m_Speed;
                m_Player.transform.position += m_Position;
            }
            else if (InputManager.InputManager.Instance.GetArrow(InputManager.ArrowCode.LeftArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                m_Position.x -= Time.deltaTime * m_Speed;
                m_Player.transform.position += m_Position;
            }
        }
    }

}
