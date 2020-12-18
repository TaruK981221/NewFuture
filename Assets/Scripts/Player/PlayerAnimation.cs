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
        //private float m_timer = 0.0f;
        public RuntimeAnimatorController[] RuntimeAnimatorController;
        public RuntimeAnimatorController RuntimeAnimatorController0;
        public RuntimeAnimatorController RuntimeAnimatorController1;
        public RuntimeAnimatorController RuntimeAnimatorController2;

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
                "next_stylechange",
                "prev_stylechange"
            };
            GameObject myGameObject = this.gameObject;

            //子オブジェクトのアニメーターを取得
            anim = myGameObject.transform.Find("PlayerSprite").GetComponent<Animator>();
        }
        // Start is called before the first frame update
        void Start()
        {
            //アニメーターのセット
            // anim.runtimeAnimatorController = RuntimeAnimatorController[(int)P_STYLE.MAGIC];
        
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

        public void ChangeStyleAnimation(int _Style)
        {
            anim.runtimeAnimatorController = RuntimeAnimatorController[_Style];

        }


    }//    public class FallState : PlayerState END
}//namespace TeamProject END
