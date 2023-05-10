using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapSample : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase tile; //Ÿ�� �׸�...

    public TileBase tile_A;
    public TileBase tile_B;

    void Start()
    {        
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                tilemap.SetTile(new Vector3Int(i,j,0), tile); //Ÿ�� ������ �ִٸ� ��ü�ϰ�, ���ٸ� ���� �����                
            }
        }
        tilemap.SetColor(new Vector3Int(-1, 0, 0), Color.black); //���ϴ� ������ Ÿ�� �ٲٱ� 

        tilemap.SwapTile(tile_A, tile_B); //
        //tilemap.SwapTile(tilemap.GetTile(new Vector3Int(0,0,0)), tile_B);
    }
}
