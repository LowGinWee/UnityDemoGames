using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject HowToPlayPanel;

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void HowToPlayPanelActive()
    {
        HowToPlayPanel.SetActive(true);
    }
    public void HowToPlayPanelDisable()
    {
        HowToPlayPanel.SetActive(false);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
