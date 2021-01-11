using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamProject
{
    /// <summary>
    /// プレイヤーのジャンプ攻撃状態クラス
    /// </summary>
    public class JumpAttackState : PlayerState
    {
        public JumpAttackState()
        {
            SetNextState(this);
            SetPrevState(this);
        }

        override public void SetSelfState() { SelfState = P_STATE.JUMPATTACK; }
        override public bool Update()
        {
            //m_timer += Time.deltaTime;
            //m_timer = 0.0f;
            if (m_stateChangeFlag)
            {
                m_stateChangeFlag = false;
                SetPrevState(this);
                SetNextState(m_idleState);
                return true;
            }
            //SetPrevState(this);
            //SetNextState(this);
            if (DamageCheck())
            {

                return true;
            }


            return false;
        }

    }//    public class JumpAttackState : PlayerState END
}//namespace TeamProject END
