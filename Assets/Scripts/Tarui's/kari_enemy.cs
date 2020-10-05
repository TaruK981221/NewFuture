using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kari_enemy : MonoBehaviour
{
    Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigidbody.AddForce(new Vector2(1.0f, 0.0f), ForceMode2D.Force);
    }
}
