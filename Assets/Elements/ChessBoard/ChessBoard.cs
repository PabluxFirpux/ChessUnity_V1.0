using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ChessBoard : MonoBehaviour
{
    public MoveHandler moveHandler;
    public Tile preFabTile;
    private Tile[] tiles;
    public Piece preFabPiece;
    private Piece[] pieces;
    Piece.Team currentTeam;
    // Start is called before the first frame update
    void Start()
    {
        makeBoard();
        putStartPieces();
        currentTeam = Piece.Team.WHITE;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void putStartPieces()
    {
        pieces = new Piece[64];


        //Both lines of pawns
        for(int i = 8; i <= 15; i++)
        {
            pieces[i] = Instantiate<Piece>(preFabPiece);
            pieces[i].PieceConstructor(i, Piece.TypeOfPiece.PAWN, Piece.Team.BLACK);
        }
        for (int i = 48; i <= 55; i++)
        {
            pieces[i] = Instantiate<Piece>(preFabPiece);
            pieces[i].PieceConstructor(i, Piece.TypeOfPiece.PAWN, Piece.Team.WHITE);
        }


        //Black Pieces
        int numb = 0;
        Piece.Team t = Piece.Team.BLACK;

        pieces[numb+0] = Instantiate<Piece>(preFabPiece);
        pieces[numb+0].PieceConstructor(numb + 0, Piece.TypeOfPiece.ROOK, t);
        pieces[numb +7] = Instantiate<Piece>(preFabPiece);
        pieces[numb + 7].PieceConstructor(numb + 7, Piece.TypeOfPiece.ROOK, t);
        pieces[numb + 1] = Instantiate<Piece>(preFabPiece);
        pieces[numb + 1].PieceConstructor(numb + 1, Piece.TypeOfPiece.KNIGHT, t);
        pieces[numb + 6] = Instantiate<Piece>(preFabPiece);
        pieces[numb + 6].PieceConstructor(numb + 6, Piece.TypeOfPiece.KNIGHT, t);
        pieces[numb + 2] = Instantiate<Piece>(preFabPiece);
        pieces[numb + 2].PieceConstructor(numb + 2, Piece.TypeOfPiece.BISHOP, t);
        pieces[numb + 5] = Instantiate<Piece>(preFabPiece);
        pieces[numb + 5].PieceConstructor(numb + 5, Piece.TypeOfPiece.BISHOP, t);
        pieces[numb + 3] = Instantiate<Piece>(preFabPiece);
        pieces[numb + 3].PieceConstructor(numb + 3, Piece.TypeOfPiece.KING, t);
        pieces[numb + 4] = Instantiate<Piece>(preFabPiece);
        pieces[numb + 4].PieceConstructor(numb + 4, Piece.TypeOfPiece.QUEEN, t);


        //White Pieces
        numb = 56;
        t = Piece.Team.WHITE;

        pieces[numb + 0] = Instantiate<Piece>(preFabPiece);
        pieces[numb + 0].PieceConstructor(numb + 0, Piece.TypeOfPiece.ROOK, t);
        pieces[numb + 7] = Instantiate<Piece>(preFabPiece);
        pieces[numb + 7].PieceConstructor(numb + 7, Piece.TypeOfPiece.ROOK, t);
        pieces[numb + 1] = Instantiate<Piece>(preFabPiece);
        pieces[numb + 1].PieceConstructor(numb + 1, Piece.TypeOfPiece.KNIGHT, t);
        pieces[numb + 6] = Instantiate<Piece>(preFabPiece);
        pieces[numb + 6].PieceConstructor(numb + 6, Piece.TypeOfPiece.KNIGHT, t);
        pieces[numb + 2] = Instantiate<Piece>(preFabPiece);
        pieces[numb + 2].PieceConstructor(numb + 2, Piece.TypeOfPiece.BISHOP, t);
        pieces[numb + 5] = Instantiate<Piece>(preFabPiece);
        pieces[numb + 5].PieceConstructor(numb + 5, Piece.TypeOfPiece.BISHOP, t);
        pieces[numb + 3] = Instantiate<Piece>(preFabPiece);
        pieces[numb + 3].PieceConstructor(numb + 3, Piece.TypeOfPiece.KING, t);
        pieces[numb + 4] = Instantiate<Piece>(preFabPiece);
        pieces[numb + 4].PieceConstructor(numb + 4, Piece.TypeOfPiece.QUEEN, t);
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
       //TODO: Implement
       if(startPos == endpos) return;
       if (pieces[startPos] == null) return;
       if (pieces[startPos].getTeam() != currentTeam) return;
       if (pieces[endpos] != null && pieces[endpos].getTeam() == currentTeam) return;

        if(!isFree(endpos)) pieces[endpos].destroy();
        pieces[endpos] = pieces[startPos];
        pieces[startPos] = null;
        pieces[endpos].setPos(endpos);


        changeTurns();
    }

    void changeTurns()
    {
        if(currentTeam == Piece.Team.WHITE)
        {
            currentTeam = Piece.Team.BLACK;
        } else
        {
            currentTeam = Piece.Team.WHITE;
        }
    }

    public bool isFree(int pos)
    {
        return pieces[pos] == null;
    }

    public void glow(int x)
    {
        tiles[x].Glow();
    }

    public void glow(List<int> til)
    {
        til.ForEach(glow);
    }

    public void deGlow()
    {
        for(int x = 0; x < tiles.Length; x++)
        {
            tiles[x].desactivarBrillo();
        }
    }

    public Piece GetPiece(int x)
    {
        if (isFree(x)) return null;
        return pieces[x];
    }

    public Piece.Team getTeam()
    {
        return currentTeam;
    }
}
