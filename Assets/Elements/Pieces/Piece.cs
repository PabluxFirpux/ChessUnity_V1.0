using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    TypeOfPiece pieceType;
    Team team;
    public int pos;

    // Start is called before the first frame update
    void Start()
    {
        pos = 1;//TO remove
    }

    // Update is called once per frame
    void Update()
    {
        draw();
    }

    public enum TypeOfPiece
    {
        PAWN,
        ROOK,
        KNIGHT,
        BISHOP,
        QUEEN,
        KING
    }

    public enum Team
    {
        BLACK,
        WHITE
    }

    void draw()
    {
        int ypos = this.pos / 8;
        int xpos = this.pos % 8;
        transform.position = new Vector3(-3.5f+xpos,3.5f- ypos, transform.position.z);
    }

    public void setPos(int x)
    {
        this.pos = x;
    }

    public int getPos()
    {
        return this.pos;
    }
}
