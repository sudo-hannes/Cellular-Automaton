using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplate;
    public GameOfLife golObj;
    int[,] state;
    public TextAsset jsonFile;
    private void Start()
    {
        PatternsData patterns = JsonUtility.FromJson<PatternsData>(jsonFile.text);

        for (int i = 0; i < patterns.patterns.Count; i++)
        {
            var sprite =  Resources.Load<Sprite>(i.ToString());
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.SetActive(true);
            button.name = i.ToString();
            button.GetComponent<Button>().image.sprite = sprite;
            button.GetComponent<Button>().onClick.AddListener(setCurrentState);
            button.GetComponent<ButtonScroll>().setText(i.ToString());
            button.transform.SetParent(buttonTemplate.transform.parent);
        }
    }

    public void setCurrentState() 
    {
        PatternsData patterns = JsonUtility.FromJson<PatternsData>(jsonFile.text);
        GameObject go = EventSystem.current.currentSelectedGameObject;
        int idx = int.Parse(go.name);
        PatternData patternString = patterns.patterns[idx];
        string[] strPattern = patternString.pattern;
        int rowLen = strPattern.Length;
        int colLen = strPattern[0].Length;
        int[,] pattern = new int[strPattern.Length, strPattern[0].Length];

        for (int i = 0; i < strPattern.Length; i++)
        {
            for (int j = 0; j < strPattern[0].Length; j++)
            {
                char[] temp = strPattern[i].ToCharArray();
                if (temp[j] == '.')
                {
                    pattern[i, j] = 0;
                }
                else
                {
                    pattern[i, j] = 1;
                }
            }
        }

        state = new int[golObj.rows, golObj.cols];
        for (int y = 0; y < golObj.cols; y++)
        {
            for (int x = 0; x < golObj.rows; x++)
            {
                state[x, y] = 0;
            }
        }
        int posStartX = (golObj.cols / 2) - (colLen / 2);
        int posStartY = (golObj.rows / 2) - (rowLen / 2);
        for (int y = 0; y < colLen; y++)
        {
            for (int x = 0; x < rowLen; x++)
            {
                state[y + posStartY, x + posStartX] = pattern[x, y];
            }
        }

        golObj.states = state;
        golObj.drawStates();
        Camera.main.transform.position = new Vector3(0.0f, 0.0f, -100.0f);
    }


    [Serializable]
    public class PatternData
    {
        public string[] pattern;
    }


    public class PatternsData
    {
        public List<PatternData> patterns;
    }

}
