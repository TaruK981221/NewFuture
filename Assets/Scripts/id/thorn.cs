using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thorn : MonoBehaviour
{



    //貫通する場合はTrigger系(どちらかにColliderのis triggerをチェック) 衝突しあうものはCollision系(ColliderとRigidbodyが必要)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //タグがEnemyBulletのオブジェクトが当たった時に{}内の処理が行われる
        if (collision.gameObject.tag == "Player")
        //if (collision.gameObject.tag == "EnemyBullet")
        {
            Debug.Log("hit Player");  //コンソールにhit Playerが表示
            //gameObject.GetComponent<EnemyBulletManager>()でEnemyBulletManagerスクリプトを参照し
            //.powerEnemy; でEnemyBulletManagerのpowerEnemyの値をゲット
            
        }

    }

}
