﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Controller_LangtonsAnt : MonoBehaviour
{
    public Button addRuleButton;
    public Button startButton;
    public Button directionButton;
    public Button restartButton;
    public GameObject rulePrefab;
    public GameObject canvas;
    public GameObject ruleDirectionText;
    public GameObject ruleColourText;
    public GameObject ruleCurrentColourText;
    public GameObject ruleStringInput;
    private bool inputValid = false;
    public Button ruleStringSubmit;
    public GameObject ruleStringFromText;
    public GameObject ruleStringToText;
    public static bool start = false;
    private int index = 0;
    public Slider sliderElement;
    public Text sliderText;
    public Button infoButton;
    public GameObject infoText;
    private bool infoShown = false;
    public Button exitButton;
    // Start is called before the first frame update
    void Start()
    {
        sliderElement = GameObject.Find("StepSlider").GetComponent<Slider>();
        sliderText = GameObject.Find("SliderText").GetComponent<Text>();

        Button btnAddRule = addRuleButton.GetComponent<Button>();
        btnAddRule.onClick.AddListener(AddRule);

        Button btnDirection = directionButton.GetComponent<Button>();
        btnDirection.onClick.AddListener(ChangeDirection);

        //CheckInputString();
        InputField txtRuleString = ruleStringInput.GetComponent<InputField>();
        txtRuleString.onValueChanged.AddListener(delegate { CheckInputString(); });

        Button ruleStringApply = ruleStringSubmit.GetComponent<Button>();
        ruleStringApply.onClick.AddListener(applyRuleString);

        Button start = startButton.GetComponent<Button>();
        start.onClick.AddListener(StartAnt);

        Button restart = restartButton.GetComponent<Button>();
        restart.onClick.AddListener(RestartAnt);

        Slider slider = sliderElement.GetComponent<Slider>();
        slider.onValueChanged.AddListener(delegate { ChangeStep(); });

        Button btnInfo = infoButton.GetComponent<Button>();
        btnInfo.onClick.AddListener(ShowInfo);

        Button btnExit = exitButton.GetComponent<Button>();
        btnExit.onClick.AddListener(ExitToMain);
    }

    void ExitToMain()
    {
        SceneManager.LoadScene("Main Menu");
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ExitToMain();
        }
    }

    void CheckInputString()
    {
        string input = ruleStringInput.transform.GetChild(2).GetComponent<Text>().text;
        int validCount = 0;
        foreach (char item in input)
        {
            if(item == 'L' || item == 'R')
            {
                validCount++;
            }
        }

        if(validCount == input.Length)
        {
            inputValid = true;
        }
        else
        {
            inputValid = false;
        }
        if(input.Length < 1)
        {
            inputValid = false;
        }
        ruleStringSubmit.interactable = inputValid;

    }
    void applyRuleString()
    {
        string input = ruleStringInput.transform.GetChild(2).GetComponent<Text>().text;

        string textFromColour = ruleStringFromText.transform.GetComponent<Text>().text;
        Color fromColor = Color.black;
        string textToColour = ruleStringToText.transform.GetComponent<Text>().text;
        Color toColor = Color.blue;

        if (textFromColour == "Black")
        {
            fromColor = Color.black;
        }
        else if (textFromColour == "Red")
        {
            fromColor = Color.red;
        }
        else if (textFromColour == "Yellow")
        {
            fromColor = Color.yellow;
        }
        else if (textFromColour == "Green")
        {
            fromColor = Color.green;
        }
        else if (textFromColour == "Blue")
        {
            fromColor = Color.blue;
        }
        else if (textFromColour == "Purple")
        {
            fromColor = Color.magenta;
        }

        if (textToColour == "Black")
        {
            toColor = Color.black;
        }
        else if (textToColour == "Red")
        {
            toColor = Color.red;
        }
        else if (textToColour == "Yellow")
        {
            toColor = Color.yellow;
        }
        else if (textToColour == "Green")
        {
            toColor = Color.green;
        }
        else if (textToColour == "Blue")
        {
            toColor = Color.blue;
        }
        else if (textToColour == "Purple")
        {
            toColor = Color.magenta;
        }

        Debug.Log(input);

        LangtonsAnt ant = GameObject.Find("Grid").GetComponent<LangtonsAnt>();
        ant.runRuleString(input, fromColor, toColor);
        ruleStringSubmit.interactable = false;
    }
    void ChangeDirection()
    {
        string text = ruleDirectionText.GetComponent<Text>().text;
        if (text == "Left")
        {
            text = "Right";
        }
        else
        {
            text = "Left";
        }
        ruleDirectionText.GetComponent<Text>().text = text;
    }

    void StartAnt()
    {
        LangtonsAnt ant = GameObject.Find("Grid").GetComponent<LangtonsAnt>();
        start = ant.start;
        if(start == false)
        {
            start = true;
            startButton.transform.GetChild(0).GetComponent<Text>().text = "Stop";
        }
        else if(start == true)
        {
            start = false;
            startButton.transform.GetChild(0).GetComponent<Text>().text = "Start";
        }
        ant.start = start;
    }
    void RestartAnt()
    {
        SceneManager.LoadScene("Langtons Ant");
    }

    void AddRule()
    {
        string textCurrentColour = ruleCurrentColourText.transform.GetComponent<Text>().text;
        Color currentColour = Color.black;
        string textDirection = ruleDirectionText.transform.GetComponent<Text>().text;
        int direction = -99;
        string textResColour = ruleColourText.transform.GetComponent<Text>().text;
        Color selectedColour = Color.blue;

        if (textCurrentColour == "Black")
        {
            currentColour = Color.black;
        }
        else if (textCurrentColour == "Red")
        {
            currentColour = Color.red;
        }
        else if (textCurrentColour == "Yellow")
        {
            currentColour = Color.yellow;
        }
        else if (textCurrentColour == "Green")
        {
            currentColour = Color.green;
        }
        else if (textCurrentColour == "Blue")
        {
            currentColour = Color.blue;
        }
        else if (textCurrentColour == "Purple")
        {
            currentColour = Color.magenta;
        }


        if (textDirection == "Left")
        {
            direction = -1;
        }
        else if (textDirection == "Right")
        {
            direction = 1;
        }

        if(textResColour == "Black")
        {
            selectedColour = Color.black;
        }
        else if(textResColour == "Red")
        {
            selectedColour = Color.red;
        }
        else if (textResColour == "Yellow")
        {
            selectedColour = Color.yellow;
        }
        else if (textResColour == "Green")
        {
            selectedColour = Color.green;
        }
        else if (textResColour == "Blue")
        {
            selectedColour = Color.blue;
        }
        else if (textResColour == "Purple")
        {
            selectedColour = Color.magenta;
        }

        GameObject uiSpawnPos = canvas.transform.GetChild(0).GetChild(1).gameObject;

        GameObject rule = Instantiate(rulePrefab, uiSpawnPos.transform.position + Vector3.down * 75 * index, Quaternion.identity, uiSpawnPos.transform);
        if(direction == -1)
        {
            rule.transform.GetChild(1).transform.Rotate(0f, 0f, 90f);
        }
        else
        {
            rule.transform.GetChild(1).transform.Rotate(0f, 0f, -90f);
        }
        
        rule.transform.GetChild(0).GetComponent<Image>().color = selectedColour;
        Debug.Log(currentColour);
        Debug.Log(direction);
        Debug.Log(selectedColour);

        LangtonsAnt ant = GameObject.Find("Grid").GetComponent<LangtonsAnt>();
        ant.addRule(currentColour, direction, selectedColour);

        index++;
    }

    void ChangeStep()
    {
        LangtonsAnt ant = GameObject.Find("Grid").GetComponent<LangtonsAnt>();
        ant.stepCount = (int)sliderElement.value;
        sliderText.text = sliderElement.value.ToString();
    }

    void ShowInfo()
    {
        infoShown = !infoShown;
        infoText.SetActive(infoShown);
    }
}