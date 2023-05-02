using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;//적의 총알과 나의 총알의 차이는, 가는 방향/그림이 다르기 때문.. /해봤자 데미지 차이
    public bool IsActiving = false;
    bool IsPlayer = true;
    float speed = 10;
    float damage = 5;
    Coroutine cor = null;

    public void SetInfo(Vector3 pos, bool _isplayer)//정보세팅
    {
        transform.position = pos;
        IsPlayer = _isplayer;
        gameObject.SetActive(true);
        IsActiving = true;
    }
    public void InActive()
    {
        if (cor != null)
        {
            StopCoroutine(cor);
            cor = null;
        }
        IsActiving = false; //비활성화되어있다고 표시.
    }

    public void Die() //총알이 죽었을때. 애를 destroy시키는 게 아니고 비활성화.
    {
        InActive();
        GameManager.Instance.ReturnToPool(this);
        //gameObject.SetActive(false); //오브젝트 모습 꺼주고
    }

    public void StartShoot() //쏘기시작. 이건 밖에서 총알을 만들어주고 데이터 세팅 후에 부르게 됨.
    {
        if (cor != null)
        {
            StopCoroutine(cor);
            cor = null;
        }
        cor = StartCoroutine(Move());
    }
    IEnumerator Move() //총알이 움직임
    {
        while (IsActiving)
        {
            if (IsPlayer) //나의 총알일때
            {
                //올라갈것이고 y,값이 증가하는 움직임
                transform.Translate(Vector2.up * Time.deltaTime * speed, Space.World);
                if (transform.position.y > GameManager.Instance.Height) //내가 아무것도 못맞춰서 앞으로 쭉가다보니 높이를 넘김
                {
                    Die();
                    yield break;
                }
            }
            else //적의 총알일때
            {
                //내려오겠죠 y값이 감소하는 움직임
                transform.Translate(Vector2.down * Time.deltaTime * speed, Space.World);
                if (transform.position.y < -GameManager.Instance.Height) //내가 아무것도 못맞춰서 앞으로 쭉가다보니 높이를 넘김
                {
                    Die();
                    yield break;
                }
            }
            yield return null;//한프레임에 한번씩 부르도록 함...
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsPlayer && collision.gameObject.CompareTag("Enemy")
            || IsPlayer == false && collision.gameObject.CompareTag("Player")
            )
        {
            IAttack iattack = collision.gameObject.GetComponent<IAttack>(); //Player, Enemy 요 인터페이스가 있는 상태.
            if (iattack !=null)
            {
                iattack.Attacked(damage); //총알이 데미지를 준 후에는
                Die(); //비활성화.
            }
        }
        #region 공통 부모클래스 혹은 인터페이스가 없을 경우
        //인터페이스가 없다면 밑에처럼 경우마다 다 해당 스크립트 찾아서 진행해야함.
        //if (IsPlayer && collision.gameObject.CompareTag("Enemy"))
        //{
        //    Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        //    if (enemy !=null) //적의 태그를 달고 있고 적의 스크립트도 달고 있으니 얘는 적이 확실하고, 데미지를 입힐 것이다.
        //    {
        //        enemy.데미지 가함(총알이 가진 데미지); //
        //        Die();
        //    }
        //}
        //else if (IsPlayer == false && collision.gameObject.CompareTag("Player"))
        //{
        //    Player player = collision.gameObject.GetComponent<Player>();
        //    if (player != null) //적의 태그를 달고 있고 적의 스크립트도 달고 있으니 얘는 적이 확실하고, 데미지를 입힐 것이다.
        //    {
        //        player.데미지 가함(총알이 가진 데미지);
        //        Die();
        //    }
        //}               
        #endregion
    }
}
