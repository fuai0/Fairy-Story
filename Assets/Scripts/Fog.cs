using UnityEngine.Tilemaps;
using UnityEngine;
using Autodesk.Fbx;

public class FogController : MonoBehaviour
{
    public Tilemap fogTilemap;
    public TileBase fogTile;

    public float exploreRadius;
    public LayerMask fogLayer;

    void Start()
    {
        InitializeFog();

        InvokeRepeating("UpdateExploration", 0, .2f);
    }

    void InitializeFog()
    {
        fogTilemap.CompressBounds();
        BoundsInt bounds = fogTilemap.cellBounds;

        // ��fogTile����������ͼ
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                fogTilemap.SetTile(new Vector3Int(x, y, 0), fogTile);
            }
        }
    }

    void UpdateExploration()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(PlayerManager.instance.player.transform.position, exploreRadius, fogLayer);
        Debug.Log("Detected hits: " + hits.Length); // ��ӡ����⵽����������
        foreach (var hit in hits)
        {
            Vector3Int cellPos = fogTilemap.WorldToCell(hit.transform.position);
            fogTilemap.SetTile(cellPos, null); // ���������Ƭ
        }
    }
}