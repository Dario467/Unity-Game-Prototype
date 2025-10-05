using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Selectable : MonoBehaviour, ISelectable
{
    private MyHighlights myHighlights;
    private bool isSelected;
    public bool IsSelected => isSelected;
    private HashSet<Vector3Int> validMovesCell;
    private List<Vector3Int> moveSet;

    void Awake()
    {
        validMovesCell = new HashSet<Vector3Int>();
    }

    void Start()
    {
        myHighlights = GetComponent<MyHighlights>();
    }

    public void SetMoveSet(List<Vector3Int> _moveSet)
    {
        moveSet = _moveSet;
    }

    public void Select()
    {
        isSelected = true;
        ShowSelectVisuals();
    }

    public void Deselect()
    {
        isSelected = false;
        HideSelectVisuals();
    }

    public bool GetIsSelected()
    {
        return isSelected;
    }

    public void ShowSelectVisuals()
    {
        SetValidMoveCells();
        foreach (Vector3Int validMove in validMovesCell)
        {
            myHighlights.MyHighLightCreate(validMove, HighLightManager.Instance.YELLOW, 0.1f);
        }
    }

    public void HideSelectVisuals()
    {
        validMovesCell.Clear();
        myHighlights.MyHighLightsDestroy();
    }

    public void SetValidMoveCells()
    {
        if (validMovesCell == null || validMovesCell.Count == 0)
        {
            validMovesCell = ValidMovesCalculator.CalculateValidsMove(transform.position, moveSet);
        }
    }

    public bool CanMoveToMouse()
    {
        Vector3Int mouseCellPos = GridManager.Instance.MouseToCellPos();
        return validMovesCell.Contains(mouseCellPos);
    }

}
