using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOfLife : GridObject
{
    public Text genText;
    public int genCount = 0;
    public Button resetButton;
    public Toggle randomToggle;
    public bool resetRandom;
    // Start is called before the first frame update
    void Start()
    {
        intialiseGrid(randomAtStart,-20.0f,-22.0f,0.2f);
        genText.text = "Generation: "+genCount;
        resetButton.onClick.AddListener(resetOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            stepConway();
            drawStates();
            generations.Add(states);
            genCount++;
            genText.text = "Generation: " + genCount;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            reverseState();
            if (genCount > 1)
            {
                genCount--;
                genText.text = "Generation: " + genCount;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            setCell();
            drawStates();
            generations.Add(states);
        }
    }

    void resetOnClick()
    {
        resetRandom = randomToggle.isOn;
        resetGrid(resetRandom);
        genCount = 0;
        genText.text = "Generation: " + genCount;
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
