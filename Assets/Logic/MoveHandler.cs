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
            board.makeMove(start, end);
            reset();
        }
    }

    public  void reset()
    {
       // Debug.Log("Reset");
        start = -1;
        end = -1;
    }

    public  void click(int a)
    {
        if (start < 0 && end < 0)
        {
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