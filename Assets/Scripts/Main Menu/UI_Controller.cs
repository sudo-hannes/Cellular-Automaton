using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Controller : MonoBehaviour
{

    public Button btnGameOfLife;
    public Button btnLangtonsAnt;
    public Button btnExit;
    // Start is called before the first frame update
    void Start()
    {
        Button btnGoL = btnGameOfLife.GetComponent<Button>();
        btnGoL.onClick.AddListener(NavigateGameOfLife);

        Button btnLA = btnLangtonsAnt.GetComponent<Button>();
        btnLA.onClick.AddListener(NavigateLangtonsAnt);

        Button btnE = btnExit.GetComponent<Button>();
        btnE.onClick.AddListener(ExitGame);
    }

    void NavigateGameOfLife()
    {
        SceneManager.LoadScene("Conways Game Of Life");
    }

    void NavigateLangtonsAnt()
    {
        SceneManager.LoadScene("Langtons Ant");
    }

    void ExitGame()
    {
        //Debug.Log("QUIT!");
        Application.Quit();
    }
}
