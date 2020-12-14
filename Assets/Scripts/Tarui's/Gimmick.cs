using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gimmick : MonoBehaviour
{
    float postime = 0;

    [SerializeField,Header("false:左から　true:右から")]
    bool isLR = false;
    [SerializeField,Header("false:左右　true:上下")]
    bool isXY = false;

    [SerializeField, Header("移動スピード")]
    float speed = 0.01f;

    [SerializeField, Header("false:時間で管理　true:量で管理")]
    bool isChange = true;
    [SerializeField, Header("移動の切り替えタイミング(時間)")]
    float changeTime = 2.0f;
    [SerializeField, Header("移動量(上記を使わない場合)")]
    float changeVec = 1000.0f;

    [SerializeField,Header("上に乗るキャラのタグ")]
    List<string> tags;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isXY)
        {
            if (isLR)
            {
                this.transform.position +=
                    new Vector3(0, speed) * this.transform.lossyScale.y;
            }
            else
            {
                this.transform.position -=
                    new Vector3(0, speed) * this.transform.lossyScale.y;
            }
        }
        else
        {
            if (isLR)
            {
                this.transform.position +=
                    new Vector3(speed, 0) * this.transform.lossyScale.y;
            }
            else
            {
                this.transform.position -=
                    new Vector3(speed, 0) * this.transform.lossyScale.y;
            }
        }

        if (isChange)
        {
            postime += speed * this.transform.lossyScale.y;

            if(postime>changeVec)
            {
                postime = 0;
                isLR = !isLR;
            }
        }
        else
        {
            postime += Time.deltaTime;

            if (postime > changeTime)
            {
                postime = 0;
                isLR = !isLR;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool isOK = false;

        for (int i = 0; i < tags.Count - 1; i++)
        {
            if (collision.tag == tags[i])
            {
                isOK = true;

                break;
            }
        }

        if (isOK)
        {
            collision.GetComponentInParent<Rigidbody2D>().transform.SetParent(this.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        bool isOK = false;

        for (int i = 0; i < tags.Count - 1; i++)
        {
            if (collision.tag == tags[i])
            {
                isOK = true;

                break;
            }
        }

        if (isOK)
        {
            collision.GetComponentInParent<Rigidbody2D>().transform.SetParent(null);
        }
    }
}
