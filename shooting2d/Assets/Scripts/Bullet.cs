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
        gameObject.SetActive(false); //������Ʈ ��� ���ְ�
        IsActiving = false; //��Ȱ��ȭ�Ǿ��ִٰ� ǥ��.
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
            if (IsPlayer) //���� �Ѿ��϶�
            {
                //�ö󰥰��̰� y,���� �����ϴ� ������
                transform.Translate(Vector2.up * Time.deltaTime * speed , Space.World);
                if (transform.position.y >GameManager.Instance.Height) //���� �ƹ��͵� �����缭 ������ �߰��ٺ��� ���̸� �ѱ�
                {
                    Die();
                    yield break;
                }
            }
            else //���� �Ѿ��϶�
            {
                //���������� y���� �����ϴ� ������

            }
            yield return null;//�������ӿ� �ѹ��� �θ����� ��...
        }
    }
}
