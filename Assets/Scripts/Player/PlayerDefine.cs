using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーの列挙体など宣言用スクリプト
namespace TeamProject
{
    //プレイヤーの状態
    public enum P_STATE
    {
        IDLE = 0,                   //待機
        RUN,                        //走る
        RISE,                       //上昇
        FALL,                       //下降
        ATTACK,                     //攻撃
        JUMP_ATTACK,                //ジャンプ攻撃
        DAMAGE,                     //被ダメージ
        STYLE_CHANGE_NEXT,          //次のスタイルチェンジ
        STYLE_CHANGE_PREV,          //前のスタイルチェンジ
        MAX_STATE                   //全状態数
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
        SCYTHE = 0,     //大鎌スタイル（パワー）
        CLAW,           //鉤爪スタイル（スピード）
        MAGIC,          //魔法スタイル（マジック）
        MAX_STYLE
    }

    public struct P_PARAMETER
    {
        public P_STATE m_PlayerState;
        public P_GROUND m_PlayerGround;
        public P_DIRECTION m_PlayerDirection;
        public P_STYLE m_playerStyle;

    }

    
    //public class PlayerDefine : MonoBehaviour
    //{

    //}//    public class PlayerDefine : MonoBehaviour END
}//namespace TeamProject