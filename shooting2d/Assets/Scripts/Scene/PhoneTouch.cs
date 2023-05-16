using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PhoneTouch : MonoBehaviour
{
    Touch tempTouchs;
    Vector3 touchedPos= Vector3.zero;
    Vector2 firstPressPos = Vector2.zero;
    Vector2 secondPressPos = Vector2.zero;
    bool touchOn = false;
    float speed = 5;
    void CheckUITouch() 
    {
        if (Input.touchCount > 0)
        {    //터치가 1개 이상이면.
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (EventSystem.current.IsPointerOverGameObject(i) == false) //UI터치 했는지 체크. 
                {
                    tempTouchs = Input.GetTouch(i);
                    if (tempTouchs.phase == TouchPhase.Began)
                    {    //해당 터치가 시작됐다면.
                        touchedPos = Camera.main.ScreenToWorldPoint(tempTouchs.position);//ui 터치가 아닌 화면 클릭했을때 그 위치.

                        touchOn = true;

                        break;   //한 프레임(update)에는 하나만.
                    }
                }
            }
        }
    }

    void TouchMove() //업데이트같이 반복되는 곳에서 부를것
    {
        transform.Translate(Input.GetTouch(0).deltaPosition * Time.deltaTime * speed);
    }

    public void Swipe2()
    {
        if (Input.touches.Length > 0)
        {
            tempTouchs = Input.GetTouch(0);
            if (tempTouchs.phase == TouchPhase.Began)
            {
                firstPressPos = new Vector2(tempTouchs.position.x, tempTouchs.position.y);
            }
            if (tempTouchs.phase == TouchPhase.Ended)
            {
                secondPressPos = new Vector2(tempTouchs.position.x, tempTouchs.position.y);

                touchedPos = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                touchedPos.Normalize(); //길이1짜리인 단위 벡터 뽑아냄

                //swipe upwards
                if (touchedPos.y > 0 && touchedPos.x > -0.5f && touchedPos.x < 0.5f)
                {
                    Debug.Log("up swipe");
                }
                //swipe down
                if (touchedPos.y < 0 && touchedPos.x > -0.5f && touchedPos.x < 0.5f)
                {
                    Debug.Log("down swipe");
                }
                //swipe left
                if (touchedPos.x < 0 && touchedPos.y > -0.5f && touchedPos.y < 0.5f)
                {
                    Debug.Log("left swipe");
                }
                //swipe right
                if (touchedPos.x > 0 && touchedPos.y > -0.5f && touchedPos.y < 0.5f)
                {
                    Debug.Log("right swipe");
                }
            }
        }
    }

    public float perspectiveZoomSpeed = 0.5f;  //줌인,줌아웃할때 속도(perspective모드 용)      
    public float orthoZoomSpeed = 0.5f;      //줌인,줌아웃할때 속도(OrthoGraphic모드 용)  

    
    public void ZoomInAndOut()  
    {
        if (Input.touchCount == 2) //손가락 2개가 눌렸을 때
        {
            Touch touchZero = Input.GetTouch(0); //첫번째 손가락 터치를 저장
            Touch touchOne = Input.GetTouch(1); //두번째 손가락 터치를 저장

            //터치에 대한 이전 위치값을 각각 저장함
            //처음 터치한 위치(touchZero.position)에서 이전 프레임에서의 터치 위치와 이번 프로임에서 터치 위치의 차이를 뺌
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition; //deltaPosition는 이동방향 추적할 때 사용
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // 각 프레임에서 터치 사이의 벡터 거리 구함
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude; //magnitude는 두 점간의 거리 비교(벡터)
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // 거리 차이 구함(거리가 이전보다 크면(마이너스가 나오면)손가락을 벌린 상태_줌인 상태)
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // 만약 카메라가 OrthoGraphic모드 라면
            if (Camera.main.orthographic)
            {
                Camera.main.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;
                Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize, 0.1f);
            }
            else
            {
                Camera.main.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;
                Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 0.1f, 179.9f);
            }
        }
    }
}
