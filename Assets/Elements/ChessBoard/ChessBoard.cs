using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ChessBoard : MonoBehaviour
{
    public MoveHandler moveHandler;
    public Tile preFabTile;
    public Piece piece;
    private Tile[] tiles;
    // Start is called before the first frame update
    void Start()
    {
        makeBoard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void makeBoard()
    {
        tiles = new Tile[64];
        float startIpos = 3.5f;
        for (int i = 0; i < 8; i++)
        {
            float startJpos = -3.5f;
            for (int j = 0; j < 8; j++)
            {
                tiles[i * 8 + j] = Instantiate(preFabTile);
                bool isWhite = false;
                if (i % 2 == 0 && j % 2 == 0)
                {
                    isWhite = true;
                }
                else if (i % 2 == 1 && j % 2 == 1)
                {
                    isWhite = true;
                }
                tiles[i * 8 + j].Tilemaker((i * 8) + j, startJpos + j, startIpos - i, isWhite, moveHandler);
            }
        }
    }

    public void makeMove(int startPos, int endpos)
    {
        if(piece.getPos() == startPos)
        {
            piece.setPos(endpos); 
        }
    }
}
