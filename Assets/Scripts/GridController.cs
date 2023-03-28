using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{

    [SerializeField] private GameObject piecePrefab1;
    [SerializeField] private GameObject piecePrefab2;
    [SerializeField] private GameObject piecePrefab3;

    [SerializeField] private Vector3 originPosition;

    private Piece[,] grid = new Piece[8, 8];

    // [[0, 1, 2, 3, 4], [], [], []]

    // Start is called before the first frame update
    void Start()
    {
        for (int row = 0; row < grid.GetLength(0); row++)
        {
            // Store the previous piece's type to avoid spawning the same type above/below
            int previousPieceType = -1;

            // Initialize a list of available piece types
            List<int> availablePieceTypes = new List<int> { 1, 2, 3 };

            for (int column = 0; column < grid.GetLength(1); column++)
            {
                // Remove the previous piece's type from the available list if it exists
                if (previousPieceType != -1)
                {
                    availablePieceTypes.Remove(previousPieceType);
                }

                // Spawn a random piece from the available list
                int randomIndex = Random.Range(0, availablePieceTypes.Count);
                int pieceType = availablePieceTypes[randomIndex];

                Vector3 newWorldPosition = new Vector3(originPosition.x + row, originPosition.y, originPosition.z - column);

                grid[row, column] = new Piece(newWorldPosition, new Vector2(row, column));

                if (pieceType == 1)
                {
                    Instantiate(piecePrefab1, grid[row, column].GetPosition(), Quaternion.identity);
                }
                else if (pieceType == 2)
                {
                    Instantiate(piecePrefab2, grid[row, column].GetPosition(), Quaternion.identity);
                }
                else
                {
                    Instantiate(piecePrefab3, grid[row, column].GetPosition(), Quaternion.identity);
                }

                // Update the previous piece's type
                previousPieceType = pieceType;

                // Add the next piece type to the available list
                if (availablePieceTypes.Count < 3)
                {
                    availablePieceTypes.Add(previousPieceType % 3 + 1);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}