using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Controller_GameOfLIfe : MonoBehaviour
{
    public Button infoButton;
    public GameObject infoText;
    private bool infoShown = false;
    public Button exitButton;
    // Start is called before the first frame update
    void Start()
    {
        Button btnInfo = infoButton.GetComponent<Button>();
        btnInfo.onClick.AddListener(ShowInfo);

        Button btnExit = exitButton.GetComponent<Button>();
        btnExit.onClick.AddListener(ExitToMain);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main Menu");
        }
    }

    void ExitToMain()
    {
        SceneManager.LoadScene("Main Menu");
    }

    void ShowInfo()
    {
        infoShown = !infoShown;
        infoText.SetActive(infoShown);
    }
}
