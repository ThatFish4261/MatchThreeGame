using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PieceTypes
{
    Red = 0,
    Blue = 1,
    Green = 2,
    Pink = 3,
    Cyan = 4,
    Blank = 5
}

public class Piece
{
    private Vector3 position;
    private Vector2 gridPosition;
    private PieceTypes pieceType;
    private bool setForDestruction;

    public Piece()
    {
        position = Vector3.zero;
        gridPosition = Vector2.zero;
        pieceType = PieceTypes.Pink;
        setForDestruction = false;
    }

    public Piece(Vector3 position, Vector2 gridPosition)
    {
        this.position = position;
        this.gridPosition = gridPosition;
        this.pieceType = PieceTypes.Pink;
        this.setForDestruction = false;
    }

    public Piece(Vector3 position, Vector2 gridPosition, PieceTypes pieceType)
    {
        this.position = position;
        this.gridPosition = gridPosition;
        this.pieceType = pieceType;
        this.setForDestruction = false;
    }

    public Vector3 GetPosition()
    {
        return position;
    }

    public PieceTypes GetPieceType()
    {
        return pieceType;
    }
    public void SetForDestruction()
    {
        this.setForDestruction = true;
    }
    public void SetForDestruction(bool value)
    {
        this.setForDestruction = value;
    }

    public void SetPieceType(PieceTypes pieceType)
    {
        this.pieceType = pieceType;
    }

    public void SetGridPosition(Vector2 position)
    {
        this.gridPosition = position;
    }

    public Vector3 GetGridPosition()
    {
        return gridPosition;
    }

    public bool GetDestruction()
    {
        return setForDestruction;
    }
}