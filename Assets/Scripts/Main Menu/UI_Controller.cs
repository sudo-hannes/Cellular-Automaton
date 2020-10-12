using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Controller : MonoBehaviour
{

    public Button btnGameOfLife;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = btnGameOfLife.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        SceneManager.LoadScene("Conways Game Of Life");
    }
}
