using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Android : MonoBehaviour
{
    Touch touch;
    void Update()
    {                      
        //Input.GetMouseButton(0) //�ȵ���̵忡���� �׳� ��ġ..
                
        if (Input.touches.Length > 0)
        {
            Debug.Log("Input.GetTouch(0).position " + Input.GetTouch(0).position);
        }
        
        //if (Input.GetTouch(0).phase == TouchPhase.Began) //��ġ�� ����
        //{
        //}                
    }
}
