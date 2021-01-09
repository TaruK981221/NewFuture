using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamProject
{
    /// <summary>
    /// プレイヤーのダメージ状態クラス
    /// </summary>
    public class DamageState : PlayerState
    {
        public DamageState()
        {
            SetNextState(this);
            SetPrevState(this);
        }

        override public void SetSelfState() { SelfState = P_STATE.DAMAGE; }

        //// Update is called once per frame
        override public bool Update()
        {
            //急ぎなので直値を利用
            switch (m_Param.m_PlayerDirection)
            {
                case P_DIRECTION.RIGHT:
                    m_Position.x -= 256.0f * Time.deltaTime;
                    break;
                case P_DIRECTION.LEFT:
                    m_Position.x += 256.0f * Time.deltaTime;
                    break;
                case P_DIRECTION.MAX_DIRECT:
                    break;
                default:
                    break;
            }
            if (m_timer > 0.3f)
            {
                SetNextState(m_idleState);
                m_timer = 0.0f;
                return true;
            }
            m_timer += Time.deltaTime;
            return false;
        }
    }//    public class DamageState : PlayerState END
}//namespace TeamProject END
