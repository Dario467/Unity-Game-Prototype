using System.Collections.Generic;
using UnityEngine;

public class PieceControll : MonoBehaviour, IControllable
{
    private Selectable selectable;
    private PieceMove pieceMove;
    private List<Vector3Int> pieceMoveSet;

    public void SetMoveSet(List<Vector3Int> _moveSet)
    {
        pieceMoveSet = _moveSet;
    }

    void Start()
    {
        InitializePiece();
        selectable = GetComponent<Selectable>();
        pieceMove = GetComponent<PieceMove>();
        selectable.SetMoveSet(pieceMoveSet);
    }

    void OnEnable()
    {
        CursorManager.MissClick += TryMove;
        PieceMove.StopMoving += IStopMoving;
    }

    void OnDisable()
    {
        CursorManager.MissClick -= TryMove;
        PieceMove.StopMoving -= IStopMoving;
    }

    public void IStopMoving(PieceMove _piceMove)
    {
        if (pieceMove == _piceMove)
        {
            if (selectable.GetIsSelected())
            {
                selectable.ShowSelectVisuals();
            }
        }
    }

    public void TryMove()
    {
        if (selectable == SelectionManager.Instance.GetCurrentSelection())
        {
            if (selectable.CanMoveToMouse())
            {
                selectable.HideSelectVisuals();
                pieceMove.StartMove(GridManager.Instance.MouseToCellCenter());
            }
            else
            {
                Debug.Log("Se deselecciono");
                SelectionManager.Instance.TryDeselect(selectable);
            }
        }
    }

    public void Clicked()
    {
        SelectionManager.Instance.TrySelect(selectable);
    }
    
    private void InitializePiece()
    {
        Vector3Int cellPos = GridManager.Instance.GetGrid().WorldToCell(transform.position);
        GridManager.Instance.OccupyCell(cellPos, this.gameObject);
        transform.position = GridManager.Instance.GetGrid().GetCellCenterWorld(cellPos);
    }
}
