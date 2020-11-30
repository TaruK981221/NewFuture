using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_gazer_col : MonoBehaviour
{
    enemy_gazer parentCom = null;

    // 攻撃対象
    Transform player = null;

    SpriteRenderer sprite = null;

    // Start is called before the first frame update
    void Awake()
    {
        parentCom = this.transform.parent.GetComponentInChildren<enemy_gazer>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        sprite = this.GetComponent<SpriteRenderer>();

        Color color = Color.yellow;
        sprite.color = color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == player)
        {
            parentCom.AtkCollision();
            Color color = Color.red;
            sprite.color = color;
        }
    }
}
