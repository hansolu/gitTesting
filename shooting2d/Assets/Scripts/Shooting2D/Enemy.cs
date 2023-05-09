using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour,IAttack //�������̽� �޾��ְ�
{
    public CTEnum.EnemyKind EnemyKind = CTEnum.EnemyKind.End; //�̰ŷ� ���� ���� �ٷ� �Ǻ�
    protected float HP = 0;
    protected float HPMax = 0;
    protected float power = 2;
    protected float speed = 2;
    protected Vector2 orgpos = Vector2.zero; //���� ��ġ ���̾���
    protected Vector2 dir = Vector2.down; //���̴ϱ� �Ʒ��� ���������ϴ°��� default����. dir = direction�� ����

    protected Coroutine cor = null;
    public bool IsActiving { get; protected set; } = false; //���� �۵��ϰ� �ִ��� ǥ���ϴ� bool����.
                                                            //�ۿ����� �θ��⸸ �����ϰ� IsActiving�� �������� ���� �� �ڽĵ�ȿ����� �ϰڴ�.

    public virtual void StartShoot() //�ܺο��� ���ʹ��� �۵��� �����϶�� ��ų �Լ�.
    {
        gameObject.SetActive(true); //������Ʈ Ǯ�� �ִٰ� �ҷ����ͼ� �۵� �������״� Ȱ��ȭ ������
        if (cor !=null)
        {
            StopCoroutine(cor);
            cor = null;
        }
        IsActiving = true;        
    }

    //�������� ������ �ڷ�ƾ ���� / ������Ʈ ����
    //IEnumerator Move() //�ڷ�ƾ�� ���
    //{
    //    yield return null;
    //}
    //void Update()
    //{        
    //}
    //void FixedUpdate() //������Ʈ�� ���
    //{
    //    if (IsActiving == false)
    //    {
    //        return;
    //    }
    //}

    //�������̽� ����
    public void Attacked(float damage)//�ǰ�
    {
        HP -= damage;
        Debug.Log("���� HP : " + HP);
        if (HP <= 0)
        {
            Die();
        }
    }

    public void Die() //�׾����� �۵����� ��Ű��, ���ӸŴ����� Ǯ�� ����������
    {
        CTEnum.ItemKind _kind = (CTEnum.ItemKind)Random.Range(0, (int)CTEnum.ItemKind.End);

        switch (_kind)
        {
            case CTEnum.ItemKind.PowerUp:
                CTItem_PowerUp _powerup = new CTItem_PowerUp();
                _powerup.damage = 3;//�̰͵� �������� �ִ���.. ���� ���ؼ� �ִ���...
                ItemManager.Instance.CreateItem(_kind, transform.position , _powerup as CTStructure_Item);
                break;
            case CTEnum.ItemKind.HPUp:

                break;            
            default:
                break;
        }
        
        //�ָ� ���� ���ϰ� �ϴ� �۾� �ϱ�����
        InActive();
        GameManager.Instance.ReturnToPool(this);
    }

    public void InActive() //�ܼ��� ���α�. 
    {
        if (cor != null) //���� �ִ� �ڷ�ƾ�� �־��ٸ� �ڷ�ƾ ������Ű�� ���������
        {
            StopCoroutine(cor);
            cor = null;
        }
        IsActiving = false; //�۵� ���� boolǥ��
    }
}
