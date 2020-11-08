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
            SetNextState();
            Debug.Log("コンストラクタ:ATTACK");
        }

        override public void SetSelfState() { m_selfState = P_STATE.ATTACK; }

        //// Update is called once per frame
        //void Update()
        //{

        //}
    }//    public class AttackState : PlayerState END
}//namespace TeamProject END
