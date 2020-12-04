using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamProject
{
    /// <summary>
    /// プレイヤーの攻撃状態クラス
    /// </summary>
    public class AttackState : PlayerState
    {
        public AttackState()
        {
            SetPrevState(this);
            SetNextState(this);
        }

        override public void SetSelfState() { SelfState = P_STATE.ATTACK; }
        override public bool Update()
        {
            m_timer += Time.deltaTime;
            if (m_timer>0.1f)
            {
                m_timer = 0.0f;
                SetPrevState(this);
                SetNextState(m_idleState);
                return true;
            }
            SetPrevState(this);
            SetNextState(this);

            return true;
        }
        }//    public class AttackState : PlayerState END
    }//namespace TeamProject END
