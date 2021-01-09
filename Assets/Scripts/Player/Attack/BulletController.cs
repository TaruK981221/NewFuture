using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamProject
{
    public class BulletController : MonoBehaviour
    {
        private Rigidbody2D rb2d;
        private Renderer rend;
        //弾速
       public float bulletSpeed = 500.0f;


        void Start()
        {
            rb2d = this.transform.GetComponent<Rigidbody2D>();

            rend = this.GetComponent<Renderer>();
            //速度の設定（Rigidbody2D）
           rb2d.velocity = new Vector2(bulletSpeed *transform.right.x, 0.0f);
     
        }

        // Update is called once per frame
        void FixedUpdate()
        {

            //カメラ表示外に出たら
            if (!rend.isVisible)
            {
                //削除する
                Destroy(this.gameObject);
            }

        }

  
    }
}