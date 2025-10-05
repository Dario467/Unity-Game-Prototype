using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PieceMove : MonoBehaviour, IMove
{
    [SerializeField] private float speed;
    private Vector3 toMovePos;
    private Vector3 initialPos;
    private Animator anim;

    public static event Action<PieceMove> StopMoving;

    private bool isMoving;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            MoveToCell();
            if (transform.position == toMovePos)
            {
                isMoving = false;
                anim.SetBool("isMoving",isMoving);
                StopMoving?.Invoke(this);
                CenterPiece();
            }
        }
    }

    public void MoveToCell()
    {
        transform.position = Vector3.MoveTowards(transform.position, toMovePos, speed * Time.deltaTime);
        //Vector3 direction = new Vector3(toMovePos.x - initialPos.x, toMovePos.y - initialPos.y, 0).normalized;
        //transform.position += speed * Time.deltaTime * direction;
    }

    public void StartMove(Vector3 toMove)
    {
        GridManager.Instance.FreeCell(GridManager.Instance.WorldToCellFunc(transform.position));
        GridManager.Instance.OccupyCell(GridManager.Instance.MouseToCellPos(), this.gameObject);
        initialPos = transform.position;
        toMovePos = toMove;
        isMoving = true;
        anim.SetBool("isMoving",isMoving);
    }

    private void CenterPiece()
    {
        Vector3Int cellPos = GridManager.Instance.WorldToCellFunc(transform.position);
        transform.position = GridManager.Instance.GetGrid().GetCellCenterWorld(cellPos);
    }

    public bool IsMoving() {
        return isMoving;
    }
}
