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
        // Debug.Log(grid);

        for (int row = 0; row < grid.GetLength(0); row++)
        {
            for (int column = 0; column < grid.GetLength(1); column++)
            {
                System.Random rand = new System.Random();
                int randomNumber = rand.Next(1,30);

                Vector3 newWorldPosition = new Vector3(originPosition.x + row, originPosition.y, originPosition.z - column);

                grid[row, column] = new Piece(newWorldPosition, new Vector2(row, column));
                
                if (randomNumber >= 1 && randomNumber < 10)
                {
                    Instantiate(piecePrefab1, grid[row, column].GetPosition(), Quaternion.identity);
                }

                else if (randomNumber >= 11 && randomNumber < 20)
                {
                    Instantiate(piecePrefab2, grid[row, column].GetPosition(), Quaternion.identity);
                }

                else if (randomNumber >= 21 && randomNumber < 30)
                {
                    Instantiate(piecePrefab3, grid[row, column].GetPosition(), Quaternion.identity);
                }
                else
                {
                    Instantiate(piecePrefab1, grid[row, column].GetPosition(), Quaternion.identity);
                }

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            Destroy
        }*/
    }
}