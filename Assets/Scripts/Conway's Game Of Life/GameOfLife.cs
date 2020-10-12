using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOfLife : GridObject
{
    // Start is called before the first frame update
    
    void Start()
    {
        intialiseGrid(randomAtStart,-1.0f,-0.8f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            stepConway();
            drawStates();
            generations.Add(states);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            reverseState();
        }

        if (Input.GetMouseButtonDown(0))
        {
            setCell();
            drawStates();
            generations.Add(states);
        }
    }

    void stepConway()
    {
        int[,] next = new int[rows, cols];

        for (int y = 1; y < cols - 1; y++)
        {
            for (int x = 1; x < rows - 1; x++)
            {
                int neighbors = 0;
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        neighbors += states[(x + i), (y + j)];
                    }
                }
                neighbors -= states[x, y];

                if (states[x, y] == 1 && neighbors < 2)
                {
                    next[x, y] = 0;
                }
                else if (states[x, y] == 1 && neighbors > 3)
                {
                    next[x, y] = 0;
                }
                else if (states[x, y] == 0 && neighbors == 3)
                {
                    next[x, y] = 1;
                }
                else
                {
                    next[x, y] = states[x, y];
                }
            }
        }
        states = next;
    }
}
