using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 경로 찾기 해결사 클래스
public class PathFindSolver<TPathNode, TUserContext> : SettlersEngine.SpatialAStar<TPathNode,
TUserContext> where TPathNode : SettlersEngine.IPathNode<TUserContext>
{
    protected override Double Heuristic(PathNode inStart, PathNode inEnd)
    {
        int formula = CTileManager.formula;
        int dx = Math.Abs(inStart.X - inEnd.X);
        int dy = Math.Abs(inStart.Y - inEnd.Y);

        if (formula == 0)
            return Math.Sqrt(dx * dx + dy * dy); // 유클리드 거리 (최단 거리)
        else if (formula == 1)
            return (dx * dx + dy * dy); // 유클리드 거리 제곱 (최단 거리)
        else if (formula == 2)
            return Math.Min(dx, dy); // 대각선 최소 (x,y중 짧은쪽으로)
        else if (formula == 3)
            return (dx * dy) + (dx + dy); // 맨하탄 거리 (격자)
        else
            return Math.Abs(inStart.X - inEnd.X) + Math.Abs(inStart.Y - inEnd.Y); // 맨하탄 거리 (격자)
        //return Math.Abs(inStart.X - inEnd.X) + Math.Abs(inStart.Y - inEnd.Y);
        //return 1*(Math.Abs(inStart.X - inEnd.X) + Math.Abs(inStart.Y - inEnd.Y) - 1); //optimized tile based Manhatten
        //return ((dx * dx) + (dy * dy)); //Khawaja distance
    }

    protected override Double NeighborDistance(PathNode inStart, PathNode inEnd)
    {
        return Heuristic(inStart, inEnd);
    }

    public PathFindSolver(TPathNode[,] inGrid)
        : base(inGrid)
    {
    }
}
