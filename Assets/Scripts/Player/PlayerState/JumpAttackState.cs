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

        override public void SetSelfState() { SelfState = P_STATE.JUMP_ATTACK; }

        //// Update is called once per frame
        //void Update()
        //{

        //}
    }//    public class JumpAttackState : PlayerState END
}//namespace TeamProject END
