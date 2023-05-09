using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : Item
{
    public int AddPower = 3;

    //1 생성자로 AddPower를 받도록 하거나
    //PowerUp(float power) //
    //2 생성후에 SetInfo로 받도록 하거나..
    public void SetInfo(/*int power*/CTItem_PowerUp _powerup)
    {
        AddPower = _powerup.damage;
    }

    //어떤거는 int / float / string /....
    

    //이 함수가 불렸다면 나라는 아이템이 플레이어와 접촉했고 내 기능을 실현시켜야함.
    public override void Use(Player player) 
    {
        player.SetPower(AddPower);
    }
}
