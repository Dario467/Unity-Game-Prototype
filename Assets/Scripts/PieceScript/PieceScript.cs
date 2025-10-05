using System.Collections.Generic;
using UnityEngine;
public class PieceScript: MonoBehaviour
{
    private PieceControll pieceControll;
    private void Awake()
    {
        pieceControll = GetComponent<PieceControll>();
        List<Vector3Int> moveSet = new()
        {
            new Vector3Int(1, 0, 0),
            new Vector3Int(2, 0, 0),
            new Vector3Int(-1, 0, 0),
            new Vector3Int(-2, 0, 0),
            new Vector3Int(0, 1, 0),
            new Vector3Int(0, 2, 0),
            new Vector3Int(0, -1, 0),
            new Vector3Int(0, -2, 0),
            new Vector3Int(1, 1, 0),
            new Vector3Int(-1, -1, 0),
            new Vector3Int(1, -1, 0),
            new Vector3Int(-1, 1, 0),
        };
        pieceControll.SetMoveSet(moveSet);
    }    
}
