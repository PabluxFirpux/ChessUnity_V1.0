using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public MoveHandler moveHandler;
    private Color whiteTile = new Color(200/255f, 195/255f, 150 / 255f);
    private Color blackTile = new Color(176 / 255f, 107 / 255f, 56 / 255f);
    private Color highlightDark = new Color(56/255f,82/255f,57/255f);
    private Color highlightLight = new Color(123/255f,159/255f,101/255f);
    private SpriteRenderer _sprite;
    private float xpos;
    private float ypos;
    private int number;
    private Color color;
    private Color highlight;

    public void Tilemaker(int number, float xpos, float ypos, bool isWhite, MoveHandler mh)
    {
        this.number = number;
        this.xpos = xpos;
        this.ypos = ypos;
        this._sprite = GetComponent<SpriteRenderer>();
        color = isWhite ? whiteTile : blackTile;
        highlight = isWhite ? highlightLight : highlightDark;
        moveHandler = mh;
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(xpos, ypos, transform.position.z);
        _sprite.color = color;
    }

    public void Glow()
    {
        _sprite.color = highlight;
    }

    public void desactivarBrillo()
    {
        _sprite.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getNumber()
    {
        return this.number;
    }

    void OnMouseDown()
    {
        moveHandler.click(number);
    }
}
