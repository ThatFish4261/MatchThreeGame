using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GridController : MonoBehaviour
{
    public bool pressedDown;
    public Vector2 pressedDownPosition;
    public Vector2 pressedUpPosition;
    public GameObject pressedDownGameObject;
    public GameObject pressedUpGameObject;

    [SerializeField] private GameObject piecePrefab1;
    [SerializeField] private GameObject piecePrefab2;
    [SerializeField] private GameObject piecePrefab3;
    [SerializeField] private GameObject piecePrefab4;

    [SerializeField] private Vector3 originPosition;
    
    [Header("UI")]
    [SerializeField] private GameObject matchesFoundText;

    private Vector2 startMovementPiecePosition;
    private Vector2 endMovementPiecePosition;

    private bool validMoveInProcess = false;

    private int matchesFound;


    private Piece[,] grid = new Piece[8, 8];

    void Start()
    {
        pressedDown = false;

        for (int row = 0; row < grid.GetLength(0); row++)
        {
            // Store the previous piece's type to avoid spawning the same type above/below
            int previousPieceType = -1;

            // Initialize a list of available piece types
            List<int> availablePieceTypes = new List<int> { 1, 2, 3, 4, 5 };

            for (int column = 0; column < grid.GetLength(1); column++)
            {
                // Remove the previous piece's type from the available list if it exists
                if (previousPieceType != -1)
                {
                    availablePieceTypes.Remove(previousPieceType);
                }

                // Spawn a random piece from the available list
                int randomIndex = UnityEngine.Random.Range(0, availablePieceTypes.Count);
                int pieceType = availablePieceTypes[randomIndex];

                Vector3 newWorldPosition = new Vector3(originPosition.x + row, originPosition.y, originPosition.z - column);

                Piece newPiece = new Piece(newWorldPosition, new Vector2(row, column));

                if (pieceType == 1)
                {
                    GameObject pieceGameObject = Instantiate(piecePrefab1, newPiece.GetPosition(), Quaternion.identity);
                    newPiece.SetPieceType(PieceTypes.Red);

                    PieceController controller = pieceGameObject.GetComponent<PieceController>();
                    controller.SetPiece(newPiece);

                }
                else if (pieceType == 2)
                {
                    GameObject pieceGameObject = Instantiate(piecePrefab2, newPiece.GetPosition(), Quaternion.identity);
                    newPiece.SetPieceType(PieceTypes.Blue);

                    PieceController controller = pieceGameObject.GetComponent<PieceController>();
                    controller.SetPiece(newPiece);
                }
                else if (pieceType == 3)
                {
                    GameObject pieceGameObject = Instantiate(piecePrefab3, newPiece.GetPosition(), Quaternion.identity);
                    newPiece.SetPieceType(PieceTypes.Green);

                    PieceController controller = pieceGameObject.GetComponent<PieceController>();
                    controller.SetPiece(newPiece);

                }
                else 
                {
                    GameObject pieceGameObject = Instantiate(piecePrefab4, newPiece.GetPosition(), Quaternion.identity);
                    newPiece.SetPieceType(PieceTypes.Pink);

                    PieceController controller = pieceGameObject.GetComponent<PieceController>();
                    controller.SetPiece(newPiece);

                }

                // Update the previous piece's type
                previousPieceType = pieceType;

                // Add the next piece type to the available list
                if (availablePieceTypes.Count < 3)
                {
                    availablePieceTypes.Add(previousPieceType % 3 + 1);
                }


                grid[row, column] = newPiece;
            }
        }
    }

    private void Update()
    {
        if (validMoveInProcess)
        {
            Debug.Log("Switching");

            // Update the visual layer
            Vector3 placeHolderPosition = pressedDownGameObject.transform.position;
            pressedDownGameObject.transform.position = pressedUpGameObject.transform.position;
            pressedUpGameObject.transform.position = placeHolderPosition;

            // Update the data layer to match the visual layer
            Piece placeHolderPiece = grid[(int)endMovementPiecePosition.x, (int)endMovementPiecePosition.y];
            grid[(int)endMovementPiecePosition.x, (int)endMovementPiecePosition.y] = grid[(int)startMovementPiecePosition.x, (int)startMovementPiecePosition.y];
            grid[(int)startMovementPiecePosition.x, (int)startMovementPiecePosition.y] = placeHolderPiece;

            validMoveInProcess = false;

            matchesFound += 1;
        }

        matchesFoundText.GetComponent<Text>().text = matchesFound.ToString();
    }

    private Piece GetGridPiece(int row, int column)
    {
        Piece foundPiece;
        try
        {
            foundPiece = grid[row, column];
            if (foundPiece == null || foundPiece.GetDestruction())
            {
                return null;
            }

            return foundPiece;
        }
        catch (IndexOutOfRangeException)  // CS0168
        {
            // Catch IndexOutOfRangeException when the grid is asked to retrieve a
            // piece from an unknown location.
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }

        return null;
    }

    private Piece GetGridPiece(int row, int column, bool isDestroyed)
    {
        Piece foundPiece;
        try
        {
            foundPiece = grid[row, column];
            if (foundPiece == null)
            {
                return null;
            }

            if (!isDestroyed)
            {
                return null;
            }

            return foundPiece;
        }
        catch (IndexOutOfRangeException)  // CS0168
        {
            // Catch IndexOutOfRangeException when the grid is asked to retrieve a
            // piece from an unknown location.
        }

        return null;
    }

    public void ValidMove(Vector2 start, Vector2 end)
    {
        Debug.Log("validating start (" + start.x + ", " + start.y + ") | end (" + end.x + ", " + end.y + ")");
        startMovementPiecePosition = start;
        endMovementPiecePosition = end;

        // Using this boolean value to not do subsequent matches
        bool matchFound = false;

        if (!matchFound)
        {
            // Get type of piece based on start position
            // and check for neighboring pieces of the
            // same type below and above the end position
            try
            {
                Piece topPiece1 = GetGridPiece((int)end.x, (int)end.y - 1);
                Piece bottomPiece1 = GetGridPiece((int)end.x, (int)end.y + 1);
                Debug.Log("Top piece type: " + topPiece1.GetPieceType());
                Debug.Log("Bottom piece type: " + bottomPiece1.GetPieceType());
                Piece midPiece1 = GetGridPiece((int)start.x, (int)start.y);
                Piece toDestroy1 = GetGridPiece((int)end.x, (int)end.y);
                Debug.Log("Mid piece type: " + midPiece1.GetPieceType());
                if (topPiece1.GetPieceType() == bottomPiece1.GetPieceType())
                {
                    if (topPiece1.GetPieceType() == midPiece1.GetPieceType())
                    {
                        matchFound = true;
                        validMoveInProcess = true;
                        topPiece1.SetForDestruction();
                        bottomPiece1.SetForDestruction();
                        toDestroy1.SetForDestruction();
                        Debug.Log("======= MATCHED =======");
                    }
                }
            }
            catch (NullReferenceException)
            {
                // Object reference not set to an instance of an object
            }
        }

        if (!matchFound)
        {
            // Checking for pattern of moving up or down and having
            // two matching types on the left
            try
            {
                Piece leftPiece = GetGridPiece((int)end.x - 1, (int)end.y);
                Piece leftLeftPiece = GetGridPiece((int)end.x - 2, (int)end.y);
                Piece checkPiece1 = GetGridPiece((int)start.x, (int)start.y);
                if (leftPiece.GetPieceType() == leftLeftPiece.GetPieceType())
                {
                    if (leftPiece.GetPieceType() == checkPiece1.GetPieceType())
                    {
                        matchFound = true;
                        validMoveInProcess = true;
                        Piece toDestroy2 = grid[(int)end.x, (int)end.y];

                        leftPiece.SetForDestruction();
                        leftLeftPiece.SetForDestruction();
                        toDestroy2.SetForDestruction();
                        Debug.Log("======= MATCHED =======");
                    }
                }
            }
            catch (NullReferenceException)
            {
                // Object reference not set to an instance of an object
            }
        }

        if (!matchFound)
        {
            // Checking for pattern of moving up or down and having
            // two matching types on the right
            try
            {
                Piece rightPiece = GetGridPiece((int)end.x + 1, (int)end.y);
                Piece rightRightPiece = GetGridPiece((int)end.x + 2, (int)end.y);
                Piece checkPiece2 = GetGridPiece((int)start.x, (int)start.y);
                if (rightPiece.GetPieceType() == rightRightPiece.GetPieceType())
                {
                    if (rightPiece.GetPieceType() == checkPiece2.GetPieceType())
                    {
                        matchFound = true;
                        validMoveInProcess = true;
                        Piece toDestroy2 = GetGridPiece((int)end.x, (int)end.y);

                        rightPiece.SetForDestruction();
                        rightRightPiece.SetForDestruction();
                        toDestroy2.SetForDestruction();
                        Debug.Log("======= MATCHED =======");
                    }
                }
            }
            catch (NullReferenceException)
            {
                // Object reference not set to an instance of an object
            }
        }

        if (!matchFound)
        {
            // Checking for pattern of moving up or down and having
            // two matching types on either side
            try
            {
                Piece rightPiece = GetGridPiece((int)end.x + 1, (int)end.y);
                Piece leftPiece = GetGridPiece((int)end.x - 1, (int)end.y);
                Piece checkPiece3 = GetGridPiece((int)start.x, (int)start.y);
                if (rightPiece.GetPieceType() == leftPiece.GetPieceType())
                {
                    if (rightPiece.GetPieceType() == checkPiece3.GetPieceType())
                    {
                        matchFound = true;
                        validMoveInProcess = true;
                        Piece toDestroy2 = GetGridPiece((int)end.x, (int)end.y);

                        rightPiece.SetForDestruction();
                        leftPiece.SetForDestruction();
                        toDestroy2.SetForDestruction();
                        Debug.Log("======= MATCHED =======");
                    }
                }
            }
            catch (NullReferenceException)
            {
                // Object reference not set to an instance of an object
            }
        }

        if (!matchFound)
        {
            // Checking for pattern of moving up, down, left, or right and having
            // two matching types below
            try
            {
                Piece belowPiece = GetGridPiece((int)end.x, (int)end.y + 1);
                Piece belowBelowPiece = GetGridPiece((int)end.x, (int)end.y + 2);
                Piece checkPiece4 = GetGridPiece((int)start.x, (int)start.y);
                if (belowPiece.GetPieceType() == belowBelowPiece.GetPieceType())
                {
                    if (belowPiece.GetPieceType() == checkPiece4.GetPieceType())
                    {
                        matchFound = true;
                        validMoveInProcess = true;
                        Piece toDestroy2 = GetGridPiece((int)end.x, (int)end.y);

                        belowPiece.SetForDestruction();
                        belowBelowPiece.SetForDestruction();
                        toDestroy2.SetForDestruction();

                        Debug.Log("======= MATCHED =======");
                    }
                }
            }
            catch (NullReferenceException)
            {
                // Object reference not set to an instance of an object
            }
        }

        if (!matchFound)
        {
            // Checking for pattern of moving up, down, left, or right and having
            // two matching types above
            try
            {
                Piece abovePiece = GetGridPiece((int)end.x, (int)end.y - 1);
                Piece aboveAbovePiece = GetGridPiece((int)end.x, (int)end.y - 2);
                Piece checkPiece4 = GetGridPiece((int)start.x, (int)start.y);
                if (abovePiece.GetPieceType() == aboveAbovePiece.GetPieceType())
                {
                    if (abovePiece.GetPieceType() == checkPiece4.GetPieceType())
                    {
                        matchFound = true;
                        validMoveInProcess = true;
                        Piece toDestroy2 = GetGridPiece((int)end.x, (int)end.y);

                        abovePiece.SetForDestruction();
                        aboveAbovePiece.SetForDestruction();
                        toDestroy2.SetForDestruction();

                        Debug.Log("======= MATCHED =======");
                    }
                }
            }
            catch (NullReferenceException)
            {
                // Object reference not set to an instance of an object
            }
        }

        GameManager gameManager = GameObject

        Debug.Log("not valid move");
    }

    public bool IsDestroyed(Vector2 gridPosition)
    {
        Piece piece = GetGridPiece((int)gridPosition.x, (int)gridPosition.y, true);
        if (piece != null)
        {
            return piece.GetDestruction();
        }
        return false;
    }
}