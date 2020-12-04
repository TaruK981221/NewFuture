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
            collision.transform.GetComponentInParent<enemy_pop>().IsPop = false;

            if (collision.transform.parent.name != "EnemyPop")
            {
                Destroy(collision.transform.parent.gameObject);
            }
            else
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
