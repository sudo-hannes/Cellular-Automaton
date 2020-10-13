using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LangtonsAnt : GridObject
{
    public float offsetX = -7f;
    public float offsetY = -5f;
    public float spaceInbetween = 0.2f;
    public GameObject ant;
    private GameObject instantiatedAnt;
    private IDictionary<Color,int> ruleDictionary;
    public LayerMask gridLayer;
    
    // Start is called before the first frame update
    void Start()
    {
        intialiseGrid(false, offsetX, offsetY,spaceInbetween);
        createAnt();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            stepAnt();
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
        Debug.Log(colliders.Length);
        foreach (Collider2D collider in colliders)
        {
            //Find color of block
            GameObject currentBlock = collider.gameObject;
            Color currentColor = currentBlock.GetComponent<SpriteRenderer>().color;
            
            //Find applicable rule
            Debug.Log(collider.gameObject.name);
            
        }     
        
        
        //Apply Rule
        instantiatedAnt.transform.Rotate(0f, 0f, 90f);
        Vector2 target = instantiatedAnt.transform.right * spaceInbetween + instantiatedAnt.transform.position;
        instantiatedAnt.transform.position = Vector2.MoveTowards(instantiatedAnt.transform.position, target, 1);
    }

    void applyRule(Color color, SpriteRenderer spriteRenderer)
    {
        //Search dictionary for color

        //Get decision from dictionary

        //applyMovement
    }
}
