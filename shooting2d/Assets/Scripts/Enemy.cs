using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour,IAttack //�������̽� �޾��ְ�
{
    //���ӸŴ������� �긦 �ݸ��ؼ� �����ذ�����.. 
    
    //������ ����  =>    
    //���

    float HP = 0;
    float HPMax = 0;

    //���� �� �����ϰ�
    void Start()
    {
        //�� �ʱ�ȭ ���ְ�
    }
    
    public void StartShoot()
    {

    }
    //�������� ������ �ڷ�ƾ ���� / ������Ʈ ����
    IEnumerator Move() //�ڷ�ƾ�� ���
    {

    }

    void FixedUpdate() //������Ʈ�� ���
    {        
    }

    //�������̽� ����
    public void Attacked(float damage)//�ǰ�
    {
        HP -= damage;
        Debug.Log("���� HP : " + HP);
    }

}
