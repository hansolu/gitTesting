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
    public GameObject BulletPrefab;
    List<Bullet> bulletPool = new List<Bullet>();
    #region �ӽ� ���� ��Ƶ�
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
        halfHeight = Camera.main.orthographicSize ; //���� ���� ������
        halfWidth = halfHeight * Camera.main.aspect; //���� ���� ������.

        for (int i = 0; i < 30; i++)
        {
            tempObj = Instantiate(BulletPrefab, this.transform);
            tempBullet = tempObj.GetComponent<Bullet>();
            if (tempBullet == null)
            {
                tempBullet= tempObj.AddComponent<Bullet>();
            }
            
            bulletPool.Add(tempBullet);
            tempBullet.Die();//�������ڸ��� �ϴ� �ָ� ����.
        }
    }

    public void CreateBullet(Vector3 pos, bool isplayer)
    {
        //�Ѿ� ���鶧 �Ѿ� ��ġ������ ���� ���ϴ�pos �� ���� �����ټ� �ְ���
    }
    //�갡 �Ѿ� ������Ʈ Ǯ���� �ϳ� ������
    //������ ������ ����.
    //�Ѿ��� ù ��ġ
    //���� �Ѿ����� �Ʊ� �Ѿ�����
    //�Ѿ� �׸��� ��������
    //�������� ���� ���� ���ǵ�� ���� ���� ���...
}
