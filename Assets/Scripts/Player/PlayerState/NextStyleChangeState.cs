using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamProject
{

    public class NextStyleChangeState : PlayerState
    {

        public NextStyleChangeState()
        {
            SetNextState(this);
            SetPrevState(this);
        }
        override public void SetSelfState() { SelfState = P_STATE.STYLE_CHANGE_NEXT; }

        // Update is called once per frame
        override public bool Update()
        {
            //PositionUpdate();
            if (m_endAnimation)
            {
                SetPrevState(this);
                SetNextState(m_idleState);
                SetEndAnimFlag(false);

                ChangeNextStyle();               
                Debug.Log("次通った");
                return true;
            }
            SetNextState(this);
            SetPrevState(this);

            Debug.Log("次通ってない");

            return false;
        }
        #region//左入力時の処理
        /// <summary>
        /// 左入力時の処理
        /// </summary>
        public override void LeftKeyDownInput() { }
        public override void LeftKeyHoldInput() { }
        public override void LeftKeyUpInput() { }
        #endregion

    }//    public class NextStyleChangeState : PlayerState END
}//namespace TeamProject END
