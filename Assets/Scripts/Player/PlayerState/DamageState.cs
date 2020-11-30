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
        //void Update()
        //{

        //}
    }//    public class DamageState : PlayerState END
}//namespace TeamProject END
