using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hoge : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.Rotate(0, 0, 5);
        this.transform.position +=
            Vector3.left/* * this.transform.lossyScale.x*/;
    }
}
