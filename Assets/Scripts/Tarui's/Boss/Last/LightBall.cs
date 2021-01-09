using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBall : MonoBehaviour
{
    Vector3 Ppos;
    Vector3 Spos;

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

        atan = Mathf.Atan2(y, x);

        this.transform.eulerAngles =
            new Vector3(0, 0, atan * 180.0f / Mathf.PI - 90.0f);


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 vec;
        vec.x = speed * Mathf.Cos(atan) * this.transform.lossyScale.x * 0.05f;
        vec.y = speed * Mathf.Sin(atan) * this.transform.lossyScale.x * 0.05f;
        vec.z = 0.0f;

        this.transform.position += vec;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "PopCollision")
        {
            Destroy(this.gameObject);
        }
    }
}
