using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Node
{
    public Node(bool _isWall, int _x, int _y) 
    { 
        isWall = _isWall; 
        x = _x; 
        y = _y; 
    }

    public bool isWall;
    public Node ParentNode;

    // G : 시작으로부터 이동했던 거리, H : |가로|+|세로| 장애물 무시하여 목표까지의 거리, F : G + H
    public int x, y, G, H = 0;
    public int F { get { return G + H; } }
}


public class PathFind2 : MonoBehaviour
{
    public LineRenderer line;
    [Tooltip("출발지점 지정")]
    [SerializeField]
    Transform startposTr;
    [Tooltip("도착지점 지정")]
    [SerializeField]
    Transform targetPosTr;
    [Tooltip("맵의 영점 지정")]
    [SerializeField]
    Transform mapBottomLeftTr;
    [Tooltip("맵의 끝지점 지정")]
    [SerializeField]
    Transform mapTopRightTr;

    bool allowDiagonal; //대각선 허용
    bool dontCrossCorner; //코너에 방해물이 있을때 대각선으로 그냥 가기 방지..//대각선을 지날때 내가 부피가 있는 상태면 체크 필요함.

    Toggle Toggle_Diagonal;
    Toggle Toggle_Corner;

    Vector2Int bottomLeft, topRight; 
    Vector2Int startPos, targetPos;    

    int sizeX, sizeY; //필드의 너비와 높이..
    [SerializeField]
    Node[,] NodeArray; //노드 정보 필드만큼의 노드정보를 담음
    Node StartNode, TargetNode, CurNode; //계속 쓰는 변수라서 선언해둠.

    List<Node> OpenList = new List<Node>(); //검사를 위한 노드 리스트
    List<Node> ClosedList =new List<Node>(); //최소노드들만 모아둔 리스트
    //public List<Node> FinalNodeList = new List<Node>();
    [SerializeField]
    Stack<Node> FinalNodeStack = new Stack<Node>();//

    int wallLayer = 0;
    #region 임시변수
    Collider2D tempCol=null;
    Vector2 checkVec = Vector2.zero;
    #endregion

    private void Start()
    {        
        line = GetComponent<LineRenderer>();
        wallLayer = 1<<LayerMask.NameToLayer("Wall");//벽레이어 int로 들고 있기. 빠른계산
        startposTr = GameObject.Find("StartPos").transform;
        targetPosTr = GameObject.Find("TargetPos").transform;
        mapBottomLeftTr = GameObject.Find("mapBottomLeftTr").transform;
        mapTopRightTr = GameObject.Find("mapTopRightTr").transform;

        allowDiagonal = false;
        dontCrossCorner = false;

        Toggle_Diagonal = GameObject.Find("Toggle_Diagonal").GetComponent<Toggle>();
        Toggle_Corner = GameObject.Find("Toggle_Corner").GetComponent<Toggle>();
        Toggle_Diagonal.isOn = allowDiagonal;
        Toggle_Corner.isOn = dontCrossCorner;

        SetStartAndEndPosition();
    }

    /// <summary>
    /// 시작점, 도착점, 재정의
    /// </summary>
    public void SetStartAndEndPosition()
    {        
        if (startposTr != null)
        {
            startPos = new Vector2Int(Mathf.RoundToInt(startposTr.transform.position.x), Mathf.RoundToInt(startposTr.transform.position.y));            
        }
        else
        {
            startPos = new Vector2Int(0, 0);
        }
        
        if (targetPosTr != null)
        {
            targetPos = new Vector2Int(Mathf.RoundToInt(targetPosTr.transform.position.x), Mathf.RoundToInt(targetPosTr.transform.position.y));
        }
        else
        {
            targetPos = new Vector2Int(6, 6);
        }

        if (mapBottomLeftTr != null)
        {
            bottomLeft = new Vector2Int(Mathf.RoundToInt(mapBottomLeftTr.transform.position.x), Mathf.RoundToInt(mapBottomLeftTr.transform.position.y));
        }
        else
        {
            bottomLeft = new Vector2Int(0, 0);
        }

        if (mapTopRightTr != null)
        {
            topRight = new Vector2Int(Mathf.RoundToInt(mapTopRightTr.transform.position.x), Mathf.RoundToInt(mapTopRightTr.transform.position.y));
        }
        else
        {
            topRight = new Vector2Int(10, 7);
        }
    }

    public void SetCheckToggle(bool isDiagonal)
    {
        if (isDiagonal)
        {
            allowDiagonal = Toggle_Diagonal.isOn;
        }
        else
        {
            dontCrossCorner = Toggle_Corner.isOn;
        }
    }

