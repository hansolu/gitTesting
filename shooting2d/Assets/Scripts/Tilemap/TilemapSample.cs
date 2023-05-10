using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapSample : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase tile; //타일 그림...

    public TileBase tile_A;
    public TileBase tile_B;

    void Start()
    {        
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                tilemap.SetTile(new Vector3Int(i,j,0), tile); //타일 기존에 있다면 교체하고, 없다면 새로 만들기                
            }
        }
        tilemap.SetColor(new Vector3Int(-1, 0, 0), Color.black); //원하는 색으로 타일 바꾸기 

        tilemap.SwapTile(tile_A, tile_B); //
        //tilemap.SwapTile(tilemap.GetTile(new Vector3Int(0,0,0)), tile_B);
    }
}
