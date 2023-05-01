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
        halfheight = GameManager.Instance.Height - 0.5f; //�� �׸� ���̰� ���� ���ΰ��� 1�̹Ƿ� ������ 0.5�� ���ذ��� �������� ������ �Ѵ�
        halfwidth = GameManager.Instance.Width - 0.5f;//�� �׸� ������ ���� ���� ���� 0.5�̹Ƿ� ������ 0.25�� ���ذ��� ��������.
        isShootable = true;
    }
 
    void Update()
    {
        //��ǲ �ϴ´�� �¿���Ϸ� �����̵��� �ڵ� ¥�ּ���...
        vec.x = Input.GetAxisRaw("Horizontal");
        vec.y = Input.GetAxisRaw("Vertical");
        if (isShootable == false)//�̹� ���� ���� �ð� üũ�� �ʿ��� ����
        {
            shootIntervalCheck += Time.deltaTime;
            if (shootIntervalCheck >= shootInterval)
            {
                isShootable = true;
                shootIntervalCheck = 0;
            }
        }

        if (Input.GetButtonDown("Fire1"))//����ctrl, ���콺 ���� Ŭ��
        {
            if (isShootable) //�Ѿ� ������ ������ ���¸�
            {                
                isShootable = false;
                //���ӸŴ������� �Ѿ� �����ش޶�� ��û.   
                GameManager.Instance.CreateBullet(bulletPoint.position, true);
            }
        }        
    }

    private void FixedUpdate()    
    {
        //�ΰ��� ����� �������� �ϳ� �� �Ͻø� �ǰ���. ���� �̰� ���� �ٸ�����鵵 �����ϴ�.
        transform.Translate(vec * 0.1f/*Ÿ�ӵ�ŸŸ���� ����ؼ�..���Ƿ� �� ��*/, Space.World);
        //rigid.velocity = vec * speed;
        if (transform.position.x > 0  //���� ��ġ�� x �� 0 ���� ũ�� (�ϴ� �����ʿ� ����)
            && transform.position.x > halfwidth) //�������ε� ������ halfwidth ���ΰ��� ����.
        {
            vec.x = /*GameManager.Instance.Width*/halfwidth; //�Ѿ���� x���� ȭ�� ���� ������������ ������Ű����.
            vec.y = transform.position.y; //���� ���̰��� �����ϵ�
            transform.position = vec;
        }
        else if (transform.position.x < 0 //���� ��ġ�� ���� ���ʿ�����.
            && transform.position.x < -halfwidth) //���� ���� ���� ���ؼ��� �Ѿ������.
        {
            vec.x = -halfwidth; //���� ���ʿ����� x���� ���������� -halfwidth
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
