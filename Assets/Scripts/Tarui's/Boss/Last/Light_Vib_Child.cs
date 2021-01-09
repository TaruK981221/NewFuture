using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Vib_Child : MonoBehaviour
{
    Vector2 startPos = Vector2.zero;
    Vector2 endPos = Vector2.zero;

    float Theta = 0.0f;
    float range = 0.0f;
    [SerializeField]
    float MaxRange = 50.0f;

    float num = 0.0f;

    [SerializeField]
    float TimeMax = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        startPos = this.transform.localPosition;

        Theta = Random.Range(0.0f, 360.0f);
        range = Random.Range(0, MaxRange);

        endPos.x = range * Mathf.Cos(Theta * Mathf.Deg2Rad);
        endPos.y = range * Mathf.Sin(Theta * Mathf.Deg2Rad);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (num < TimeMax)
        {
            num += Time.deltaTime;

            float t = num / TimeMax;
            this.transform.localPosition =
                Vector3.Lerp(startPos, endPos, t);
        }
        else
        {
            num = 0.0f;

            startPos = this.transform.localPosition = endPos;

            Theta = Random.Range(0.0f, 360.0f);
            range = Random.Range(0, MaxRange);

            endPos.x = range * Mathf.Cos(Theta * Mathf.Deg2Rad);
            endPos.y = range * Mathf.Sin(Theta * Mathf.Deg2Rad);
        }
    }
}
