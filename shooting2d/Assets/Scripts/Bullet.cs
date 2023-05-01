using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;//
    public bool IsActiving = false;
    bool IsPlayer = true;
    float speed = 10;
    float damage = 5;
    Coroutine cor = null;

    public void SetInfo()
    {
        gameObject.SetActive(true);
        IsActiving = true;
    }
    public void Die()
    {
        if (cor != null)
        {
            StopCoroutine(cor);
            cor = null;
        }
        gameObject.SetActive(false); //오브젝트 모습 꺼주고
        IsActiving = false; //비활성화되어있다고 표시.
    }

    public void StartShoot()
    {
        if (cor!=null)
        {
            StopCoroutine(cor);
            cor = null;
        }
        cor = StartCoroutine(Move());
    }
    IEnumerator Move()
    {
        while (true)
        {
            if (IsPlayer) //나의 총알일때
            {
                //올라갈것이고 y,값이 증가하는 움직임
                transform.Translate(Vector2.up * Time.deltaTime * speed , Space.World);
                if (transform.position.y >GameManager.Instance.Height) //내가 아무것도 못맞춰서 앞으로 쭉가다보니 높이를 넘김
                {
                    Die();
                    yield break;
                }
            }
            else //적의 총알일때
            {
                //내려오겠죠 y값이 감소하는 움직임

            }
            yield return null;//한프레임에 한번씩 부르도록 함...
        }
    }
}
