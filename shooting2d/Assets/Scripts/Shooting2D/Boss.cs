using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{    
    float ShootRadius = 0; //반지름 길이..
    float ShootDegree = 0;//각도를 지정하거나
    int ShootCount = 12;//360도로 몇개나 쏠건지

    float shootInterval = 0.8f;
    //float shootIntervalCheck = 0;
    Vector2 bulletpos = Vector2.zero;
    
    public override void StartShoot()
    {
        ShootDegree = 360 / ShootCount;
        if (transform.childCount > 0 && ShootRadius <= 0)
        {
            ShootRadius = Mathf.Abs(transform.GetChild(0).transform.localPosition.y);//반지름
        }
        power = 3;
        HP = 50;
        HPMax = 50;
        orgpos.x = 0;
        orgpos.y = GameManager.Instance.Height -3;
        transform.position = orgpos;
        dir = (transform.GetChild(0).transform.position - transform.position).normalized;
        base.StartShoot();        
        cor = StartCoroutine(StartMove());
    }

    protected /*override*/ IEnumerator StartMove()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootInterval);
            for (int i = 0; i < ShootCount; i++)
            {
                dir = Quaternion.AngleAxis(-ShootDegree, Vector3.forward) * dir;
                bulletpos.x = dir.x;
                bulletpos.y = dir.y + ShootRadius;
                GameManager.Instance.CreateBullet(bulletpos, false, CTEnum.BulletKind.Boss, dir.normalized, power);
            }
            yield return new WaitForSeconds(shootInterval);
            for (int i = 0; i < ShootCount; i++)
            {
                dir = Quaternion.AngleAxis(-ShootDegree, Vector3.forward) * dir;
                bulletpos.x = dir.x;
                bulletpos.y = dir.y + ShootRadius;
                GameManager.Instance.CreateBullet(bulletpos, false, CTEnum.BulletKind.Boss, dir.normalized, power);
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(shootInterval);
            bulletpos = transform.GetChild(0).transform.position;
            for (float i = -1; i < 1; )
            {
                i += 0.2f;
                dir.x = Mathf.Sin(i);                
                dir.y = -1;
                GameManager.Instance.CreateBullet(bulletpos, false, CTEnum.BulletKind.Boss, dir.normalized, power);
                yield return new WaitForSeconds(0.2f);
            }
            for (float i = 1; i > -1;)
            {
                i -= 0.2f;
                dir.x = Mathf.Sin(i);                
                dir.y = -1;
                GameManager.Instance.CreateBullet(bulletpos, false, CTEnum.BulletKind.Boss, dir.normalized, power);
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
}
