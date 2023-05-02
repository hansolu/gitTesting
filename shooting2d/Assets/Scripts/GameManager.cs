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
    public GameObject EnemyPrefab; //�� ������

    Coroutine cor = null; //���� ���� ������ ���ӸŴ����� �ڷ�ƾ�� ���� ����.
    #region ����Ʈ ������Ʈ Ǯ��. �������� �ƴ�.
    //List<Bullet> bulletPool = new List<Bullet>(); 
    //�������� ��Ȱ��ȭ ������Ʈ Ǯ  / Ȱ��ȭ���� ������Ʈ�� ���� �� ���ε׾��� ������ �׳� �ϳ��� �� ����.
    //�ΰ��� �����ѵ�, �ϳ��� ������ �����ϰ� ��Ȱ��ȭ �� �ָ� ���ܳ��ڴ�
    //�ٸ��ϳ��� Ȱ��/��Ȱ�� ���� ��Ƶΰ� for�� ���鼭 ��Ȱ��ȭ�� �ָ� ���ڴ�.
    #endregion
    Queue<Bullet> bulletQue = new Queue<Bullet>(); //ť�� ���� ������Ʈ Ǯ

    Queue<Enemy> enemyQue = new Queue<Enemy>(); //���� ��� ������Ʈ Ǯ..

    #region �ӽ� ���� ��Ƶ�.��Ŵ� ����. �׳� �Լ� �ȿ��� ���� ������ ����ص� ��.
    GameObject tempObj; //�Ѿ� ������ �ӽú���
    Bullet tempBullet;//�Ѿ˿� �ӽú���
    GameObject tempEnemyObj;//���� ������ �ӽú���
    Enemy tempEnemy;//���� �� �ӽú���
    #endregion
    float playDelayTime = 0; //��ð� ���� ���� ���� �����ϵ��� �� ����..
    float playDelayTime_Min = 0; //�������� �÷��� �ּҽð� 
    float playDelayTime_Max = 0; //�������� �÷��� �ִ�ð�
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
        halfHeight = Camera.main.orthographicSize; //���� ���� ������
        halfWidth = halfHeight * Camera.main.aspect; //���� ���� ������.

        //�Ѿ� ������Ʈ Ǯ
        for (int i = 0; i < 30; i++)
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
    public void CreateBullet(Vector3 pos, bool isplayer)
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

        tempBullet.SetInfo(pos, isplayer);//�Ѿ� ���鶧 �Ѿ� ��ġ������ ���� ���ϴ�pos �� ���� �����ټ� �ְ���
        tempBullet.StartShoot();
    }
    public void CreateEnemy(/*Vector3 pos ���� ���ӸŴ������� ���� �������ְ� ���� �ٸ� �Ű������� ����.*/)
    {
        //�� ����
        //������Ѷ�...
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
        //Enemy �ϳ� ���ͼ� ����.
        //tempEnemy.SetInfo ���� ���� �� ���� �����̳� 
        tempEnemy.StartShoot();
    }

    public void ReturnToPool(Bullet _bullet)
    {
        bulletQue.Enqueue(_bullet);
        _bullet.gameObject.SetActive(false);
    }


    //���ӷ���
    IEnumerator GameRoutine() //���� ���ۺ��� ���� ����������.
    {
        //�����ð�����
        //���� ������ ���ǿ� ����        
        while (true)
        {
            playDelayTime = Random.Range(playDelayTime_Min, playDelayTime_Max);
            yield return new WaitForSeconds(playDelayTime);
            //���� ȣ��
            CreateEnemy();
        }
    }
}
