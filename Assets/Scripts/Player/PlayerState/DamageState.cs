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
            Debug.Log("コンストラクタ:DAMAGE");
        }

        override public void SetSelfState() { m_selfState = P_STATE.DAMAGE; }

        //// Update is called once per frame
        //void Update()
        //{

        //}
    }//    public class DamageState : PlayerState END
}//namespace TeamProject END
