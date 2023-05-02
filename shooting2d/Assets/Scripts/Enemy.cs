using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour,IAttack //인터페이스 달아주고
{
    //게임매니저에서 얘를 콜링해서 세팅해가지구.. 
    
    //움직임 시작  =>    
    //사망

    float HP = 0;
    float HPMax = 0;

    //나의 피 선언하고
    void Start()
    {
        //피 초기화 해주고
    }
    
    public void StartShoot()
    {

    }
    //움직임의 구현은 코루틴 쓰기 / 업데이트 쓰기
    IEnumerator Move() //코루틴의 경우
    {

    }

    void FixedUpdate() //업데이트의 경우
    {        
    }

    //인터페이스 구현
    public void Attacked(float damage)//피격
    {
        HP -= damage;
        Debug.Log("적의 HP : " + HP);
    }

}
