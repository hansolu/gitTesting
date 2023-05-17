using UnityEngine;
using System.Collections;
using System;

// 이동 경로 노드 (위치, 이동 가능 여부)
public class CPathNode : SettlersEngine.IPathNode<System.Object>
{   
    // 위치 x, y
	public Int32 X { get; set; } 
	public Int32 Y { get; set; }
    // 이동 불가
	public Boolean IsWall {get; set;}
	
    // 이동 가능 여부 체크
	public bool IsWalkable(System.Object unused)
	{
		return !IsWall;
	}
}
