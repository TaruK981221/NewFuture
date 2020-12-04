using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_pop : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;

    //GameObject PopCollision;

    GameObject child;

    bool isPop = false;
    public bool IsPop
    {
        set
        {
            isPop = value;
        }
        get
        {
            return isPop;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //PopCollision = GameObject.Find("PopCollision");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "PopCollision" && !isPop)
        {
            child = Instantiate(prefab);
            child.transform.SetParent(this.transform, false);
            isPop = true;
        }
    }
}
