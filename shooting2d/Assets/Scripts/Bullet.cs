using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;//���� �Ѿ˰� ���� �Ѿ��� ���̴�, ���� ����/�׸��� �ٸ��� ����.. /�غ��� ������ ����
    public bool IsActiving = false;
    bool IsPlayer = true;
    float speed = 10;
    float damage = 5;
    Coroutine cor = null;

    public void SetInfo(Vector3 pos, bool _isplayer)//��������
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
        IsActiving = false; //��Ȱ��ȭ�Ǿ��ִٰ� ǥ��.
    }

    public void Die() //�Ѿ��� �׾�����. �ָ� destroy��Ű�� �� �ƴϰ� ��Ȱ��ȭ.
    {
        InActive();
        GameManager.Instance.ReturnToPool(this);
        //gameObject.SetActive(false); //������Ʈ ��� ���ְ�
    }

    public void StartShoot() //������. �̰� �ۿ��� �Ѿ��� ������ְ� ������ ���� �Ŀ� �θ��� ��.
    {
        if (cor != null)
        {
            StopCoroutine(cor);
            cor = null;
        }
        cor = StartCoroutine(Move());
    }
    IEnumerator Move() //�Ѿ��� ������
    {
        while (IsActiving)
        {
            if (IsPlayer) //���� �Ѿ��϶�
            {
                //�ö󰥰��̰� y,���� �����ϴ� ������
                transform.Translate(Vector2.up * Time.deltaTime * speed, Space.World);
                if (transform.position.y > GameManager.Instance.Height) //���� �ƹ��͵� �����缭 ������ �߰��ٺ��� ���̸� �ѱ�
                {
                    Die();
                    yield break;
                }
            }
            else //���� �Ѿ��϶�
            {
                //���������� y���� �����ϴ� ������
                transform.Translate(Vector2.down * Time.deltaTime * speed, Space.World);
                if (transform.position.y < -GameManager.Instance.Height) //���� �ƹ��͵� �����缭 ������ �߰��ٺ��� ���̸� �ѱ�
                {
                    Die();
                    yield break;
                }
            }
            yield return null;//�������ӿ� �ѹ��� �θ����� ��...
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsPlayer && collision.gameObject.CompareTag("Enemy")
            || IsPlayer == false && collision.gameObject.CompareTag("Player")
            )
        {
            IAttack iattack = collision.gameObject.GetComponent<IAttack>(); //Player, Enemy �� �������̽��� �ִ� ����.
            if (iattack !=null)
            {
                iattack.Attacked(damage); //�Ѿ��� �������� �� �Ŀ���
                Die(); //��Ȱ��ȭ.
            }
        }
        #region ���� �θ�Ŭ���� Ȥ�� �������̽��� ���� ���
        //�������̽��� ���ٸ� �ؿ�ó�� ��츶�� �� �ش� ��ũ��Ʈ ã�Ƽ� �����ؾ���.
        //if (IsPlayer && collision.gameObject.CompareTag("Enemy"))
        //{
        //    Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        //    if (enemy !=null) //���� �±׸� �ް� �ְ� ���� ��ũ��Ʈ�� �ް� ������ ��� ���� Ȯ���ϰ�, �������� ���� ���̴�.
        //    {
        //        enemy.������ ����(�Ѿ��� ���� ������); //
        //        Die();
        //    }
        //}
        //else if (IsPlayer == false && collision.gameObject.CompareTag("Player"))
        //{
        //    Player player = collision.gameObject.GetComponent<Player>();
        //    if (player != null) //���� �±׸� �ް� �ְ� ���� ��ũ��Ʈ�� �ް� ������ ��� ���� Ȯ���ϰ�, �������� ���� ���̴�.
        //    {
        //        player.������ ����(�Ѿ��� ���� ������);
        //        Die();
        //    }
        //}               
        #endregion
    }
}
