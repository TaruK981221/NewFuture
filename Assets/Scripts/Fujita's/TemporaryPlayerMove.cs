using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DefaultExecutionOrder(0)]

public class TemporaryPlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        /*---- 自オブジェクト移動処理(簡易) ----*/
        Vector3 pos = this.transform.position;      // インスタンス生成

        if (Input.GetKey(KeyCode.RightArrow))
        {
            pos.x += 0.1f;
            this.transform.position = pos;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            pos.x -= 0.1f;
            this.transform.position = pos;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            pos.y += 0.1f;
            this.transform.position = pos;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            pos.y -= 0.1f;
            this.transform.position = pos;
        }
    }
}
