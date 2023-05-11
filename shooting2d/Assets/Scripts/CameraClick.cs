using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClick : MonoBehaviour
{    
    // Update is called once per frame
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
