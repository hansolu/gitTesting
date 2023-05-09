using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public CTEnum.ItemKind _kind = CTEnum.ItemKind.End;

    //��� �������� �������� ��ӹ޵���
    //�׷��� �ݵ�� Use�� �����ϵ��� �Ѵ�

    //P �Ŀ��ϱ� �Ծ����� ���ݷ� ����.        
    //���ǵ� ����
    //�Ѿ� ��� ���� ����.. (���ݼӵ�����)
    //HP�� �ø�..
    //�����ð� ���� /Ȥ�� ���� ��� (�� 3ȸ ���)        

    //�Ʊ� ����.. 


    //��ź. ȭ�鿡 �ִ� �� ����. //��� �����

    //Ư���������� Ư���ϰ� �������� �ʴ� �̻� ��� �������� 
    //����� �������� ������
    public virtual void Move() //Ư�� ��ũ��Ʈ���� �����ǰ� ���������� ����.
    {
        //�������� ������.. �Ʒ��� ������ ������...
        //�̵�����..

        //Ȥ�� ȭ������� �����ٸ� �ױ�. ��Ȱ��ȭ..
    }

    public /*virtual*/ void SetPosition(Vector2 pos) //�Ű������� ���͸� ������ ��.
    {
        transform.position = pos; //���� ��ġ ����.
    }

    public abstract void Use(Player player);    
        //���� ó���Ұ� ó���ϰ�
        //������..        
}
