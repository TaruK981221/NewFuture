using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_gazer_beam : MonoBehaviour
{
    Vector3 Ppos;
    Vector3 Spos;

    float dis;
    float atan;

    [SerializeField]
    float speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        Spos = this.transform.position;

        GameObject player =
            GameObject.FindGameObjectWithTag("Player");

        Ppos = player.transform.position;

        float x, y;
        x = Ppos.x - Spos.x;
        y = Ppos.y - Spos.y;

        dis = Mathf.Sqrt(x * x + y * y);
        atan = Mathf.Atan(y / x);

        this.transform.eulerAngles = 
            new Vector3(0, 0, atan * 180.0f / Mathf.PI - 90.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 vec;
        vec.x = dis * Mathf.Cos(atan) * 0.01f * speed;
        vec.y = dis * Mathf.Sin(atan) * 0.01f * speed;
        vec.z = 0.0f;

        this.transform.position -= vec;
    }
}
