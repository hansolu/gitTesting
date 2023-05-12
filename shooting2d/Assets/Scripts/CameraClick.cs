using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
public class CameraClick : MonoBehaviour
{
    Vector2 startpos = Vector2.zero;
    Vector2 endpos = Vector2.zero;
    Vector2 pos1 = Vector2.zero;
    Vector2 pos2 = Vector2.zero;
    public LineRenderer line;
    //public enum SampleEnum //int
    //{
    //    None = 0, // 0b 0000 0000
    //    AA = 1, // 0b00000001
    //    BB = 2, // 0b00000010
    //    CC = 4, // 0b00000100
    //    DD = 8, // 0b00001000
    //    EE = 16
    //}
    //SampleEnum sampleE = SampleEnum.AA | SampleEnum.BB; //이런식으로 체크 가능..

    //void CheckEnum()
    //{
    //    sampleE.HasFlag(SampleEnum.AA); //AA가 속하는지 여부 체크 가능해짐
    //}
    float left, right, top, bottom = 0;
    void Start()
    {
        left = Screen.width * 0.1f;
        right = Screen.width * 0.9f;
        top = Screen.height * 0.9f;
        bottom = Screen.height * 0.1f;        
    }
    void Update()
    {
        //if (Input.mousePosition.x <= left)
        //{            
        //}
        //else if (Input.mousePosition.x >= right)
        //{

        //}
        //if (Input.mousePosition.y <= bottom)
        //{
        //}
        //else if (Input.mousePosition.y >= top)
        //{
        //}
        if (Input.GetMouseButtonDown(0))
        {
            //    //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //    Vector2 vec= Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //    RaycastHit2D hit = Physics2D.Raycast(vec, Vector2.zero, 15f);
            //    if (hit.collider!=null)
            //    {
            //        hit.transform.GetComponent<SpriteRenderer>().color = Color.red;
            //        Debug.Log(hit.collider.gameObject.name);
            //    }
            startpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            endpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos1.x = endpos.x;
            pos1.y = startpos.y;

            pos2.x = startpos.x;
            pos2.y = endpos.y;
            line.SetPosition(0, startpos);
            line.SetPosition(1, pos1);
            line.SetPosition(2, endpos);
            line.SetPosition(3, pos2);
            line.SetPosition(4, startpos);
        }
        //else if (Input.GetMouseButtonUp(0))
        //{            
        //}
    }   
}
