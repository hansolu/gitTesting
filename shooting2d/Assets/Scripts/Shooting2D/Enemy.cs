using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour,IAttack //인터페이스 달아주고
{
    public CTEnum.EnemyKind EnemyKind = CTEnum.EnemyKind.End; //이거로 적의 종류 바로 판별
    protected float HP = 0;
    protected float HPMax = 0;
    protected float power = 2;
    protected float speed = 2;
    protected Vector2 orgpos = Vector2.zero; //생성 위치 용이었고
    protected Vector2 dir = Vector2.down; //적이니까 아래로 내려가야하는것이 default겠죠. dir = direction의 축약어

    protected Coroutine cor = null;
    public bool IsActiving { get; protected set; } = false; //내가 작동하고 있는지 표시하는 bool변수.
                                                            //밖에서는 부르기만 가능하고 IsActiving의 값설정은 나와 내 자식들안에서만 하겠다.

    public virtual void StartShoot() //외부에서 에너미의 작동을 시작하라고 시킬 함수.
    {
        gameObject.SetActive(true); //오브젝트 풀에 있다가 불려나와서 작동 시작할테니 활성화 시켜줌
        if (cor !=null)
        {
            StopCoroutine(cor);
            cor = null;
        }
        IsActiving = true;        
    }

    //움직임의 구현은 코루틴 쓰기 / 업데이트 쓰기
    //IEnumerator Move() //코루틴의 경우
    //{
    //    yield return null;
    //}
    //void Update()
    //{        
    //}
    //void FixedUpdate() //업데이트의 경우
    //{
    //    if (IsActiving == false)
    //    {
    //        return;
    //    }
    //}

    //인터페이스 구현
    public void Attacked(float damage)//피격
    {
        HP -= damage;
        Debug.Log("적의 HP : " + HP);
        if (HP <= 0)
        {
            Die();
        }
    }

    public void Die() //죽었으면 작동중지 시키고, 게임매니저의 풀로 돌려보내기
    {
        CTEnum.ItemKind _kind = (CTEnum.ItemKind)Random.Range(0, (int)CTEnum.ItemKind.End);

        switch (_kind)
        {
            case CTEnum.ItemKind.PowerUp:
                CTItem_PowerUp _powerup = new CTItem_PowerUp();
                _powerup.damage = 3;//이것도 랜덤으로 주던지.. 값을 정해서 주던지...
                ItemManager.Instance.CreateItem(_kind, transform.position , _powerup as CTStructure_Item);
                break;
            case CTEnum.ItemKind.HPUp:

                break;            
            default:
                break;
        }
        
        //애를 끄고 뭐하고 하는 작업 하기전에
        InActive();
        GameManager.Instance.ReturnToPool(this);
    }

    public void InActive() //단순히 꺼두기. 
    {
        if (cor != null) //돌고 있던 코루틴이 있었다면 코루틴 정지시키고 비워버리기
        {
            StopCoroutine(cor);
            cor = null;
        }
        IsActiving = false; //작동 중지 bool표시
    }
}
