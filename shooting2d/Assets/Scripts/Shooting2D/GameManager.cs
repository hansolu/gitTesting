using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region �̱���
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
    //�Ѿ� ������Ʈ Ǯ�� ����ſ���.
    //ť�� ����Ʈ.. ����ϸ� �ǰ���
    //���⿡ ���� �� �迭 ���� ���ٸ� �迭�� �ɰŰ�..
    //�ٵ� �迭�̸�, ������Ʈ Ǯ�� ������ �߰��ϰ������ �����..
    public GameObject BulletPrefab; //�Ѿ� ������
    public GameObject[] EnemyPrefab; //�� ������
    public Sprite[] BulletSprites; //�Ѿ� �׸� ����..

    Coroutine cor = null; //���� ���� ������ ���ӸŴ����� �ڷ�ƾ�� ���� ����.
    #region ����Ʈ ������Ʈ Ǯ��. �������� �ƴ�.
    //List<Bullet> bulletPool = new List<Bullet>(); 
    //�������� ��Ȱ��ȭ ������Ʈ Ǯ  / Ȱ��ȭ���� ������Ʈ�� ���� �� ���ε׾��� ������ �׳� �ϳ��� �� ����.
    //�ΰ��� �����ѵ�, �ϳ��� ������ �����ϰ� ��Ȱ��ȭ �� �ָ� ���ܳ��ڴ�
    //�ٸ��ϳ��� Ȱ��/��Ȱ�� ���� ��Ƶΰ� for�� ���鼭 ��Ȱ��ȭ�� �ָ� ���ڴ�.
    #endregion
    
    Queue<Bullet> bulletQue = new Queue<Bullet>(); //ť�� ���� ������Ʈ Ǯ

    Dictionary<CTEnum.EnemyKind, Queue<Enemy>> enemyKindQue = new Dictionary<CTEnum.EnemyKind, Queue<Enemy>>(); //���� ��� ������Ʈ Ǯ..

    public Player Player { get; private set; }
    
    #region �ӽ� ���� ��Ƶ�.��Ŵ� ����. �׳� �Լ� �ȿ��� ���� ������ ����ص� ��.
    GameObject tempObj; //�Ѿ� ������ �ӽú���
    Bullet tempBullet;//�Ѿ˿� �ӽú���
   
    GameObject tempEnemyObj;//���� ������ �ӽú���
    Enemy tempEnemy;//���� �� �ӽú���
    #endregion
    float playDelayTime = 0; //��ð� ���� ���� ���� �����ϵ��� �� ����..
    float playDelayTime_Min = 0.5f; //�������� �÷��� �ּҽð� 
    float playDelayTime_Max = 1.5f; //�������� �÷��� �ִ�ð�
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

    public bool IsPlaying = false; //���ӸŴ����� ���� ��ƾ�� ������ �ִ��� üũ�ϴ� �Һ���.

    public void SetPlayer(Player _player)
    {
        Player = _player;
    }
    void Start() //������ ����ȭ������ ������ �� ���� �ٽý��� �̷��� �����ϱ� Start�� �ϴ� ��.
    {
        if (halfHeight==0 || halfWidth ==0 || bulletQue.Count <=0 || enemyKindQue.Count <=0) //Init�Ⱥҷȴٸ� ���������� �̷����°���
        {
            Init();
        }
        if (cor !=null)
        {
            StopCoroutine(cor);
            cor = null;
        }
        IsPlaying = true; //���ӽ��� �÷��� ���� ����
        cor = StartCoroutine(GameRoutine()); //���ӷ��� ����.
    }
    void Init() //
    {
        halfHeight = Camera.main.orthographicSize; //���� ���� ������
        halfWidth = halfHeight * Camera.main.aspect; //���� ���� ������.

        //�Ѿ� ������Ʈ Ǯ
        for (int i = 0; i < 30; i++) //30�� = ������ ��.
        {
            //GetBullet(); //������Ʈ Ǯ�� ����Ʈ�� ���� ��
            tempObj = Instantiate(BulletPrefab, this.transform);
            tempBullet = tempObj.GetComponent<Bullet>();
            if (tempBullet == null)
            {
                tempBullet = tempObj.AddComponent<Bullet>();
            }
            bulletQue.Enqueue(tempBullet);                        

            tempBullet.InActive();//�������ڸ��� �ϴ� �ָ� ����.
            tempBullet.gameObject.SetActive(false);
        }

        for (int i = 0; i < (int)CTEnum.EnemyKind.End; i++) //��A, B,C,BOSS �� ���� ��ųʸ� ����� ��������
        {
            enemyKindQue.Add((CTEnum.EnemyKind)i, new Queue<Enemy>()); //
            for (int j = 0; j < 5; j++) //���⼭ 5�� �� ȭ�鿡 ���� ������ ���� �ִ� 5�� ���� ���� �� ���� �ְڴٶ�� ���� �Ͽ� �����.
            {
                tempEnemyObj = Instantiate(EnemyPrefab[i], this.transform);
                tempEnemy = tempEnemyObj.GetComponent<Enemy>();
                if (tempEnemy == null)
                {                                        
                    Debug.LogError("�����տ� Enemy���� ��ũ��Ʈ�� �پ����� ���� Ȯ�� ���");
                }

                enemyKindQue[(CTEnum.EnemyKind)i].Enqueue(tempEnemy);
                tempEnemy.InActive();//�������ڸ��� �ϴ� �ָ� ����.                
                tempEnemy.gameObject.SetActive(false);//������Ʈ�� ��.
            }
        }
    }
    #region ������Ʈ Ǯ�� ����Ʈ�� �Ἥ ���� �������� �Ϻ�
    //������Ʈ Ǯ�� ����Ʈ�� ����, �� ����Ʈ���� ��Ȱ��ȭ�� �ָ� ���� ��찡 �ƴҶ� �������� ���Ǵ� ����̹Ƿ� �Լ��� ����
    //Bullet GetBullet() //�Ѿ� �ϳ� ���� �� �Ѿ� ��ȯ.
    //{
    //    tempObj = Instantiate(BulletPrefab, this.transform);
    //    tempBullet = tempObj.GetComponent<Bullet>();
    //    if (tempBullet == null)
    //    {
    //        tempBullet = tempObj.AddComponent<Bullet>();
    //    }

    //    bulletPool.Add(tempBullet);        
    //    tempBullet.Die();//�������ڸ��� �ϴ� �ָ� ����.
    //    return tempBullet;
    //}
    #endregion
    public void CreateBullet(Vector3 pos, bool isplayer, /*Sprite sprite*/CTEnum.BulletKind _bulletkind, Vector3 bulletDir, float bulletpower) //�Ѿ��� ���⵵ ���� �Ű������� �༭ �ְ� ��������� �� ���� �����ֱ�..
        //�׸��� ���̶� ���� ���� �Ѿ� �׸��� ���� �����ٵ� �̷��� �򰥸��ϱ� �׸��� �ٲ��ָ� ������...
    {
        #region �״� ���� ���� Ǯ���� �� ) ����Ʈ�� ������ �˻��ؼ� ��Ȱ��ȭ �� �ָ� ã�Ƽ� ���� ��..
        ////������Ʈ Ǯ���� �Ѿ� ������ ���� �����ϰ� �ָ� �����ݸ�����.
        //for (int i = 0; i < bulletPool.Count; i++)
        //{
        //    if (bulletPool[i].IsActiving == false) //��Ȱ��ȭ�� �ָ� ã�Ƽ�
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

        tempBullet.SetInfo(pos, isplayer, BulletSprites[(int)_bulletkind], bulletDir, bulletpower);//�Ѿ� ���鶧 �Ѿ� ��ġ������ ���� ���ϴ�pos �� ���� �����ټ� �ְ���
        tempBullet.StartShoot();
    }

    //���� ����� � ������ ���� ���������...
    public void CreateEnemy(/*Vector3 pos ���� ���ӸŴ������� ���� �������ְ� ���� �ٸ� �Ű������� ����.*/CTEnum.EnemyKind _kind)
    {
        //�� ���� ������Ѷ�...
        if (enemyKindQue[_kind].Count > 0)
        {
            tempEnemy = enemyKindQue[_kind].Dequeue();//Enemy �ϳ� ���ͼ� ����.
        }
        else //�� ť�� ���������
        {
            tempEnemyObj = Instantiate(EnemyPrefab[(int)_kind], this.transform);
            tempEnemy = tempEnemyObj.GetComponent<Enemy>();
            if (tempEnemy == null)
            {
                Debug.LogError("�����տ� Enemy���� ��ũ��Ʈ�� �������� ����. Ȯ�� �ʿ�");
            }
        }
        
        //tempEnemy.SetInfo(�Ű������� �Ѱ��� �����͵� ����.)// Ȥ�� Startó�� Init() ���� ���� ���� �ϰ������ �������� �̸� ����.. 
        tempEnemy.StartShoot();
    }

    public void ReturnToPool(Bullet _bullet)
    {
        bulletQue.Enqueue(_bullet);
        _bullet.gameObject.SetActive(false);
    }
    public void ReturnToPool(Enemy _enemy) 
    {
        enemyKindQue[_enemy.EnemyKind].Enqueue(_enemy);//Ǯ�� �ٽ� �ֱ�
        _enemy.gameObject.SetActive(false); //�ְ� �ָ� ���ֱ�. �̰� �Ȳ��� ���� ��� �׳� �����ְ��� = ����� ����..
    }

    //���ӷ���
    IEnumerator GameRoutine() //���� ���ۺ��� ���� ����������.
    {
        while (Player == null)
        {
            //�� �÷��̾ �����ɶ����� ���� ���𰡸� ���� ����.
            yield return null;
        }
        //�����ð�����
        //���� ������ ���ǿ� ����        
        //int val = 0;
        while (IsPlaying)
        {            
            playDelayTime = Random.Range(playDelayTime_Min, playDelayTime_Max);

            //�� ���̿� �۾� �־�ǰ�

            //���� ȣ��
            //CreateEnemy(CTEnum.EnemyKind.Enemy_A);

            //yield return new WaitForSeconds(playDelayTime); //�����ð��Ŀ� ���� �������� �����ض�

            //val = Random.Range(0, ���� ���� ���� ����);

            //switch (val)
            //{
            //    case 0:
            //        0�� ���� ����();                    
            //        break;
            //    case 1:
            //       1�� ���� ����();
            //        break;
            //    default:
            //        break;
            //}

            for (int i = 0; i < 2; i++)
            {
            CreateEnemy(CTEnum.EnemyKind.Enemy_B);
                yield return new WaitForSeconds(0.1f); //�����ð��Ŀ� ���� �������� �����ض�                        
            }
            
            yield return new WaitForSeconds(playDelayTime); //�����ð��Ŀ� ���� �������� �����ض�                        

            //for (int i = 0; i < 3; i++)
            //{
            //CreateEnemy(CTEnum.EnemyKind.Enemy_C);
            //}
            //yield return new WaitForSeconds(playDelayTime); //�����ð��Ŀ� ���� �������� �����ض�

            //���������� �ϴ� ������ �ΰ� ���� ���� �ϰ������ ����ٰ� ���� �������ǰ�...                        
        }
    }

    //���Ϻ��� �Լ� ��������
    //void ����1()
    //{
    //    playDelayTime = 10; //��� �ѹ� �����ϸ� 10�ʵ��� ������ ���� �ϱ�����..
        
    //    //��� A10�� �����
    //    //B�� 2�� ����� �ϴ� ������ ����...
    //}    
}
