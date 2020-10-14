﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class LangtonsAnt : GridObject
{
    public float offsetX = -7f;
    public float offsetY = -5f;
    public float spaceInbetween = 0.2f;
    public GameObject ant;
    private GameObject instantiatedAnt;
    public Dictionary<Color,int> ruleDictionary;
    public Dictionary<Color, Color> colorMap;
    public LayerMask gridLayer;
    public int count = 0;
    public Text countText;
    
    // Start is called before the first frame update
    void Start()
    {
        countText = GameObject.Find("CountText").GetComponent<Text>();
        ruleDictionary = new Dictionary<Color,int>();
        colorMap = new Dictionary<Color, Color>();
        aliveColor = Color.white;
        deadColor = Color.white;
        intialiseGrid(false, offsetX, offsetY,spaceInbetween);
        createAnt();
    }

    // Update is called once per frame
    void Update()
    {
        if(ruleDictionary.Count > 0)
        {
            stepAnt();
            count++;
            countText.text = count.ToString();
        }
    }
    
    void createAnt()
    {
        string midCellCoords = rows / 2 + ":" + cols / 2;
        GameObject midCell = GameObject.Find(midCellCoords);
        instantiatedAnt = Instantiate(ant, midCell.transform.position, Quaternion.identity);
    }

    void stepAnt()
    {
        //Get block the ant is on
        Collider2D[] colliders = Physics2D.OverlapCircleAll(instantiatedAnt.transform.position, 0.1f);
        //Debug.Log(colliders.Length);
        foreach (Collider2D collider in colliders)
        {
            //Find color of block
            GameObject currentBlock = collider.gameObject;
            Color currentColor = currentBlock.GetComponent<SpriteRenderer>().color;

            //Find direction based on rule
            applyRule(currentColor, currentBlock.GetComponent<SpriteRenderer>());
            return;
        }     
    }

   

    void applyRule(Color color, SpriteRenderer spriteRenderer)
    {
        //Search dictionary for color
        //Get decision from dictionary
        int direction = ruleDictionary[color];
        //Change current block color
        spriteRenderer.color = colorMap[color];
        //applyMovement
        antMove(direction);
    }

    void antMove(int direction)
    {
        if(direction == 1)
        {
            instantiatedAnt.transform.Rotate(0f, 0f, 90f);
            Vector2 target = instantiatedAnt.transform.right * spaceInbetween + instantiatedAnt.transform.position;
            instantiatedAnt.transform.position = Vector2.MoveTowards(instantiatedAnt.transform.position, target, 1);
        }
        else if (direction == -1)
        {
            instantiatedAnt.transform.Rotate(0f, 0f, -90f);
            Vector2 target = instantiatedAnt.transform.right * spaceInbetween + instantiatedAnt.transform.position;
            instantiatedAnt.transform.position = Vector2.MoveTowards(instantiatedAnt.transform.position, target, 1);
        }
        
    }

    public void addRule(Color color, int direction, Color resultColor)
    {
        ruleDictionary.Add(color, direction);
        colorMap.Add(color, resultColor);
        //Debug.Log(ruleDictionary);
    }
}