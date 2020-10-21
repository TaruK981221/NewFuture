using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TeamProject
{
    //プレイヤーの状態
    public enum P_STATE
    {
        IDLE = 0,       //待機
        RUN,            //走る
        JUMP,           //ジャンプ
        RISE,           //上昇
        FALL,           //下降
        ATTACK,          //攻撃
        JUMP_ATTACK,          //ジャンプ攻撃
        DAMAGE,          //被ダメージ
        STYLE_CHANGE,          //スタイルチェンジ
        MAX_STATE       //全状態数
    }
    //プレイヤーの方向
    public enum P_DIRECTION
    {
        RIGHT = 0,    //右
        LEFT,         //左
        MAX_DIRECT    //全方向
    }
    //プレイヤーの接地状態
    public enum P_GROUND
    {
        GROUND = 0,    //接地
        JUMP_1ST,      //ジャンプ1回目
        JUMP_2ND,      //ジャンプ2回目
        FALL,          //落下
        MAX_STATE      //全状態数
    }

    public enum P_STYLE
    {
        BLADE = 0,
        SPEED,
        MAGIC,
        MAX_STYLE
    }
    public class PlayerState : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }//public class PlayerState : MonoBehaviour END
}//namespace TeamProject END


