using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour,IAttack
{
    public Transform bulletPoint;
    Rigidbody2D rigid;
    Animator anim;
    Vector2 vec = Vector2.zero;
    float speed = 5;    
    float halfwidth = 0;
    float halfheight = 0;
    float shootInterval = 0.5f;
    float shootIntervalCheck = 0;

    float HP = 0; 
    float HPMax = 0;
    float power = 5;
    bool isShootable = true;
    
    void Start()
    {
        HP = 100; //���ϴ� �Ǿ��� ���� �������ְ�.. 
        HPMax = 100;
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); //�� ��ũ��Ʈ�� �޸� ���ӿ�����Ʈ�� ��Ȯ�� �޷��ִٴ� �����Ͽ� ����..       
        halfheight = GameManager.Instance.Height - 0.5f; //�� �׸� ���̰� ���� ���ΰ��� 1�̹Ƿ� ������ 0.5�� ���ذ��� �������� ������ �Ѵ�
        halfwidth = GameManager.Instance.Width - 0.5f;//�� �׸� ������ ���� ���� ���� 0.5�̹Ƿ� ������ 0.25�� ���ذ��� ��������.

        GameManager.Instance.SetPlayer(this);
        isShootable = true;        
    }
 
    void Update()
    {
        //��ǲ �ϴ´�� �¿���Ϸ� �����̵��� �ڵ� ¥�ּ���...
        vec.x = Input.GetAxisRaw("Horizontal");
        vec.y = Input.GetAxisRaw("Vertical");
        anim.SetFloat("MoveX", vec.x);

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
                GameManager.Instance.CreateBullet(bulletPoint.position, true,
                    GameManager.Instance.BulletSprites[(int)CTEnum.BulletKind.PlayerBullet],Vector2.up, power);
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

    public void Attacked(float damage) //���� �ǰ�
    {
        HP -= damage; //�Ѿ��� ���� �ݶ��̴��� �ε�������, �ְ� �� �Լ��� �ҷ��� ���� �������� �� ����.
        Debug.Log("���� ���� ü�� : "+HP);
        anim.SetTrigger("Hit");
    }

    void OnTriggerEnter2D(Collider2D collision)//
    {
        if (collision.gameObject.CompareTag("Enemy")) //
        {
            Debug.Log("���� �浹����");
            Attacked(5);            
        }
    }
}
