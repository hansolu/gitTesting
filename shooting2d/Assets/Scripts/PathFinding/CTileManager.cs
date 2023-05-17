using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CTileInfo
{
    public int width;
    public int height;
    public List<string> tiletypes;
}

public class CTileManager : MonoBehaviour {

    #region 싱글톤
    static CTileManager instance = null;
    public static CTileManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new CTileManager();
                IsInit = false;
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Init();
            IsInit = false;
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
    public Transform _rootTile; //최상위 타일 오브젝트.(
    public TextAsset _tileInfoJsonFile; //타일정보 파일 //제이슨 파일..
    public CTileInfo _tileInfo; //타일정보 객체 //제이슨에 기록해놨던.. 가로랑 세로.. 타일정보..
    public GameObject[] _tilePrefab; //타일 프리팹
    public static int formula = 1; //길찾기 방식
    public int[,] _tileTypes; //타일 타입 정보 ㅂ배열(2차원
    public CPathNode[,] _tiles; //경로 정보 2차원 배열 //데이터적인것
    //타일정보배열과 인덱스가 일치해야함.
    public SpriteRenderer[,] tileSpriteRenderers; // 타일 오브젝트 2차원 배열. //시각적으로 해줄것.
    public float _tileSize; //타일 크기와 동일하게.

    public Camera camera;
    public int walkLayer=0;

    public static bool IsInit = false; //초기화되었는지 여부.

    public void Init()
    {
        camera = Camera.main;
        walkLayer = 1 << LayerMask.NameToLayer("Walkable");
        CreateTiles();
        IsInit = true;
    }

    void CreateTiles()
    {
        //제이슨 내용을 뽑아온다. 디코더.
        _tileInfo = JsonUtility.FromJson<CTileInfo>(_tileInfoJsonFile.text); 
        //단점은 딕셔너리 로드 안됨.         

        //타일 정보 배열 객체 생성
        _tileTypes = new int[_tileInfo.width, _tileInfo.height];

        int y = 0; //파일에 든 타일의 타입정보 담을 배열생성
        for (int i = _tileInfo.height -1 ; i >= 0; i--)
        {
            string indexString = _tileInfo.tiletypes[i];
            //구분자 기준으로 타입 문자열 배열로 추출
            string[] strs = indexString.Split(",".ToCharArray(), System.StringSplitOptions.None);

            //타일 타입요소를 타일타입 정보 배열에 ㅊ위치에 맞게 저장
            for (int x = 0; x < _tileInfo.width; x++)
            {
                _tileTypes[x, y] = int.Parse(strs[x]);
            }
            y++;
        }
        //타일 오브젝트 배열생성
        tileSpriteRenderers = new SpriteRenderer[_tileInfo.width, _tileInfo.height];

        _tiles = new CPathNode[_tileInfo.width, _tileInfo.height];
        GameObject tempobj; 
        //배열의 타입정보에 맞는 타일 오브젝트 생성
        for (y = 0; y < _tileInfo.height; y++) 
        {
            for (int x = 0; x < _tileInfo.width; x++)
            {
                //오브젝트 위치 계산
                Vector2 pos = new Vector2(x + _tileSize, y + _tileSize);

                int tileType = _tileTypes[x, y];

                //타일 타입정보에 맞는 타일 오브젝트를 생성
                tempobj = Instantiate(_tilePrefab[tileType], pos, Quaternion.identity);                

                //타일 정보 배열에 타일정보 객체를 생성해서 참조함.
                _tiles[x, y] = new CPathNode();                

                _tiles[x, y].X = x; //타일의 x위치 설정
                _tiles[x, y].Y = y;

                _tiles[x, y].IsWall = (tileType == 0) ? true : false; //true 이동불가

                //타일의 위치를 이름으로 설정함
                tempobj.name = x + "," + y;
                //현재 타일 오브젝트를 루트 타일의 자식으로 설정
                tempobj.transform.SetParent(_rootTile);

                tileSpriteRenderers[x, y] = tempobj.GetComponent<SpriteRenderer>();
            }
        }
    }
    
    public void ClearTileColor()
    {
        //타일타입에 맞는 타일을 생성
        for (int y = 0; y < _tileInfo.height; y++)
        {
            for (int x = 0; x < _tileInfo.width; x++)
            {
                tileSpriteRenderers[x,y].color = new Color(1f, 1f, 1f, 1f);
            }
        }
    }
}
