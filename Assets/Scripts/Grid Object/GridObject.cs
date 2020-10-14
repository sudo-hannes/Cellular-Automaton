using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    public int rows = 87;
    public int cols = 48;
    public bool randomAtStart = false;
    public GameObject[,] cells;
    public GameObject prefab;
    public Color deadColor = new Color(191.0f, 191.0f, 191.0f);
    public Color aliveColor = new Color(34.0f, 139.0f, 34.0f);
    public int[,] states;
    public List<int[,]> generations = new List<int[,]>();
    Camera cam;

    public void intialiseGrid(bool randomCells, float offsetx, float offsety, float spaceInbetween)
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
                cells[x, y] = Instantiate(prefab, new Vector3((x * spaceInbetween) + offsetx, (y * spaceInbetween) + offsety, 0), Quaternion.identity);
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

    public void resetGrid(bool randomAtStart)
    {
        generations.Clear();
        for (int y = 0; y < cols; y++)
        {
            for (int x = 0; x < rows; x++)
            {
                if (randomAtStart)
                {
                    states[x, y] = Random.Range(0, 2);
                }
                else 
                {
                    states[x, y] = 0;
                }
            }
        }
        drawStates();
    }

    //set a specific cell to alive or dead
    public void setCell()
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
                states[x, y] = 0;
            }
            else
            { 
                states[x, y] = 1;
            }
        }
    }

    //draws the current state
    public void drawStates()
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

    public void reverseState()
    {

        if (generations.Count-1 > 0)
        {
            generations.RemoveAt(generations.Count - 1);
            states = generations[generations.Count - 1];
            drawStates();
        }   
    }
}
