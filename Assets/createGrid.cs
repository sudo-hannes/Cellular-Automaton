using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class createGrid : MonoBehaviour
{
    public int rows = 85;
    public int cols = 48;
    public GameObject[,] cells;
    public GameObject prefab;
    private int[,] states;
    // Start is called before the first frame update

    void Start()
    {
        states = new int[rows, cols];
        cells = new GameObject[rows, cols];
        for (int y = 0; y < cols; y++)
        {
            for (int x = 0; x < rows; x++) 
            {
                states[x, y] = Random.Range(0,2);
                cells[x, y] = Instantiate(prefab, new Vector3((x*0.2f)-1.0f, (y*0.2f)-0.8f, 0), Quaternion.identity);
                if (x == 0) {
                    cells[x, y].SetActive(false);
                }
                if (y == 0)
                {
                    cells[x, y].SetActive(false);
                }
                if (y == cols)
                {
                    cells[x, y].SetActive(false);
                }
                if (y == rows)
                {
                    cells[x, y].SetActive(false);
                }
            }
        }
        drawStates();


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            stepConway();
            drawStates();
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

    void drawStates()
    {
        for (int y = 0; y < cols; y++)
        {
            for (int x = 0; x < rows; x++)
            {
                SpriteRenderer rend = cells[x, y].GetComponent<SpriteRenderer>();
                if (states[x, y] == 1)
                {
                    rend.color = Color.black;
                }
                else
                {
                    rend.color = Color.white;
                }
                
            }
        }
    }
}

    
