using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{

    private Vector3 position;
    private Vector2 gridPosition;
    private GameObject piecePrefab;

    // Start is called before the first frame update
    public Piece()
    {
        position = Vector3.zero;
        gridPosition = Vector2.zero;
    }

    public Piece(Vector3 position, Vector2 gridPosition, GameObject piecePrefab)
    {
        this.position = position;
        this.gridPosition = gridPosition;
        this.piecePrefab = piecePrefab;
    }
}