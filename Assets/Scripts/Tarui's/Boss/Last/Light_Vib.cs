using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Vib : MonoBehaviour
{
    SpriteRenderer[] sr;

    [SerializeField]
    float DeathTime = 5.0f;

    float time = 0;

    [SerializeField, Header("生成する光の球の個数")]
    int BallNum = 5;

    int BallNowNum = 1;

    [SerializeField]
    GameObject Ball;

    bool isStart = true;
    bool isEnd = false;

    // Start is called before the first frame update
    void Start()
    {
        sr = this.GetComponentsInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(time < DeathTime)
        {
            // 出現
            if (isStart)
            {
                if (1 / (float)(BallNum + 1.0f) * DeathTime > time)
                {
                    float a = time / (1 / (float)(BallNum + 1.0f) * DeathTime);

                    Color color = sr[0].color;
                    color.a = a;

                    for (int i = 0; i < sr.Length; i++)
                    {
                        sr[i].color = color;
                    }
                }
                else
                {
                    Color color = sr[0].color;
                    color.a = 1.0f;

                    for (int i = 0; i < sr.Length; i++)
                    {
                        sr[i].color = color;
                    }

                    isStart = false;
                    isEnd = true;
                }
            }

            // 弾発射
            if (((float)BallNowNum / (float)(BallNum + 1)) * DeathTime < time)
            {
                BallNowNum += 1;

                Instantiate(Ball, this.transform.position, Quaternion.identity);
            }

            // 消滅
            if (isEnd)
            {
                if ((float)BallNum / (float)(BallNum + 1.0f) * DeathTime < time)
                {
                    float a = 1 - (time - ((float)BallNum / (float)(BallNum + 1.0f) * DeathTime)) / (1 / (float)(BallNum + 1.0f) * DeathTime);

                    Color color = sr[0].color;
                    color.a = a;

                    for (int i = 0; i < sr.Length; i++)
                    {
                        sr[i].color = color;
                    }
                }
            }

            time += Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
