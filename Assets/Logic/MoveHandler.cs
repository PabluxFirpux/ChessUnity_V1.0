using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHandler : MonoBehaviour
{
    public ChessBoard board;
    int start;
    int end;

    private void Start()
    {
        reset();
    }

    private void Update()
    {
        if(start >= 0 && end >= 0)
        {
            //Falta revisar si esta en la lista, y eliminar los bloqueados por las piezas
            board.makeMove(start, end);
            reset();
        }
    }

    public  void reset()
    {
        // Debug.Log("Reset");
        board.deGlow();
        start = -1;
        end = -1;
    }

    public  void click(int a)
    {
        if (start < 0 && end < 0)
        {
            if (board.isFree(a)) return;
            if (board.GetPiece(a).getTeam() != board.getTeam()) return; 
            board.glow(MoveGenerator.possibleMoves(board.GetPiece(a)));
            start = a;
        } else if (start > end)
        {
            end = a;
        } else
        {
            reset();
        }
    }
}