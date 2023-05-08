using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{//�� �Ѿ� ��ũ��Ʈ�� Ư¡��. �����ư� �־��� dir�� ��� �����ϴ� ������ ����. ���� �� ��ü�� �ε����� ����� ����. ȭ���� �Ѿ�� ��Ȱ��ȭ��.
    public SpriteRenderer spriteRenderer;//���� �Ѿ˰� ���� �Ѿ��� ���̴�, ���� ����/�׸��� �ٸ��� ����.. /�غ��� ������ ����
    public bool IsActiving = false;
    bool IsPlayer = true;
    float speed = 10;
    float damage = 5;
    Vector2 dir = Vector2.zero; //���� ���ư� ���� ��Ƶδ� ���� 
    Coroutine cor = null;

    //Ŀ���� �� �Ѿ�. �� �Ѿ��� �ܺο��� ù ��ġ���� ������ �÷��̾����� ����, �׸�, �Ѿ��� ���ư� ����, �Ѿ��� �� �� ������.
    public void SetInfo(Vector3 pos, bool _isplayer, Sprite sprite, Vector3 bulletDir, float bulletpower)//��������
    {
        spriteRenderer.sprite = sprite; //�� �׸� �ٲ��ֱ�
        dir = bulletDir;
        damage = bulletpower;
        transform.position = pos;
        IsPlayer = _isplayer;
        gameObject.SetActive(true);

        if (IsPlayer == false) //���� ���̸� �Ѿ��� �Ʒ��� �������� ģ���ϱ� �׸��� �������Կ�
        {
            spriteRenderer.flipY = true;
        }
        else
            spriteRenderer.flipY = false;

        IsActiving = true;
        
        if (_isplayer) //�÷��̾ ����������� ����� �ϱ����
        {
            Debug.Log("�÷��̾��� �Ѿ��� �� : " + bulletpower );
        }        
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
            transform.Translate(dir * Time.deltaTime * speed, Space.World);
            //if (IsPlayer) //���� �Ѿ��϶�
            //{
                //�ö󰥰��̰� y,���� �����ϴ� ������
                //transform.Translate(Vector2.up * Time.deltaTime * speed, Space.World);
                if (transform.position.y > GameManager.Instance.Height) //���� �ƹ��͵� �����缭 ������ �߰��ٺ��� ���̸� �ѱ�
                {
                    Die();
                    yield break;
                }
            //}
            //else //���� �Ѿ��϶�
            //{
                //���������� y���� �����ϴ� ������
                //transform.Translate(Vector2.down * Time.deltaTime * speed, Space.World);
                if (transform.position.y < -GameManager.Instance.Height) //���� �ƹ��͵� �����缭 ������ �߰��ٺ��� ���̸� �ѱ�
                {
                    Die();
                    yield break;
                }
            //}
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
