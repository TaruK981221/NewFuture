using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMode : MonoBehaviour
{
    // 子オブジェクト格納用
    private GameObject[] ChildObjects;

    // Start is called before the first frame update
    void Start()
    {
        int childNum = 0;

        // 左中右順に格納
        foreach (Transform child in this.gameObject.transform)
        {
            ChildObjects[childNum] = child.gameObject;
            childNum++;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
