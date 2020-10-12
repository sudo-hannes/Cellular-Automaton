using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Controller_LangtonsAnt : MonoBehaviour
{
    public Button addRuleButton;
    public GameObject rulePrefab;
    public GameObject canvas;
    private int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = addRuleButton.GetComponent<Button>();
        btn.onClick.AddListener(AddRule);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
    void AddRule()
    {
        GameObject uiSpawnPos = canvas.transform.GetChild(1).gameObject;

        Instantiate(rulePrefab, uiSpawnPos.transform.position + Vector3.down * 125 * index, Quaternion.identity, uiSpawnPos.transform);

        index++;
    }
}
