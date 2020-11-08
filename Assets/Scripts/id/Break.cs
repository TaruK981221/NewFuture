using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{


    //[SerializeField]を書くことによりpublicでなくてもInspectorから値を編集できます
    [SerializeField]
    private float hp = 2;  //体力
    [SerializeField]
    Sprite sprite1;  //壊れる前
    [SerializeField]
    Sprite sprite2 = null; //ひび割れ
    private void Start()
    {
        this.GetComponent<SpriteRenderer>().sprite = sprite2;
    }

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
            hp -= collision.gameObject.GetComponent<PlayerControlScript>().powerEnemy;
        }

        //体力が0以下になった時{}内の処理が行われる
        if (hp <= 0)
        {
            Destroy(gameObject);  //ゲームオブジェクトが破壊される
        }
    }
   
}