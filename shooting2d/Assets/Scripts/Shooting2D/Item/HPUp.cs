using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPUp : Item
{
    float hpup = 0;
    public void SetInfo(/*int power*/CTItem_HPUp _hpup)
    {
        hpup = _hpup.HP;
    }

    public override void Use(Player player)
    {
        player.SetAddHP(hpup);
    }
}
