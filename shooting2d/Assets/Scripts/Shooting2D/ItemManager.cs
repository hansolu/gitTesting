using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    #region �̱���
    static ItemManager instance = null;
    public static ItemManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ItemManager();
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (instance != this)
            {
                Destroy(this);
            }
        }
    }
    #endregion 

    public GameObject[] ItemPrefab;//������ ������ ��������.
    //������Ʈ Ǯ ���������..

    public void CreateItem(CTEnum.ItemKind _kind, Vector2 _pos, CTStructure_Item item_info/*������ ���� ����..*/) 
    {
        return; //������....���� ���߾���..
        GameObject _obj;
        _obj = Instantiate(ItemPrefab[(int)_kind], transform);
       
        switch (_kind)
        {
            case CTEnum.ItemKind.PowerUp:
                PowerUp _powerup = _obj.GetComponent<PowerUp>();
                CTItem_PowerUp powerup = item_info as CTItem_PowerUp;
                _powerup.SetInfo(powerup); //���ݷ¼���
                _powerup.SetPosition(_pos);
                _powerup.Move();
                break;         
            default:
                break;
        }
    }

    //public Item CreateItem(CTEnum.ItemType _type, Vector2 _pos, CTStructure_Item item_info/*������ ���� ����..*/)
    //{
    //    GameObject _obj;
    //    _obj = Instantiate(ItemPrefab[(int)_type], transform);

    //    switch (_type)
    //    {
    //        case CTEnum.ItemType.AttackUP:
    //            break;
    //        case CTEnum.ItemType.HPup:
    //            break;
    //        case CTEnum.ItemType.Morph:

    //            Item item = new Morph();
    //            //�����Ұ� �����ϰ� 
    //            return item;
    //            break;
    //        default:
    //            break;
    //    }
    //}
}
