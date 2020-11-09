using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamProject
{
    //
    public class PlayerAnimation : MonoBehaviour
    {
        private string[] pAnimName;
        private Animator anim = null;
        private void Awake()
        {
            pAnimName = new string[(int)P_STATE.MAX_STATE]
            {
                "idle",
                "run",
                "rise",
                "fall",
                "attack",
                "jump_attack",
                "damage",
                "stylechange_next",
                "stylechange_prev"
            };
        }
        // Start is called before the first frame update
        void Start()
        {
            GameObject myGameObject=this.gameObject;

            //子オブジェクトのアニメーターを取得
            anim = myGameObject.transform.Find("PlayerSprite").GetComponent<Animator>();

        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// 引数で渡された状態のアニメーションをtrueにする
        /// </summary>
        /// <param name="_currentPState"></param>
        public void AnimON(P_STATE _currentPState)
        {            
            anim.SetBool(pAnimName[(int)_currentPState], true);
        }

        public void AnimOFF(P_STATE _currentPState)
        {            
            anim.SetBool(pAnimName[(int)_currentPState], false);
        }

    }//    public class FallState : PlayerState END
}//namespace TeamProject END
