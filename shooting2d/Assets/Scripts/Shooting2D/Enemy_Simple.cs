using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Simple : Enemy //�ܼ��� �����̴� ���� �� ����. �ѾȽ�¾�
{
    //��� Awake / Start������ �θ��� ����� ���̰�
    public AnimationCurve curve; //Ŀ�긦 ������ �˾ƾ� ���߿� �����̳ʰ� �׷��� ��� �״�� �����ְ���...
    float siny = 0;
    float xvalue = 0;
    bool isplus = true;
    public override void StartShoot() //���� ���� �Ű������� �״ٸ�
    { //�Ű������� ���õ� �ؾ��ϱ⶧����

        EnemyKind = CTEnum.EnemyKind.Enemy_A;
        power = 2;
        HP = 5;
        HPMax = 5; //���߿� UI������ �׸��� �� �� �ƽ� �ʼ���.
        speed = 3;
        orgpos.x = Random.Range( - GameManager.Instance.Width +1, GameManager.Instance.Width-1);
        orgpos.y = GameManager.Instance.Height + 1; //ȭ�� �� 1���� ������ �¾..
        transform.position = orgpos;
        //dir = orgpos;
        base.StartShoot();
    }
    void FixedUpdate()
    {
        if (IsActiving == false) 
        {
            return;
        }
        
        if (isplus)
        {
            xvalue += Time.deltaTime;
            if (xvalue >= 1) //���� ���� ũ�� ������� �ϰ������ 1�� �ƴϰ� 2, 3.. ��� ���ڸ� �޸��ָ� �ǰ���...
            {
                isplus = false;
            }
        }
        else
        {
            xvalue -= Time.deltaTime;
            if (xvalue <= -1)
            {
                isplus = true;
            }
        }
        dir.x = xvalue; //x���� �����ߴ� �����ߴ� ���ݺ�..
        dir.y -= Time.deltaTime *speed; //y�� ��� �����ϰ�

        transform.position = orgpos + dir;

        #region �����Լ� ����� �
        //siny += Time.deltaTime;
        //dir.y -= Time.deltaTime;
        //dir.x = Mathf.Sin(siny) * speed;

        //transform.position = orgpos + dir;
        #endregion
        #region ������ ���
        ////�׳� �������� �������� ���        
        //dir.y -= Time.deltaTime;

        //transform.position = dir;        
        //Translate== position+= (��ü������ �������ϱ�) / position =  ���� ���ֱ� �� ���̸� ���� �ƽŴٸ�
        #endregion
        
        if (transform.position.y <= -GameManager.Instance.Height)
        {
            Die();
        }
    }
}
