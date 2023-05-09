using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    Queue<Vector2> playerPosQue = new Queue<Vector2>(); //Stack에 넣고 관리하면 위치 역행가능해짐
    int followDelay = 0;
    Transform playerTr;
    Transform ShootPoint;
    Vector2 betweendist = Vector2.zero;
    float shootDelayTime = 0;
    bool IsShootable = true;    

    public void SetInfo(Transform playerTr, Vector2 playerpos, Vector2 betweendist, float playerdelaytime)
    {
        if (ShootPoint == null)
        {
            ShootPoint = transform.GetChild(0);
        }
        this.betweendist = betweendist;
        this.playerTr = playerTr;
        this.transform.position = playerpos+betweendist;
        IsShootable = true;
        shootDelayTime = playerdelaytime + 0.2f; //하여간 플레이어보다는 느리게 쏘기
    }    

    private void Update()
    {
        playerPosQue.Enqueue(playerTr.position);
        if (Input.GetButtonDown("Fire1")) //이렇게 직접받거나.. 플레이어가 눌렀을때 팔로워한테 알리거나..
        {
            if (IsShootable)
            {
                IsShootable = false;                
                GameManager.Instance.CreateBullet(ShootPoint.position, true, CTEnum.BulletKind.Player_Follower, Vector3.up, 3);
            }
        }        
    }
    private void FixedUpdate()
    {
        if (playerPosQue.Count >= followDelay)
        {
            this.transform.position = playerPosQue.Dequeue() + betweendist;
        }        
    }
}
