using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    //카메라 
    [SerializeField]
    float offsetY = 0; //카메라 본래 y값
    [SerializeField]
    float offsetZ = 0; //카메라 본래 z값
    
    float speed = 5; //
    public Transform playerTr;
    Vector3 vec = Vector3.zero;
    
    //float 카메라의 x 왼쪽 끝 최소점 = -10;
    //float 카메라의 x 오른쪽 끝 최대점 = 10;

    void Awake() //정말 시작하자마자 카메라의 본래 값을 저장해둠.
    {
        offsetY = this.transform.position.y;
        offsetZ = this.transform.position.z;                        
    }

    void LateUpdate()
    {
        vec.x = playerTr.position.x;
        vec.y = playerTr.position.y + offsetY;
        vec.z = playerTr.position.z + offsetZ; //playerTr.position.z는 어차피 0이지만 3d일경우 z값도 있을것.

        //카메라 끝점들은 개인 맵+ 카메라 크기 설정에 따라 값이 다를 것이므로 개념만 적어둠.
        //if (vec.x >= 맵의 오른쪽끝 - 카메라 가로의 절반) //화면의 끝 위치 표시나 정보를 알아둬야 카메라는 더이상 이동 못하도록 제한 둘 수 있을것.
        //{
        //    vec.x= 맵의 오른쪽끝 - 카메라 가로의 절반;
        //}
        //else if (vec.x <= 맵의 왼쪽끝 - 카메라 가로의 절반)
        //{
        //    vec.x = 맵의 왼쪽끝 - 카메라 가로의 절반;
        //}

        transform.position = Vector3.Lerp(transform.position, vec, Time.deltaTime * speed); //보간해주는 것이기 때문에 개인 취향.
        //Lerp = 보간. 카메라의 부드러운 움직임 위함
    }

}
