using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    private Sprite sprite;
    private SpriteRenderer spriteRenderer;
    TypeOfPiece pieceType;
    Team team;
    public int pos;
    public int pastPos;
    public Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PieceConstructor(int pos, TypeOfPiece typeOfPiece, Team team) 
    {
        this.pos = pos;
        this.team = team;
        this.pieceType = typeOfPiece;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        sprite = selectSprite();
        spriteRenderer.sprite = sprite;
        draw();
    }

    // Update is called once per frame
    void Update()
    {
        if(pos != pastPos)
        {
            draw();
            pastPos = pos;
        }
    }

    Sprite selectSprite()
    {
        switch(pieceType)
        {
            case TypeOfPiece.PAWN:
                return team.Equals(Team.WHITE) ? sprites[0] : sprites[6];
            case TypeOfPiece.ROOK:
                return team.Equals(Team.WHITE) ? sprites[1] : sprites[7];
            case TypeOfPiece.KNIGHT:
                return team.Equals(Team.WHITE) ? sprites[2] : sprites[8];
            case TypeOfPiece.BISHOP:
                return team.Equals(Team.WHITE) ? sprites[3] : sprites[9];
            case TypeOfPiece.QUEEN:
                return team.Equals(Team.WHITE) ? sprites[4] : sprites[10];
            case TypeOfPiece.KING:
                return team.Equals(Team.WHITE) ? sprites[5] : sprites[11];
            default: return null;
        }
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

    public Team getTeam()
    {
        return this.team;
    }

    public TypeOfPiece getType()
    {
        return this.pieceType;
    }

    public void destroy()
    {
        Destroy(gameObject);
    }
}
