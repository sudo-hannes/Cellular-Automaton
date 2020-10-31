using System.Collections;
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
    public Dictionary<Color, int> ruleDictionary;
    public Dictionary<Color, Color> colorMap;
    public LayerMask gridLayer;
    public int count = 0;
    public Text countText;
    public bool start = false;
    
    // Start is called before the first frame update
    void Start()
    {
        countText = GameObject.Find("CountText").GetComponent<Text>();
        ruleDictionary = new Dictionary<Color,int>();
        colorMap = new Dictionary<Color, Color>();
        aliveColor = Color.black;
        deadColor = Color.black;
        intialiseGrid(false, offsetX, offsetY,spaceInbetween);
        createAnt();
        //runRuleString("LRRRRLLLRRR", Color.black, Color.white);
    }

    // Update is called once per frame
    void Update()
    { 
        if(start)
        {
            if (ruleDictionary.Count > 0)
            {
                stepAnt();
                count++;
                countText.text = "Generation: " + count.ToString();
            }
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
            Debug.Log(currentColor.ToString());
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

        Debug.Log("Rules:");
        foreach (var item in ruleDictionary)
        {
            Debug.Log(item.Key + ", " + item.Value);
        }
        Debug.Log("Color Map:");
        foreach (var item in colorMap)
        {
            Debug.Log(item.Key + ", " + item.Value);
        }
    }

    public void runRuleString(string input, Color from, Color to)
    {

        Color b = from;
        Color w = to;
        Color l = from;

        int decision = 0;
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] == 'L')
            {
                decision = -1;
            }
            else if (input[i] == 'R')
            {
                decision = 1;
            }
            else
            {
                Debug.LogError("Incorrect input string");
            }
            if (i == 0)
            {
                l = Color.black;
                addRule(l, decision, Color.Lerp(b, w, (float)(i + 1) / input.Length));
            }
            else if (i == input.Length - 1)
            {
                l = Color.Lerp(b, w, (float)i / input.Length);
                addRule(l, decision, Color.black);
            }
            else
            {
                l = Color.Lerp(b, w, (float)i / input.Length);
                addRule(l, decision, Color.Lerp(b, w, (float)(i + 1) / input.Length));
            }

        }
    }
    public void runGrowingSquare()
    {
        /*
        Dictionary<Color, int> growingSquareRules = new Dictionary<Color, int>;
        Dictionary<Color, Color> growingSquareColorMap = new Dictionary<Color, Color>;

        Color b = Color.black;
        Color w = Color.white;

        growingSquareRules.Add();
        */
        Color b = Color.red;
        Color w = Color.white;
        Color l = Color.red;

        //LRRRRRLLR
        int[] ruleDirectionArray = { -1, 1, 1, 1, 1, 1, -1, -1, 1 };
        for (int i = 0; i < 9; i++)
        {
            if (i == 0)
            {
                l = Color.black;
                addRule(l, ruleDirectionArray[i], Color.Lerp(b, w, (float)(i + 1) / 9));
            }
            else if (i == 8)
            {
                l = Color.Lerp(b, w, (float)i / 9);
                addRule(l, ruleDirectionArray[i], Color.black);
            }
            else
            {
                l = Color.Lerp(b, w, (float)i / 9);
                addRule(l, ruleDirectionArray[i], Color.Lerp(b, w, (float)(i + 1) / 9));
            }

        }

    }
}