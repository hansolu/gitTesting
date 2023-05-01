using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region 싱글톤
    static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
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
    //총알 오브젝트 풀을 만들거에요.
    //큐나 리스트.. 사용하면 되겠죠
    //취향에 따라서 뭐 배열 굳이 쓴다면 배열도 될거고..
    //근데 배열이면, 오브젝트 풀이 꽉차서 추가하고싶을때 어려움..
    public GameObject BulletPrefab;
    List<Bullet> bulletPool = new List<Bullet>();
    #region 임시 변수 모아둠
    GameObject tempObj;
    Bullet tempBullet;
    #endregion
    float halfHeight = 0;
    float halfWidth = 0;
    public float Height 
    { 
        get 
        {
            if(halfHeight==0) 
                Init(); 
            return halfHeight; 
        } 
    }
    public float Width 
    {
        get 
        { 
            if(halfWidth==0) 
                Init(); 
            return halfWidth;
        } 
    }
    void Init()
    {
        halfHeight = Camera.main.orthographicSize ; //세로 절반 사이즈
        halfWidth = halfHeight * Camera.main.aspect; //가로 절반 사이즈.

        for (int i = 0; i < 30; i++)
        {
            tempObj = Instantiate(BulletPrefab, this.transform);
            tempBullet = tempObj.GetComponent<Bullet>();
            if (tempBullet == null)
            {
                tempBullet= tempObj.AddComponent<Bullet>();
            }
            
            bulletPool.Add(tempBullet);
            tempBullet.Die();//생성하자마자 일단 애를 꺼둠.
        }
    }

    public void CreateBullet(Vector3 pos, bool isplayer)
    {
        //총알 만들때 총알 위치값으로 실제 원하는pos 를 적용 시켜줄수 있겠죠
    }
    //얘가 총알 오브젝트 풀에서 하나 꺼내서
    //걔한테 데이터 세팅.
    //총알의 첫 위치
    //적군 총알인지 아군 총알인지
    //총알 그림은 뭘로할지
    //데미지는 뭘로 할지 스피드는 어케 줄지 등등...
}
