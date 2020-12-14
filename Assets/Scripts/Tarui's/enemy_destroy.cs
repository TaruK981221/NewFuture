using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_destroy : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "enemy" &&
            collision.transform.GetComponentInParent<enemy_pop>().IsPop)
        {
            enemy_pop pop = collision.transform.GetComponentInParent<enemy_pop>();

            pop.IsPop = false;
            
            Destroy(pop.transform.GetChild(0).gameObject);
        }
    }
}
