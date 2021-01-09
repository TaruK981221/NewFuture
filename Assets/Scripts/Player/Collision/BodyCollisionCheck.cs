using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamProject
{

    public class BodyCollisionCheck : MonoBehaviour
    {
        private string enemyTag = "enemy";
        private string enemyAttackTag = "EnemyAttack";
        private bool isCollision = false;
        private bool isEnemyEnter, isEnemyStay, isEnemyExit;

        //接触判定を返すメソッド
        //物理判定の更新毎に呼ぶ必要がある
        public bool IsCollision()
        {
            if (isEnemyEnter || isEnemyStay)
            {
                isCollision = true;
            }
            else if (isEnemyExit)
            {
                isCollision = false;
            }

            isEnemyEnter = false;
            isEnemyStay = false;
            isEnemyExit = false;
            return isCollision;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == enemyTag || collision.tag == enemyAttackTag)
            {
                isEnemyEnter = true;
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.tag == enemyTag || collision.tag == enemyAttackTag)
            {
                isEnemyStay = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == enemyTag || collision.tag == enemyAttackTag)
            {
                isEnemyExit = true;
            }
        }
    }
}