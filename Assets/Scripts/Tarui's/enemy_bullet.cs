using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_bullet : MonoBehaviour
{
    bool LRFlg = false;
    public bool LRFLG
    {
        set
        {
            LRFlg = value;
        }
    }

    new Rigidbody2D rigidbody;

    new Renderer renderer;

    [SerializeField]
    float speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.transform.GetComponent<Rigidbody2D>();
        renderer = this.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(LRFlg)
        {
            rigidbody.velocity = new Vector2(-speed, 0.0f);
        }
        else
        {
            rigidbody.velocity = new Vector2(speed, 0.0f);
        }

        if(!renderer.isVisible)
        {
            Destroy(this.gameObject);
        }
    }
}
