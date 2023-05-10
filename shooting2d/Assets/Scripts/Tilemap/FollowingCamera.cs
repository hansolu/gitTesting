using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    //ī�޶� 
    [SerializeField]
    float offsetY = 0; //ī�޶� ���� y��
    [SerializeField]
    float offsetZ = 0; //ī�޶� ���� z��
    
    float speed = 5; //
    public Transform playerTr;
    Vector3 vec = Vector3.zero;
    
    //float ī�޶��� x ���� �� �ּ��� = -10;
    //float ī�޶��� x ������ �� �ִ��� = 10;

    void Awake() //���� �������ڸ��� ī�޶��� ���� ���� �����ص�.
    {
        offsetY = this.transform.position.y;
        offsetZ = this.transform.position.z;                        
    }

    void LateUpdate()
    {
        vec.x = playerTr.position.x;
        vec.y = playerTr.position.y + offsetY;
        vec.z = playerTr.position.z + offsetZ; //playerTr.position.z�� ������ 0������ 3d�ϰ�� z���� ������.

        //ī�޶� �������� ���� ��+ ī�޶� ũ�� ������ ���� ���� �ٸ� ���̹Ƿ� ���丸 �����.
        //if (vec.x >= ���� �����ʳ� - ī�޶� ������ ����) //ȭ���� �� ��ġ ǥ�ó� ������ �˾Ƶ־� ī�޶�� ���̻� �̵� ���ϵ��� ���� �� �� ������.
        //{
        //    vec.x= ���� �����ʳ� - ī�޶� ������ ����;
        //}
        //else if (vec.x <= ���� ���ʳ� - ī�޶� ������ ����)
        //{
        //    vec.x = ���� ���ʳ� - ī�޶� ������ ����;
        //}

        transform.position = Vector3.Lerp(transform.position, vec, Time.deltaTime * speed); //�������ִ� ���̱� ������ ���� ����.
        //Lerp = ����. ī�޶��� �ε巯�� ������ ����
    }

}
