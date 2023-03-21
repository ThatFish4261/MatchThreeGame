using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{

    private int[,] grid = new int[8, 8];

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(grid); 

        for (int row = 0; row < grid.Length; row++)
        {
            for (int column = 0; column < grid.GetLength(1); column++)
            {
                grid[row, column] = column;
                Debug.Log(grid[row, column]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
