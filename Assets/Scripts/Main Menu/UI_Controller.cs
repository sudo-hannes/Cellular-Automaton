using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Controller : MonoBehaviour
{

    public Button btnGameOfLife;
    public Button btnLangtonsAnt;
    // Start is called before the first frame update
    void Start()
    {
        Button btnGoL = btnGameOfLife.GetComponent<Button>();
        btnGoL.onClick.AddListener(NavigateGameOfLife);

        Button btnLA = btnLangtonsAnt.GetComponent<Button>();
        btnLA.onClick.AddListener(NavigateLangtonsAnt);
    }

    void NavigateGameOfLife()
    {
        SceneManager.LoadScene("Conways Game Of Life");
    }

    void NavigateLangtonsAnt()
    {
        SceneManager.LoadScene("Langtons Ant");
    }
}
