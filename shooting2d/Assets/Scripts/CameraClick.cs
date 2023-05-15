using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
public class CameraClick : MonoBehaviour
{
    public enum SampleEnum //int
    {
        None = 0, // 0b 0000 0000
        AA = 1, // 0b00000001
        BB = 2, // 0b00000010
        CC = 4, // 0b00000100
        DD = 8, // 0b00001000
        EE = 16
    }
    SampleEnum sampleE = SampleEnum.AA | SampleEnum.BB; //이런식으로 체크 가능..

    void CheckEnum()
    {
        sampleE.HasFlag(SampleEnum.AA); //AA가 속하는지 여부 체크 가능해짐
    }

    void Update()
    {        
        if (Input.GetMouseButtonDown(0))
        {
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector2 vec= Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(vec, Vector2.zero, 15f);
            if (hit.collider!=null)
            {
                hit.transform.GetComponent<SpriteRenderer>().color = Color.red;
                Debug.Log(hit.collider.gameObject.name);
            }
        }       
    }   
}
