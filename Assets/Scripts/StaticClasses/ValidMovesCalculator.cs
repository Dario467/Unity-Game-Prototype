using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ValidMovesCalculator
{
    public static HashSet<Vector3Int> CalculateValidsMove(Vector3 actualPosition, List<Vector3Int> moveSet)
    {
        HashSet<Vector3Int> validMoves = new HashSet<Vector3Int>();
        HashSet<Vector3> invalidDirections = new HashSet<Vector3>();
        foreach (Vector3Int m in moveSet)
        {
            Vector3Int targetCell = GridManager.Instance.WorldToCellFunc(actualPosition + m);
            Vector3 direction = new Vector3(Math.Sign(m.x), Math.Sign(m.y), 0);
            if (invalidDirections.Contains(direction))
            {
                continue;
            }
            if (GridManager.Instance.CanBeOccupied(targetCell))
            {
                validMoves.Add(targetCell);
            }
            else
            {
                invalidDirections.Add(direction);
            }
        }
        return validMoves;
    }
}
