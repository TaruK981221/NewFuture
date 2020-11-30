using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_gazer_col : MonoBehaviour
{
    enemy_gazer parentCom = null;

    // 攻撃対象
    Transform player = null;

    // Start is called before the first frame update
    void Awake()
    {
        parentCom = this.transform.parent.GetComponentInChildren<enemy_gazer>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == player)
        {
            parentCom.AtkCollision();
        }
    }
}
