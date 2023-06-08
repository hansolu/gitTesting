using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Android : MonoBehaviour
{
    Touch touch;
    void Update()
    {                      
        //Input.GetMouseButton(0) //안드로이드에서의 그냥 터치..
                
        if (Input.touches.Length > 0)
        {
            Debug.Log("Input.GetTouch(0).position " + Input.GetTouch(0).position);
        }
        
        //if (Input.GetTouch(0).phase == TouchPhase.Began) //터치의 시작
        //{
        //}                
    }
}
