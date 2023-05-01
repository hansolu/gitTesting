using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform bulletPoint;
    Rigidbody2D rigid;
    Vector2 vec = Vector2.zero;
    float speed = 5;    
    float halfwidth = 0;
    float halfheight = 0;
    float shootInterval = 0.5f;
    float shootIntervalCheck = 0;
    bool isShootable = true;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        halfheight = GameManager.Instance.Height - 0.5f; //내 그림 길이가 대충 세로값이 1이므로 절반인 0.5를 빼준것을 기준으로 갖도록 한다
        halfwidth = GameManager.Instance.Width - 0.5f;//내 그림 길이의 가로 값이 대충 0.5이므로 절반인 0.25를 빼준것을 기준으로.
        isShootable = true;
    }
 
    void Update()
    {
        //인풋 하는대로 좌우상하로 움직이도록 코드 짜주세요...
        vec.x = Input.GetAxisRaw("Horizontal");
        vec.y = Input.GetAxisRaw("Vertical");
        if (isShootable == false)//이미 총을 쏴서 시간 체크가 필요한 상태
        {
            shootIntervalCheck += Time.deltaTime;
            if (shootIntervalCheck >= shootInterval)
            {
                isShootable = true;
                shootIntervalCheck = 0;
            }
        }

        if (Input.GetButtonDown("Fire1"))//왼쪽ctrl, 마우스 왼쪽 클릭
        {
            if (isShootable) //총알 생성이 가능한 상태면
            {                
                isShootable = false;
                //게임매니저에게 총알 생성해달라고 요청.   
                GameManager.Instance.CreateBullet(bulletPoint.position, true);
            }
        }        
    }

    private void FixedUpdate()    
    {
        //두가지 방법중 취향으로 하나 뭐 하시면 되겠죠. 물론 이거 말고도 다른방법들도 많습니다.
        transform.Translate(vec * 0.1f/*타임델타타임을 대신해서..임의로 준 값*/, Space.World);
        //rigid.velocity = vec * speed;
        if (transform.position.x > 0  //나의 위치중 x 가 0 보다 크다 (일단 오른쪽에 있음)
            && transform.position.x > halfwidth) //오른쪽인데 심지어 halfwidth 가로값을 넘음.
        {
            vec.x = /*GameManager.Instance.Width*/halfwidth; //넘어버린 x값을 화면 안쪽 마지막선으로 고정시키겠음.
            vec.y = transform.position.y; //나의 높이값은 유지하되
            transform.position = vec;
        }
        else if (transform.position.x < 0 //나의 위치가 뭔가 왼쪽에있음.
            && transform.position.x < -halfwidth) //나의 가장 왼쪽 기준선을 넘어버렸음.
        {
            vec.x = -halfwidth; //나의 왼쪽에서의 x값의 마지막선은 -halfwidth
            vec.y = transform.position.y;
            transform.position = vec;
        }
        ///////////////////////
        if (transform.position.y > 0 && transform.position.y > halfheight)
        {
            vec.x = transform.position.x;
            vec.y = halfheight;
            transform.position = vec;
        }
        else if (transform.position.y < 0 && transform.position.y < -halfheight)
        {
            vec.x = transform.position.x;
            vec.y = -halfheight;
            transform.position = vec;
        }
    }
    
}
