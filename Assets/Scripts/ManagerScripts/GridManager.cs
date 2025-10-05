using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Grid grid;
    public static GridManager Instance { get; private set; }
    public class CellInfo
    {
        public bool occupied;
        public GameObject hasIn;
    }

    [SerializeField] private Tilemap groundTile;
    private Dictionary<Vector3Int, CellInfo> groundCells = new Dictionary<Vector3Int, CellInfo>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        InitializeTiles();
    }

    private void InitializeTiles()
    {
        foreach (Vector3Int cells in groundTile.cellBounds.allPositionsWithin)
        {
            if (groundTile.HasTile(cells))
            {
                groundCells[cells] = new CellInfo { occupied = false, hasIn = null };
            }
        }
    }

    public bool ContainsCell(Vector3Int cellPos)
    {
        return groundCells.ContainsKey(cellPos);
    }

    public bool CanBeOccupied(Vector3Int cellpos)
    {
        if (ContainsCell(cellpos))
        {
            if (!groundCells[cellpos].occupied)
            {
                return true;
            }
        }
        return false;
    }

    public void OccupyCell(Vector3Int cellPos, GameObject hasObject)
    {
        if (!ContainsCell(cellPos) || groundCells[cellPos].occupied)
        {
            return;
        }
        groundCells[cellPos].occupied = true;
        groundCells[cellPos].hasIn = hasObject;
        Debug.Log("ocupada");
    }

    public void FreeCell(Vector3Int cellPos)
    {
        if (!ContainsCell(cellPos) || !groundCells[cellPos].occupied)
        {
            return;
        }
        groundCells[cellPos].occupied = false;
        groundCells[cellPos].hasIn = null;
        Debug.Log("liberada");
    }

    public Vector3Int MouseToCellPos()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        return grid.WorldToCell(mousePosition);
    }

    public Vector3 MouseToCellCenter()
    {
        Vector3Int mouseCellPos = MouseToCellPos();
        return grid.GetCellCenterWorld(mouseCellPos);
    }

    public Grid GetGrid()
    {
        return grid;
    }

    public Vector3Int WorldToCellFunc(Vector3 worldPosition)
    {
        return grid.WorldToCell(worldPosition);
    }

    public Vector3 GetCellCenterWorldFunc(Vector3Int cellPos)
    {
        return grid.GetCellCenterWorld(cellPos);
    }
}
