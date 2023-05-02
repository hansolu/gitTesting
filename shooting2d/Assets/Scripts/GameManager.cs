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
    public GameObject BulletPrefab; //총알 프리팹
    public GameObject EnemyPrefab; //적 프리팹

    Coroutine cor = null; //게임 로직 돌리는 게임매니저의 코루틴의 변수 선언.
    #region 리스트 오브젝트 풀링. 좋은예가 아님.
    //List<Bullet> bulletPool = new List<Bullet>(); 
    //이전에는 비활성화 오브젝트 풀  / 활성화중인 오브젝트들 묶음 을 따로뒀었고 지금은 그냥 하나로 할 예정.
    //두가지 가능한데, 하나는 정말로 순수하게 비활성화 된 애만 남겨놓겠다
    //다른하나는 활성/비활성 같이 모아두고 for문 돌면서 비활성화된 애만 쓰겠다.
    #endregion
    Queue<Bullet> bulletQue = new Queue<Bullet>(); //큐로 만든 오브젝트 풀

    Queue<Enemy> enemyQue = new Queue<Enemy>(); //적을 담는 오브젝트 풀..

    #region 임시 변수 모아둠.요거는 취향. 그냥 함수 안에서 지역 변수로 사용해도 됨.
    GameObject tempObj; //총알 만들기용 임시변수
    Bullet tempBullet;//총알용 임시변수
    GameObject tempEnemyObj;//적기 만들기용 임시변수
    Enemy tempEnemy;//적기 용 임시변수
    #endregion
    float playDelayTime = 0; //요시간 마다 뭔가 적이 등장하도록 할 것임..
    float playDelayTime_Min = 0; //랜덤돌릴 플레이 최소시간 
    float playDelayTime_Max = 0; //랜덤돌릴 플레이 최대시간
    float halfHeight = 0;
    float halfWidth = 0;
    public float Height
    {
        get
        {
            if (halfHeight == 0)
                Init();
            return halfHeight;
        }
    }
    public float Width
    {
        get
        {
            if (halfWidth == 0)
                Init();
            return halfWidth;
        }
    }
    void Init() //
    {
        halfHeight = Camera.main.orthographicSize; //세로 절반 사이즈
        halfWidth = halfHeight * Camera.main.aspect; //가로 절반 사이즈.

        //총알 오브젝트 풀
        for (int i = 0; i < 30; i++)
        {
            //GetBullet(); //오브젝트 풀을 리스트로 썼을 때
            tempObj = Instantiate(BulletPrefab, this.transform);
            tempBullet = tempObj.GetComponent<Bullet>();
            if (tempBullet == null)
            {
                tempBullet = tempObj.AddComponent<Bullet>();
            }
            bulletQue.Enqueue(tempBullet);
            tempBullet.InActive();//생성하자마자 일단 애를 꺼둠.
            tempBullet.gameObject.SetActive(false);
        }
    }
    #region 오브젝트 풀을 리스트로 써서 좋지 않은예의 일부
    //오브젝트 풀을 리스트로 쓰고, 그 리스트에서 비활성화된 애만 남긴 경우가 아닐때 공통으로 사용되는 기능이므로 함수로 뺐음
    //Bullet GetBullet() //총알 하나 만들어서 그 총알 반환.
    //{
    //    tempObj = Instantiate(BulletPrefab, this.transform);
    //    tempBullet = tempObj.GetComponent<Bullet>();
    //    if (tempBullet == null)
    //    {
    //        tempBullet = tempObj.AddComponent<Bullet>();
    //    }

    //    bulletPool.Add(tempBullet);        
    //    tempBullet.Die();//생성하자마자 일단 애를 꺼둠.
    //    return tempBullet;
    //}
    #endregion
    public void CreateBullet(Vector3 pos, bool isplayer)
    {
        #region 그닥 좋지 않은 풀링의 예 ) 리스트와 일일이 검색해서 비활성화 된 애를 찾아서 쓰는 것..
        ////오브젝트 풀에서 총알 꺼내서 정보 세팅하고 애를 시작콜링해줌.
        //for (int i = 0; i < bulletPool.Count; i++)
        //{
        //    if (bulletPool[i].IsActiving == false) //비활성화된 애를 찾아서
        //    {
        //        tempBullet = bulletPool[i];
        //        break;
        //    }
        //}
        #endregion
        if (bulletQue.Count > 0)
        {
            tempBullet = bulletQue.Dequeue();
        }
        else
        {
            tempObj = Instantiate(BulletPrefab, this.transform);
            tempBullet = tempObj.GetComponent<Bullet>();
            if (tempBullet == null)
            {
                tempBullet = tempObj.AddComponent<Bullet>();
            }
        }

        tempBullet.SetInfo(pos, isplayer);//총알 만들때 총알 위치값으로 실제 원하는pos 를 적용 시켜줄수 있겠죠
        tempBullet.StartShoot();
    }
    public void CreateEnemy(/*Vector3 pos 같은 게임매니저에서 직접 설정해주고 싶은 다른 매개변수들 선언.*/)
    {
        //적 만들어서
        //실행시켜라...
        if (enemyQue.Count > 0)
        {
            tempEnemy = enemyQue.Dequeue();
        }
        else
        {
            tempEnemyObj = Instantiate(EnemyPrefab, this.transform);
            tempEnemy = tempEnemyObj.GetComponent<Enemy>();
            if (tempEnemy == null)
            {
                tempEnemy = tempEnemyObj.AddComponent<Enemy>();
            }
        }
        //Enemy 하나 빼와서 세팅.
        //tempEnemy.SetInfo 내지 뭔가 적 정보 세팅이나 
        tempEnemy.StartShoot();
    }

    public void ReturnToPool(Bullet _bullet)
    {
        bulletQue.Enqueue(_bullet);
        _bullet.gameObject.SetActive(false);
    }


    //게임로직
    IEnumerator GameRoutine() //게임 시작부터 게임 끝날때까지.
    {
        //일정시간마다
        //뭔가 나만의 조건에 따라서        
        while (true)
        {
            playDelayTime = Random.Range(playDelayTime_Min, playDelayTime_Max);
            yield return new WaitForSeconds(playDelayTime);
            //적기 호출
            CreateEnemy();
        }
    }
}
