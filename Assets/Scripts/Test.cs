/*
 * FileName:    Test
 * Author:
 * CreateTime:  
 * Description:
 * 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            /*print((int)(pos.x - 0.5f));
            print((int)(pos.y + 0.5f));
            print((int)pos.z);*/
        }
    }
}
