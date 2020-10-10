using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class createGrid : MonoBehaviour
{
    public int rows = 87;
    public int cols = 48;
    public bool randomAtStart = false;
    public GameObject[,] cells;
    public GameObject prefab;
    public Color deadColor = new Color(191.0f, 191.0f, 191.0f);
    public Color aliveColor = new Color(34.0f,139.0f,34.0f);
    private int[,] states;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        intialiseGrid(randomAtStart);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            stepConway();
            drawStates();
        }

        if (Input.GetMouseButtonDown(0)) 
        {
            setCell();
        }
    }

    //step foward one generation using conway's game of life
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

    //intialise Grid at start of game
    void intialiseGrid(bool randomCells) 
    {
        states = new int[rows, cols];
        cells = new GameObject[rows, cols];
        cam = GetComponent<Camera>();
        for (int y = 0; y < cols; y++)
        {
            for (int x = 0; x < rows; x++)
            {
                if (randomCells)
                {
                    states[x, y] = Random.Range(0, 2);
                }
                else
                {
                    states[x, y] = 0;
                }
                cells[x, y] = Instantiate(prefab, new Vector3((x * 0.2f) - 1.0f, (y * 0.2f) - 0.8f, 0), Quaternion.identity);
                cells[x, y].name = x + ":" + y;
                if (x == 0)
                {
                    cells[x, y].SetActive(false);
                }
                if (y == 0)
                {
                    cells[x, y].SetActive(false);
                }
                if (y == cols - 1)
                {
                    cells[x, y].SetActive(false);
                }
                if (x == rows - 1)
                {
                    cells[x, y].SetActive(false);
                }
            }
        }
        drawStates();
    }

    //set a specific cell to alive or dead
    void setCell()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            SpriteRenderer rend = hit.transform.GetComponent<SpriteRenderer>();
            string name = hit.transform.name;
            string[] coordinates = name.Split(':');
            int x = int.Parse(coordinates[0]);
            int y = int.Parse(coordinates[1]);
            if (rend.color == aliveColor)
            {
                rend.color = deadColor;

                states[x, y] = 0;
            }
            else
            {
                rend.color = aliveColor;
                states[x, y] = 1;
            }
        }
    }

    //draws the current state
    void drawStates()
    {
        for (int y = 0; y < cols; y++)
        {
            for (int x = 0; x < rows; x++)
            {
                SpriteRenderer rend = cells[x, y].GetComponent<SpriteRenderer>();
                if (states[x, y] == 1)
                {
                    rend.color = aliveColor;
                }
                else
                {
                    rend.color = deadColor;
                }
                
            }
        }
    }
}

    
