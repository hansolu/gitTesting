using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTStructure_Item
{
    public string ItemName="";
    public int ItemPrice=0;    
}

public class CTItem_PowerUp : CTStructure_Item
{
    public int damage=3;
    
}
public class CTItem_HPUp : CTStructure_Item
{
    public float HP = 10;
}
public class CTItem_Morph : CTStructure_Item
{ 
 //내가 변신할 대상의 무언가 데이터..   
}

