using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public CTEnum.ItemKind _kind = CTEnum.ItemKind.End;

    //모든 아이템은 아이템을 상속받도록
    //그래서 반드시 Use를 구현하도록 한다

    //P 파워니까 먹었을때 공격력 증가.        
    //스피드 증가
    //총알 쏘는 간격 조정.. (공격속도증가)
    //HP를 올림..
    //일정시간 무적 /혹은 공격 방어 (한 3회 방어)        

    //아군 생성.. 


    //폭탄. 화면에 있는 적 제거. //요거 말고는

    //특정아이템이 특별하게 움직이지 않는 이상 모든 아이템은 
    //비슷한 움직임을 갖겠죠
    public virtual void Move() //특정 스크립트에서 재정의가 가능해지기 때문.
    {
        //아이템은 무조건.. 아래로 찬찬히 내려옴...
        //이동시작..

        //혹시 화면밖으로 나갔다면 죽기. 비활성화..
    }

    public /*virtual*/ void SetPosition(Vector2 pos) //매개변수로 벡터만 받으면 됨.
    {
        transform.position = pos; //시작 위치 세팅.
    }

    public abstract void Use(Player player);    
        //내가 처리할거 처리하고
        //꺼지기..        
}
