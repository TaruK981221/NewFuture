using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision_kari : MonoBehaviour
{
    private string playerAttackTag = "PlayerAttack";
    private bool isGround = false;
    private bool isGroundEnter, isGroundStay, isGroundExit;

    //接地判定を返すメソッド
    //物理判定の更新毎に呼ぶ必要がある
    public bool IsGround()
    {
        if (isGroundEnter || isGroundStay)
        {
            isGround = true;
        }
        else if (isGroundExit)
        {
            isGround = false;
        }

        isGroundEnter = false;
        isGroundStay = false;
        isGroundExit = false;
        return isGround;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == playerAttackTag)
        {
            isGroundEnter = true;
            Destroy(this.gameObject);
            Debug.Log("やられたあ");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == playerAttackTag)
        {
            isGroundStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == playerAttackTag)
        {
            isGroundExit = true;
        }
    }
}
