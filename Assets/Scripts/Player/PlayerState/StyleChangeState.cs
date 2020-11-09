using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamProject
{

    public class StyleChangeState : PlayerState
    {
        // Start is called before the first frame update
        override public void SetSelfState() { m_selfState = P_STATE.STYLE_CHANGE_NEXT; }

        // Update is called once per frame
        override public bool Update()
        {
            PositionUpdate();

            return false;
        }
    }
}
