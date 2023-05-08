using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : Item
{
    public int AddPower = 3;

    //1 �����ڷ� AddPower�� �޵��� �ϰų�
    //PowerUp(float power) //
    //2 �����Ŀ� SetInfo�� �޵��� �ϰų�..
    public void SetInfo(/*int power*/CTItem_PowerUp _powerup)
    {
        AddPower = _powerup.damage;
    }

    //��Ŵ� int / float / string /....
    

    //�� �Լ��� �ҷȴٸ� ����� �������� �÷��̾�� �����߰� �� ����� �������Ѿ���.
    public override void Use(Player player) 
    {
        player.SetPower(AddPower);
    }
}
