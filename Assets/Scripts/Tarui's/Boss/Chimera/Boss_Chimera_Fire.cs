using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Chimera_Fire : MonoBehaviour
{
    Boss_Chimera parent;

    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponentInParent<Boss_Chimera>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!parent.IsAtk && 
            collision.gameObject == parent.gameObject)
        {
            parent.AtkCol(2);
        }
    }
}
