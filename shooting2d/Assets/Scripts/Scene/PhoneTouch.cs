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
        {    //��ġ�� 1�� �̻��̸�.
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (EventSystem.current.IsPointerOverGameObject(i) == false) //UI��ġ �ߴ��� üũ. 
                {
                    tempTouchs = Input.GetTouch(i);
                    if (tempTouchs.phase == TouchPhase.Began)
                    {    //�ش� ��ġ�� ���۵ƴٸ�.
                        touchedPos = Camera.main.ScreenToWorldPoint(tempTouchs.position);//ui ��ġ�� �ƴ� ȭ�� Ŭ�������� �� ��ġ.

                        touchOn = true;

                        break;   //�� ������(update)���� �ϳ���.
                    }
                }
            }
        }
    }

    void TouchMove() //������Ʈ���� �ݺ��Ǵ� ������ �θ���
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

                touchedPos.Normalize(); //����1¥���� ���� ���� �̾Ƴ�

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

    public float perspectiveZoomSpeed = 0.5f;  //����,�ܾƿ��Ҷ� �ӵ�(perspective��� ��)      
    public float orthoZoomSpeed = 0.5f;      //����,�ܾƿ��Ҷ� �ӵ�(OrthoGraphic��� ��)  

    
    public void ZoomInAndOut()  
    {
        if (Input.touchCount == 2) //�հ��� 2���� ������ ��
        {
            Touch touchZero = Input.GetTouch(0); //ù��° �հ��� ��ġ�� ����
            Touch touchOne = Input.GetTouch(1); //�ι�° �հ��� ��ġ�� ����

            //��ġ�� ���� ���� ��ġ���� ���� ������
            //ó�� ��ġ�� ��ġ(touchZero.position)���� ���� �����ӿ����� ��ġ ��ġ�� �̹� �����ӿ��� ��ġ ��ġ�� ���̸� ��
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition; //deltaPosition�� �̵����� ������ �� ���
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // �� �����ӿ��� ��ġ ������ ���� �Ÿ� ����
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude; //magnitude�� �� ������ �Ÿ� ��(����)
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // �Ÿ� ���� ����(�Ÿ��� �������� ũ��(���̳ʽ��� ������)�հ����� ���� ����_���� ����)
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // ���� ī�޶� OrthoGraphic��� ���
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
