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
    public GameObject[] EnemyPrefab; //적 프리팹
    public Sprite[] BulletSprites; //총알 그림 모음..

    Coroutine cor = null; //게임 로직 돌리는 게임매니저의 코루틴의 변수 선언.
    #region 리스트 오브젝트 풀링. 좋은예가 아님.
    //List<Bullet> bulletPool = new List<Bullet>(); 
    //이전에는 비활성화 오브젝트 풀  / 활성화중인 오브젝트들 묶음 을 따로뒀었고 지금은 그냥 하나로 할 예정.
    //두가지 가능한데, 하나는 정말로 순수하게 비활성화 된 애만 남겨놓겠다
    //다른하나는 활성/비활성 같이 모아두고 for문 돌면서 비활성화된 애만 쓰겠다.
    #endregion
    
    Queue<Bullet> bulletQue = new Queue<Bullet>(); //큐로 만든 오브젝트 풀

    Dictionary<CTEnum.EnemyKind, Queue<Enemy>> enemyKindQue = new Dictionary<CTEnum.EnemyKind, Queue<Enemy>>(); //적을 담는 오브젝트 풀..

    public Player Player { get; private set; }
    
    #region 임시 변수 모아둠.요거는 취향. 그냥 함수 안에서 지역 변수로 사용해도 됨.
    GameObject tempObj; //총알 만들기용 임시변수
    Bullet tempBullet;//총알용 임시변수
   
    GameObject tempEnemyObj;//적기 만들기용 임시변수
    Enemy tempEnemy;//적기 용 임시변수
    #endregion
    float playDelayTime = 0; //요시간 마다 뭔가 적이 등장하도록 할 것임..
    float playDelayTime_Min = 0.5f; //랜덤돌릴 플레이 최소시간 
    float playDelayTime_Max = 1.5f; //랜덤돌릴 플레이 최대시간
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

    public bool IsPlaying = false; //게임매니저가 게임 루틴을 돌리고 있는지 체크하는 불변수.

    public void SetPlayer(Player _player)
    {
        Player = _player;
    }
    void Start() //지금은 메인화면으로 나가기 및 게임 다시시작 이런게 없으니까 Start에 일단 둠.
    {
        if (halfHeight==0 || halfWidth ==0 || bulletQue.Count <=0 || enemyKindQue.Count <=0) //Init안불렸다면 이정보들이 이런상태겠죠
        {
            Init();
        }
        if (cor !=null)
        {
            StopCoroutine(cor);
            cor = null;
        }
        IsPlaying = true; //게임시작 플래그 먼저 설정
        cor = StartCoroutine(GameRoutine()); //게임로직 실행.
    }
    void Init() //
    {
        halfHeight = Camera.main.orthographicSize; //세로 절반 사이즈
        halfWidth = halfHeight * Camera.main.aspect; //가로 절반 사이즈.

        //총알 오브젝트 풀
        for (int i = 0; i < 30; i++) //30개 = 임의의 수.
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

        for (int i = 0; i < (int)CTEnum.EnemyKind.End; i++) //적A, B,C,BOSS 에 대한 딕셔너리 만들기 가능해짐
        {
            enemyKindQue.Add((CTEnum.EnemyKind)i, new Queue<Enemy>()); //
            for (int j = 0; j < 5; j++) //여기서 5는 한 화면에 같은 종류의 적이 최대 5기 까지 존재 할 수도 있겠다라는 가정 하에 만든것.
            {
                tempEnemyObj = Instantiate(EnemyPrefab[i], this.transform);
                tempEnemy = tempEnemyObj.GetComponent<Enemy>();
                if (tempEnemy == null)
                {                                        
                    Debug.LogError("프리팹에 Enemy관련 스크립트가 붙어있지 않음 확인 요망");
                }

                enemyKindQue[(CTEnum.EnemyKind)i].Enqueue(tempEnemy);
                tempEnemy.InActive();//생성하자마자 일단 애를 꺼둠.                
                tempEnemy.gameObject.SetActive(false);//오브젝트도 끔.
            }
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
    public void CreateBullet(Vector3 pos, bool isplayer, /*Sprite sprite*/CTEnum.BulletKind _bulletkind, Vector3 bulletDir, float bulletpower) //총알의 방향도 내가 매개변수로 줘서 애가 어느쪽으로 쭉 갈지 정해주기..
        //그리고 적이랑 나랑 같은 총알 그림을 쓰고 있을텐데 이러면 헷갈리니까 그림도 바꿔주면 좋겠죠...
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

        tempBullet.SetInfo(pos, isplayer, BulletSprites[(int)_bulletkind], bulletDir, bulletpower);//총알 만들때 총알 위치값으로 실제 원하는pos 를 적용 시켜줄수 있겠죠
        tempBullet.StartShoot();
    }

    //적을 만들되 어떤 종류의 적을 만들것인지...
    public void CreateEnemy(/*Vector3 pos 같은 게임매니저에서 직접 설정해주고 싶은 다른 매개변수들 선언.*/CTEnum.EnemyKind _kind)
    {
        //적 만들어서 실행시켜라...
        if (enemyKindQue[_kind].Count > 0)
        {
            tempEnemy = enemyKindQue[_kind].Dequeue();//Enemy 하나 빼와서 세팅.
        }
        else //내 큐가 비어있으면
        {
            tempEnemyObj = Instantiate(EnemyPrefab[(int)_kind], this.transform);
            tempEnemy = tempEnemyObj.GetComponent<Enemy>();
            if (tempEnemy == null)
            {
                Debug.LogError("프리팹에 Enemy관련 스크립트가 존재하지 않음. 확인 필요");
            }
        }
        
        //tempEnemy.SetInfo(매개변수로 넘겨준 데이터들 세팅.)// 혹은 Start처럼 Init() 뭔가 정보 세팅 하고싶은것 실행전에 미리 수행.. 
        tempEnemy.StartShoot();
    }

    public void ReturnToPool(Bullet _bullet)
    {
        bulletQue.Enqueue(_bullet);
        _bullet.gameObject.SetActive(false);
    }
    public void ReturnToPool(Enemy _enemy) 
    {
        enemyKindQue[_enemy.EnemyKind].Enqueue(_enemy);//풀에 다시 넣기
        _enemy.gameObject.SetActive(false); //넣고 애를 꺼주기. 이걸 안끄면 적이 계속 그냥 켜져있겠죠 = 모습이 보임..
    }

    //게임로직
    IEnumerator GameRoutine() //게임 시작부터 게임 끝날때까지.
    {
        while (Player == null)
        {
            //내 플레이어가 생성될때까지 다음 무언가를 진행 안함.
            yield return null;
        }
        //일정시간마다
        //뭔가 나만의 조건에 따라서        
        //int val = 0;
        while (IsPlaying)
        {            
            playDelayTime = Random.Range(playDelayTime_Min, playDelayTime_Max);

            //이 사이에 작업 넣어되고

            //적기 호출
            //CreateEnemy(CTEnum.EnemyKind.Enemy_A);

            //yield return new WaitForSeconds(playDelayTime); //일정시간후에 무언가 다음일을 수행해라

            //val = Random.Range(0, 내가 만든 패턴 개수);

            //switch (val)
            //{
            //    case 0:
            //        0번 패턴 실행();                    
            //        break;
            //    case 1:
            //       1번 패턴 실행();
            //        break;
            //    default:
            //        break;
            //}

            for (int i = 0; i < 2; i++)
            {
            CreateEnemy(CTEnum.EnemyKind.Enemy_B);
                yield return new WaitForSeconds(0.1f); //일정시간후에 무언가 다음일을 수행해라                        
            }
            
            yield return new WaitForSeconds(playDelayTime); //일정시간후에 무언가 다음일을 수행해라                        

            //for (int i = 0; i < 3; i++)
            //{
            //CreateEnemy(CTEnum.EnemyKind.Enemy_C);
            //}
            //yield return new WaitForSeconds(playDelayTime); //일정시간후에 무언가 다음일을 수행해라

            //시작했을때 일단 딜레이 두고 다음 뭔가 하고싶으면 여기다가 로직 세워도되고...                        
        }
    }

    //패턴별로 함수 만들어놓고
    //void 패턴1()
    //{
    //    playDelayTime = 10; //얘는 한번 실행하면 10초동안 이패턴 유지 하기위함..
        
    //    //얘는 A10개 만들고
    //    //B는 2개 만들고 하는 식으로 세팅...
    //}    
}
