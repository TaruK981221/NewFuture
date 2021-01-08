using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Chimera_AtkCol : MonoBehaviour
{
    enum ASTS
    {
        Jump = 1,
        Fire,

        end
    }

    [SerializeField]
    ASTS sts = ASTS.Jump;

    Boss_Chimera parent;
    Transform player = null;

    bool isLR = false;

    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponentInParent<Boss_Chimera>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        isLR = parent.IsLR;
        if(isLR)
        {
            switch (sts)
            {
                case ASTS.Jump:
                    transform.localPosition = new Vector3(3, 0);
                    break;
                case ASTS.Fire:
                    transform.localPosition = new Vector3(-3, 0);
                    break;
            }
        }
        else
        {
            switch (sts)
            {
                case ASTS.Jump:
                    transform.localPosition = new Vector3(-3, 0);
                    break;
                case ASTS.Fire:
                    transform.localPosition = new Vector3(3, 0);
                    break;
            }
        }
    }

    private void FixedUpdate()
    {
        isLR = parent.IsLR;
        if (isLR)
        {
            switch (sts)
            {
                case ASTS.Jump:
                    transform.localPosition = new Vector3(3, 0);
                    break;
                case ASTS.Fire:
                    transform.localPosition = new Vector3(-3, 0);
                    break;
            }
        }
        else
        {
            switch (sts)
            {
                case ASTS.Jump:
                    transform.localPosition = new Vector3(-3, 0);
                    break;
                case ASTS.Fire:
                    transform.localPosition = new Vector3(3, 0);
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!parent.IsAtk &&
            collision.gameObject == player.gameObject)
        {
            parent.AtkCol((int)sts);
        }
    }
}