    public void PathFinding()
    {
        //좌표에 상관없이, 이동기본단위를 블럭화 한다.
        //bottomLeft의 좌표와 상관없이 bottomLeft를 0,0배열에 넣기위함

        // NodeArray의 크기 정해주고, isWall, x, y 대입  
        //0에서 부터 시작하기 때문에 1더해줌.
        sizeX = topRight.x - bottomLeft.x + 1; 
        sizeY = topRight.y - bottomLeft.y + 1;
        NodeArray = new Node[sizeX, sizeY]; //TopRight - bottomLeft 크기의 노드 다믄 2차원배열 생성.

        //노드 배열 채워주기
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                bool isWall = false;                
                checkVec.x = i + bottomLeft.x;
                checkVec.y = j + bottomLeft.y;
                tempCol = Physics2D.OverlapPoint(checkVec, wallLayer); //해당 지점이 벽인지 아닌지 판별
                
                if (tempCol!=null) //충돌체가 검출되었다면 이것은 벽상태. 나중에 이것도 더 효율적으로 바꾸려면 단순히 데이터에서 체크하면 될것.
                    isWall = true; //지금 우리는 맵데이터가 없고 유니티 타일맵으로 맵을 설정해놨기 때문...

                NodeArray[i, j] = new Node(isWall, i + bottomLeft.x, j + bottomLeft.y);//선언만 해뒀던 배열에 노드 정보들을 채워줌.
            }
        }
        
        // 시작과 끝 노드, 열린리스트와 닫힌리스트, 마지막리스트 초기화
        StartNode = NodeArray[startPos.x - bottomLeft.x, startPos.y - bottomLeft.y]; //일반적으로 bottomLeft보다 startpos가 같거나 큰 상황.
        TargetNode = NodeArray[targetPos.x - bottomLeft.x, targetPos.y - bottomLeft.y]; //타겟 포스 또한 마찬가지.
        //bottomLeft의 좌표와 상관없이 해당 좌표들을 배열의 요소로 구하기 위해 bottomLeft를 뺌.

        OpenList.Clear();//재활용을 위해 이전 정보를 비워냄
        OpenList.Add(StartNode);  //맨처음 시작을 위해 오픈리스트에 시작노드를 넣어둠.
        ClosedList.Clear();//재활용을 위해 이전 정보를 비워냄
        //FinalNodeList.Clear();
        FinalNodeStack.Clear();//재활용을 위해 이전 정보를 비워냄


        while (OpenList.Count > 0)//OpenList가 없다면 비었다면 종료. 혹은 내부적으로 타겟지점에 잘도달하였다면 종료..
        {            
            CurNode = OpenList[0];//CurNode는 일단 오픈리스트의 맨 처음.
            for (int i = 1; i < OpenList.Count; i++) //처음 시작하면 OpenList에 하나뿐이라 돌지않음.//CurNode가 오픈리스트의 처음이기때문에 같은것끼리 비교할 이유가 없으니 i는 1부터 검사한다.
                if (OpenList[i].F <= CurNode.F && OpenList[i].H < CurNode.H) // 열린리스트 중 가장 F가 작고 F가 같다면 H가 작은 걸 현재노드로 하고 열린리스트에서 닫힌리스트로 옮기기
                    CurNode = OpenList[i]; //가장 합리적인 노드 = CurNode

            OpenList.Remove(CurNode);//오픈리스트에서 제거한다
            ClosedList.Add(CurNode); //가능성이 있는 노드들의 모음인 ClosedList


            // 마지막
            if (CurNode == TargetNode) //검사하던 노드가 목표이던 타겟 노드라면
            {
                Node TargetCurNode = TargetNode; //최종노드스택에 담을 TargetCurNode 선언 및 마지막 타겟노드 넣어줌
                while (TargetCurNode != StartNode)//타겟 노드서부터 부모를 쫓아 스타트 노드로 역행
                {
                    //FinalNodeList.Add(TargetCurNode);
                    FinalNodeStack.Push(TargetCurNode); 
                    TargetCurNode = TargetCurNode.ParentNode; 
                }
                //FinalNodeList.Add(StartNode);
                //FinalNodeList.Reverse();//
                FinalNodeStack.Push(StartNode); //TargetCurNode가 시작노드가 아닐때까지 검색했으므로 시작노드가 빠져있어서 더해줌.

                //for (int i = 0; i < FinalNodeList.Count; i++) 
                //    print(i + "번째는 " + FinalNodeList[i].x + ", " + FinalNodeList[i].y);

                //길을 찾고싶던 대상을 이동시키든 할일하기. 
                DrawLine(); //검출된 길 그리기.
                return;
            }

            //뭐가됐건간에 OpenListAdd의 순서는 상관없음.
            //각 필요한 방향에 대해 검사하여 오픈리스트를 채움

            // ↑ → ↓ ←
            OpenListAdd(CurNode.x, CurNode.y + 1); 
            OpenListAdd(CurNode.x + 1, CurNode.y);
            OpenListAdd(CurNode.x, CurNode.y - 1);
            OpenListAdd(CurNode.x - 1, CurNode.y);
            
            // ↗↖↙↘
            if (allowDiagonal) //대각선 체크 순서는 상관없음.
            {
                OpenListAdd(CurNode.x + 1, CurNode.y + 1);
                OpenListAdd(CurNode.x - 1, CurNode.y + 1);
                OpenListAdd(CurNode.x - 1, CurNode.y - 1);
                OpenListAdd(CurNode.x + 1, CurNode.y - 1);
            }
        }        
    }

    void OpenListAdd(int checkX, int checkY) //가능성 있는 친구들을 오픈리스트에 넣어둠.
    {
        // 상하좌우 범위를 벗어나지 않고, 벽이 아니면서, 닫힌리스트에 없다면
        if (checkX >= bottomLeft.x && checkX <= topRight.x && checkY >= bottomLeft.y && checkY <= topRight.y  //맵 범위 안인지 체크
            && NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y].isWall ==false //벽이아닌게 맞는지 체크
            && ClosedList.Contains(NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y]) ==false ) //닫힌 리스트에 없는지 체크
        {
            // 대각선 허용시, 벽 사이로 통과 안됨
            if (allowDiagonal) 
                if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall 
                    && NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) //현재 대각선의 반대 대각선 체크.
                    return;

            //00     10
            //01     00     1을 벽이라고 쳤을때 뭔가 저런상황이면 오른쪽 상단 대각선으로 갈 수 없다고 하는것.
            // 대각선으로 지나가되 지나가는 길 어느쪽이라도 벽이 있으면 안됨(대각선이 벽과 맞닿으면 안됨)  
            if (dontCrossCorner) 
                if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall 
                    || NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) //현재 대각선의 반대대각선 어디라도 블럭이있으면 막혀있다고 보는것.
                    return;

            
            // 이웃노드에 넣고, 직선은 10, 대각선은 14비용
            Node NeighborNode = NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y]; //이웃 노드 정보 먼저 가져오기.
            int MoveCost = CurNode.G + ((CurNode.x - checkX == 0 || CurNode.y - checkY == 0) ? 10 : 14); //여태 이동한 G값 + CurNode와 상하좌우 상태면 x혹은 y가 뺐을때 0이 나올것, 그게 아니라면 대각선


            // 이동비용이 이웃노드G보다 작거나 또는 열린리스트에 이웃노드가 없다면 G, H, ParentNode를 설정 후 열린리스트에 추가            
            if (!OpenList.Contains(NeighborNode) || MoveCost < NeighborNode.G) //오픈리스트에 없다면 새로운 블럭검사 /새로운 기준점CurNode에 대해 계산한 MoveCost가 기존의 노드의 G 보다 효율적일경우 OpenList에 더함.
            {
                NeighborNode.G = MoveCost; //네이버노드의 G갱신 및 값 넣어주기
                NeighborNode.H = (Mathf.Abs(NeighborNode.x - TargetNode.x) + Mathf.Abs(NeighborNode.y - TargetNode.y)) * 10; //단순 가로 세로로만 계산...
                NeighborNode.ParentNode = CurNode;//여기 이웃에 오게 만든 CurNode를 부모 노드로 설정함.

                if (OpenList.Contains(NeighborNode) == false)
                {
                    OpenList.Add(NeighborNode);
                }                
            }
        }
    }

    //void OnDrawGizmos()
    //{
    //    if (FinalNodeList.Count != 0) for (int i = 0; i < FinalNodeList.Count - 1; i++)
    //            Gizmos.DrawLine(new Vector2(FinalNodeList[i].x, FinalNodeList[i].y), new Vector2(FinalNodeList[i + 1].x, FinalNodeList[i + 1].y));
    //}
    void DrawLine()
    {
        if (FinalNodeStack.Count != 0)
        {
            Vector3 tempPos = Vector3.zero; //임시변수 선언
            line.positionCount = FinalNodeStack.Count; //포지션 배열의 개수 지정이 가능해짐
            Node tempNode = null; //임시변수 선언
            for (int i = 0; i < line.positionCount; i++)
            {
                tempNode= FinalNodeStack.Pop();
                tempPos.x = tempNode.x;
                tempPos.y = tempNode.y;
                line.SetPosition(i, tempPos);
            }            
        }            
    }
}
