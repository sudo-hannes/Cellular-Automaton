using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonScroll : MonoBehaviour
{
    [SerializeField]
    private Text buttonText;
    public GameOfLife golObj;
    public ButtonController buttonControl;

    public void setText(string textString) 
    {
        buttonText.text = textString;
    }

    public void OnClick() 
    {
        buttonControl.setCurrentState();
    }
}
