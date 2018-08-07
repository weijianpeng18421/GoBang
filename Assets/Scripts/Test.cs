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
    public int[,] array;
    void Start()
    {
        array = new int[15, 15];
        Debug.Log(array[2, 2]);
    }

    void Update()
    {

    }
}
