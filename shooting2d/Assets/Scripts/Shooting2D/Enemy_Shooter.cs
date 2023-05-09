using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shooter : Enemy
{    
    public Transform[] ShootPoint; //�ڵ忡�� ������ѵ� �ɰ��̰� ���� �����ջ� ������ѵ���.
    float shootInterval = 1f;//���ʸ��� �� ������
    float shootIntervalCheck = 0; //���ʸ��� �� �������� ���(deltatime���ϱ��)

    public override void StartShoot() //���ӸŴ������� ���� ������ �θ�
    {
        orgpos.x = Random.Range(-GameManager.Instance.Width -3, 
            GameManager.Instance.Width + 3);
        orgpos.y = GameManager.Instance.Height + 2;

        transform.position = orgpos;

        if (EnemyKind  == CTEnum.EnemyKind.Enemy_B) //B�� ������ �� �� �ϰ�..
        {
            //B�� ��𼱰� �¾��.. �÷��̾� ���� �̵�..
            //�ǽð����� �÷��̾� ������ �������� 
            //���� ���Ҽ����� ���� �߰��� ���״� �¾���� �׳� �÷��̾ �ִ� ��ġ�� ��� ������ ��.
            power = 4;
            HP = 7;
            HPMax = 7;
            speed = 5;
            //��� �Ĵٺ��� ���⺤�͸� �����ּ���.
            dir = (GameManager.Instance.Player.transform.position - transform.position).normalized;

            //���� �ٶ󺸴� ���⺤�� ���ϱ� = Ÿ�������� - ���� ������ = ���� Ÿ���� �ٶ󺸴� ����
        }
        else if (EnemyKind == CTEnum.EnemyKind.Enemy_C) //C�� �������� ��...
        {
            //C�� ��𼱰� �¾�� �׳� �ݴ� �밢������ �̵�
            //�� �Ѿ��� ����.. �÷��̾� �߰�.
            power = 5;
            HP = 10;
            HPMax = 10;
            dir = -orgpos.normalized;
            speed = 7;
            //���� �ٶ󺸴� ������ ũ�� �޶����� ���� ���̹Ƿ�
            //�� ��� ȸ���� �ѹ��� �ϵ���..
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; //0~360�� ������ ������ ������...
            transform.rotation = Quaternion.Euler(0,0, angle +90);
        }                

        base.StartShoot(); //
        cor = StartCoroutine(Act());
    }

    IEnumerator Act()
    {
        while (IsActiving)
        {
            yield return null;
            //�̵�...
            transform.Translate(dir * Time.deltaTime * speed, Space.World);
            if (transform.position.y < -GameManager.Instance.Height) //ȭ�� ������ ��������... �������
            {
                Die();
            }

            if (shootIntervalCheck >= shootInterval) //���� �ѽ�� �������� �ð��� �Ǹ�
            {
                //�Ѿ˽��
                if (EnemyKind == CTEnum.EnemyKind.Enemy_B)
                {
                    for (int i = 0; i < ShootPoint.Length; i++)
                    {
                       GameManager.Instance.CreateBullet(ShootPoint[i].transform.position, false,
                           CTEnum.BulletKind.EnemyBullet_B, Vector2.down, power);
                    }                    
                }
                else if(EnemyKind == CTEnum.EnemyKind.Enemy_C)
                {
                    for (int i = 0; i < ShootPoint.Length; i++)
                    {
                        GameManager.Instance.CreateBullet(ShootPoint[i].transform.position, false,
                            CTEnum.BulletKind.EnemyBullet_C,
                            (GameManager.Instance.Player.transform.position - transform.position).normalized, power);
                    }
                }                
                shootIntervalCheck = 0;
            }
            else
                shootIntervalCheck += Time.deltaTime; //�ѽ�Ⱑ������ �ð� ī��Ʈ..
        }
    }
}
