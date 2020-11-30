using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamProject
{

    public class PrevStyleChangeState : PlayerState
    {
        public PrevStyleChangeState()
        {
            SetNextState(this);
            SetPrevState(this);
        }
        override public void SetSelfState() { SelfState = P_STATE.STYLE_CHANGE_PREV; }

        // Update is called once per frame
        override public bool Update()
        {
            //PositionUpdate();

            if (m_endAnimation)
            {
                SetPrevState(this);
                SetNextState(m_idleState);
                SetEndAnimFlag(false);

                ChangePrevStyle();
                Debug.Log("前通った");
                return true;
            }
            Debug.Log("前通ってない");

            return false;
        }
    }//    public class PrevStyleChangeState : PlayerState END
}//namespace TeamProject END
