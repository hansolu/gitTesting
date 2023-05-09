using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shooter : Enemy
{    
    public Transform[] ShootPoint; //코드에서 연결시켜도 될것이고 내가 프리팹상에 연결시켜도됨.
    float shootInterval = 1f;//몇초마다 쏠 것인지
    float shootIntervalCheck = 0; //몇초마다 쏠 것인지를 기록(deltatime더하기용)

    public override void StartShoot() //게임매니저에서 얘의 시작을 부름
    {
        orgpos.x = Random.Range(-GameManager.Instance.Width -3, 
            GameManager.Instance.Width + 3);
        orgpos.y = GameManager.Instance.Height + 2;

        transform.position = orgpos;

        if (EnemyKind  == CTEnum.EnemyKind.Enemy_B) //B의 프리팹 인 것 일것..
        {
            //B는 어디선가 태어나서.. 플레이어 보고 이동..
            //실시간으로 플레이어 쪽으로 가버리면 
            //내가 피할수없는 심한 추격이 될테니 태어났을때 그냥 플레이어가 있던 위치로 계속 가도록 함.
            power = 4;
            HP = 7;
            HPMax = 7;
            speed = 5;
            //사람 쳐다보는 방향벡터를 구해주세요.
            dir = (GameManager.Instance.Player.transform.position - transform.position).normalized;

            //내가 바라보는 방향벡터 구하기 = 타겟포지션 - 나의 포지션 = 내가 타겟을 바라보는 방향
        }
        else if (EnemyKind == CTEnum.EnemyKind.Enemy_C) //C의 프리팹일 것...
        {
            //C는 어디선가 태어나서 그냥 반대 대각선으로 이동
            //단 총알이 이제.. 플레이어 추격.
            power = 5;
            HP = 10;
            HPMax = 10;
            dir = -orgpos.normalized;
            speed = 7;
            //내가 바라보는 방향이 크게 달라지지 않을 것이므로
            //내 모습 회전은 한번만 하도록..
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; //0~360도 사이의 각도를 돌려줌...
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
            //이동...
            transform.Translate(dir * Time.deltaTime * speed, Space.World);
            if (transform.position.y < -GameManager.Instance.Height) //화면 밑으로 내려가면... 사라지기
            {
                Die();
            }

            if (shootIntervalCheck >= shootInterval) //내가 총쏘기 가능해진 시간이 되면
            {
                //총알쏘기
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
                shootIntervalCheck += Time.deltaTime; //총쏘기가능해진 시간 카운트..
        }
    }
}
