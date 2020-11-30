using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの攻撃エフェクトの当たり判定用処理
/// </summary>
public class AttackCollisionCheck : MonoBehaviour
{
    private string enemyTag = "enemy";
    private string breakableTag = "BreakableBlock";
    private bool isCollision = false;
    private bool isCollisionEnter, isCollisionStay, isCollisionExit;


    //衝突判定を返すメソッド
    //物理判定の更新毎に呼ぶ必要がある
    public bool IsCollision()
    {
        if (isCollisionEnter || isCollisionStay)
        {
            isCollision = true;
        }
        else if (isCollisionExit)
        {
            isCollision = false;
        }

        isCollisionEnter = false;
        isCollisionStay = false;
        isCollisionExit = false;
        return isCollision;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == enemyTag)
        {
            isCollisionEnter = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == enemyTag)
        {
            isCollisionStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == enemyTag)
        {
            isCollisionExit = true;
        }
    }

}
