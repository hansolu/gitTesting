using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CPlayerAstar : MonoBehaviour {

    //public Vector2 _initPosition;
    //public CTileManager CTileManager.Instance;

    public float _speed;

    Vector2 ClickPoint = Vector2.zero;
    //에이스타 경로 찾기 해결사
    PathFindSolver<CPathNode, System.Object> _astar;

    //에이스타 이동경로 노드들
    IEnumerable<CPathNode> _pathNodes;
    Coroutine coroutine = null;

    //이동 상태 여부
    bool _isMoving = false;
    bool _notreach = true;
    private void Start()
    {
        if (CTileManager.IsInit ==false)
        {
            CTileManager.Instance.Init();
        }
        //패스파인드 솔버가 하는거 길의 목록을 봐야함.  
        //c타일매니저의 타일 정보 든 2차원 배열.
        _astar = new PathFindSolver<CPathNode, object>(CTileManager.Instance._tiles);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //터치 위치의 월드 포지션을 구함
            ClickPoint = /*Camera.main*/CTileManager.Instance.camera.ScreenToWorldPoint(Input.mousePosition);

            //이동노드 선택
            SelectMoveNode(ClickPoint);
        }
    }
    public void SelectMoveNode(Vector2 isPosition)
    {
        //터치한곳이 이동가능한 곳인지 체크.
        Collider2D collider = Physics2D.OverlapPoint(isPosition, CTileManager.Instance.walkLayer);

        if (collider == null)
        {
            Debug.Log("콜라이더 없음 (못가는곳)");            
            return;
        }        
        
        MoveStart(collider.transform.position);
    }
    
    public void MoveStart(Vector2 targetPosition)
    {
        Collider2D collider = Physics2D.OverlapPoint(transform.position, CTileManager.Instance.walkLayer);
        if (_isMoving) //
        {
            _notreach = false;
            if (coroutine !=null) //진행중이었으면 멈추기
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }
            transform.position = collider.transform.position;
            CTileManager.Instance.ClearTileColor();
            _isMoving = false;
        }

        _isMoving = true;
                
        //일해라 에이스타
        _pathNodes = _astar.Search(collider.transform.position, targetPosition, null);

        if (_pathNodes == null) 
            Debug.Log("경로 찾기 실패");

        //찾은 길 목록 전체 색상 변경
        foreach (CPathNode pathNode in _pathNodes)
        {
            CTileManager.Instance.tileSpriteRenderers[pathNode.X, pathNode.Y].color
               = new Color(0.8f, 0.4f, 0.4f, 1f);
        }
        coroutine = StartCoroutine("MoveStartCoroutine");
    }
    
    IEnumerator MoveStartCoroutine()
    {        
        //노드 목록 배열로 추출.
        CPathNode[] findPathNode = _pathNodes.ToArray();
        for (int i = 0; i < findPathNode.Length -1; i++) //마지막에서 바로 앞까지만
        {
            _notreach = true;
            //현재위치
            Vector2 currentPos = new Vector2(findPathNode[i].X + CTileManager.Instance._tileSize,
                findPathNode[i].Y + CTileManager.Instance._tileSize);
            
            //다음위치
            Vector2 nextPos = new Vector2(findPathNode[i+1].X + CTileManager.Instance._tileSize,
                findPathNode[i+1].Y + CTileManager.Instance._tileSize);

            Vector2 _dir = (nextPos- currentPos).normalized;
            while (_notreach)
            {                
                transform.Translate(_dir * Time.deltaTime * _speed, Space.World);
                yield return null;
                if (Mathf.Abs(nextPos.x - transform.position.x) <= 0.01f && Mathf.Abs(nextPos.y - transform.position.y) <= 0.01f)
                {
                    transform.position = nextPos;
                    _notreach = false;
                }
            }
            //yield return StartCoroutine(GameObjectMoveCoroutine( currentPos, nextPos, _speed
            //    )); //지연뒤에 리턴됨. 코루틴 지연만큼 대기하고 또 코루틴 만큼 또지연           
        }
        _isMoving = false;

        CTileManager.Instance.ClearTileColor();
    }
    //IEnumerator GameObjectMoveCoroutine( Vector3 startPos, Vector3 endPos,
    //    float time)
    //{        
    //    float i = 0.0f;
    //    float rate = 1.0f / time;
    //    while (true) //1초간격 이동위치 갱신
    //    {
    //        i += Time.deltaTime * rate;
    //        //부드럽게 이동
    //        //시간 기준으로 이동크기 보정하기 위함 = lerp(보간)
    //        transform.position = Vector3.Lerp(startPos, endPos, i);
    //        //지금칸에서 담칸 넘어가는거 원하는 시간 내에 이동하게 자연스럽게 쪼갬            
            
    //        yield return null;
    //        if (i > 1)
    //        {
    //            transform.position = endPos;
    //            break;
    //        }
    //    }
    //}
}
